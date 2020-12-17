using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS.FileTransfer.Client
{
    public partial class MessagesForm : Form
    {
        ConfigurationDetails _configuration = null;
        ServerCommunications _communications = null;
        Logger _logger = null;
        List<MessageHistoryCtrl> _messageHistoryCtrls = new List<MessageHistoryCtrl>();
        Dictionary<Guid, string> _filePaths = new Dictionary<Guid, string>();
        Control _previousFileHistoryProgressCtrl = null;
        UserContactCtrl _prevUserContactCtrl = null;
        CancellationTokenSource _waitCommandCancellationTokenSource = new CancellationTokenSource();

        enum TabSetFocusOptions
        {
            Never,
            OnCreate,
            Always
        }

        public MessagesForm(ConfigurationDetails configuration, ServerCommunications communications, Logger logger)
        {
            _configuration = configuration;
            _communications = communications;
            _logger = logger;

            InitializeComponent();
        }

        private async Task WaitServerCommand()
        {
            while (true)
            {
                try
                {
                    if (!_communications.IsLoggedIn())
                    {
                        try
                        {
                            if ((_waitCommandCancellationTokenSource == null) || (_waitCommandCancellationTokenSource.IsCancellationRequested))
                                _waitCommandCancellationTokenSource = new CancellationTokenSource();

                            await Task.Delay(60000, _waitCommandCancellationTokenSource.Token);
                        }
                        catch (TaskCanceledException) { }
                    }

                    if ((_waitCommandCancellationTokenSource == null) || (_waitCommandCancellationTokenSource.IsCancellationRequested))
                        _waitCommandCancellationTokenSource = new CancellationTokenSource();

                    var command = await _communications.WaitCommand(_waitCommandCancellationTokenSource);
                    if (command != null)
                    {
                        _logger.LogInformation(string.Format("Received command {0}", command.CommandType));

                        if (command.CommandType == CommandTypeEnum.ReceiveMessage)
                        {
                            await GetMessages(command.ItemIds);
                        }
                        else if (command.CommandType == CommandTypeEnum.UploadFile)
                        {
                            await UploadFiles(command.ItemIds);
                        }
                        else if (command.CommandType == CommandTypeEnum.DownloadFile)
                        {
                            await DownloadFiles(command.ItemIds);
                        }
                        else if (command.CommandType == CommandTypeEnum.ConnectionRequest)
                        {
                            await GetConnectionRequests(command.ItemIds);
                        }
                        else if (command.CommandType == CommandTypeEnum.ApprovedConnectionRequest)
                        {
                            await GetConnectionRequests(command.ItemIds);
                        }
                        else
                            MessageBox.Show("Unknown Command type.");
                    }
                }
                catch (Exception ex)
                {
                    this.Text = "HIIT - Error";
                    _logger.LogError(string.Format("Error waiting for command. {0}", ex));
                }
            }
        }

        private async Task Login()
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Your User Name must be specified.", "Missing Login Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Your Password must be specified.", "Missing Login Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _communications.LogIn(txtUserName.Text, txtPassword.Text);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Login Failed. {0}", ex.Message);
                _logger.LogError(msg);
                MessageBox.Show(msg, "Login Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnableDisableControls();
                return;
            }

            _logger.LogInformation(string.Format("Login Successful for user {0}.", txtUserName.Text));
            _configuration.UserName = txtUserName.Text;
            _configuration.Password = txtPassword.Text;
            if (chkSaveLogInDetails.Checked)
                _configuration.Save();
            await PopulateContactsList();
            _waitCommandCancellationTokenSource.Cancel(false);
            EnableDisableControls();
        }

        private async Task PopulateContactsList()
        {
            _prevUserContactCtrl = null;
            pnlContacts.Controls.Clear();
            comboUser.Items.Clear();

            var connections = await _communications.GetConnections();

#if DEBUG
            // Add self for testing
            connections.Add(new UserConnectionModel()
            {
                Status = UserConnectionStatusEnum.Approved,
                UserName = _configuration.UserName
            });
#endif

            foreach (var connection in connections)
            {
                if (connection.Status == UserConnectionStatusEnum.Approved)
                    comboUser.Items.Add(connection.UserName);

                CheckCreateMessagesControlForUser(connection);

                var ctrl = new UserContactCtrl()
                {
                    UserConnection = connection,
                    ConnectionSelected = x =>
                        {
                            ShowTabForUser(x.UserName, TabSetFocusOptions.Always);
                        }
                };
                pnlContacts.Controls.Add(ctrl);

                int posY = 0;
                if (_prevUserContactCtrl != null)
                    posY = _prevUserContactCtrl.Location.Y + _prevUserContactCtrl.Height + 2;
                ctrl.Location = new Point(0, posY);
                ctrl.Width = tabContacts.Width;
                ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                _prevUserContactCtrl = ctrl;

                var msgHistoryControl = _messageHistoryCtrls.FirstOrDefault(x => x.DestinationUserName == connection.UserName);
                if (msgHistoryControl != null)
                    msgHistoryControl.UserConnection = connection;

            }
        }

        private async Task GetConnectionRequests(IEnumerable<Guid> ids)
        {
            await PopulateContactsList();
            
            try
            {
                var connectionRequests = await _communications.GetConnectionRequest(ids);
                if ((connectionRequests == null) || (connectionRequests.Count == 0))
                    return;
                
                foreach (var request in connectionRequests)
                {
                    string userName = (request.RequestedByUserName == _configuration.UserName) ? request.ConnectToUserName : request.RequestedByUserName;
                    var messageCtrl = GetMessageHistoryCtrlForUser(userName);
                    if (messageCtrl == null)
                        continue;

                    messageCtrl.ConnectionRequest = request;
                    ShowTabForUser(userName, TabSetFocusOptions.Never);
                }
                
                var first = connectionRequests.First();
                //ShowBallonText(string.Format("{0} would like to connect with you", first.ConnectToUserName), first.IntroductionMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred getting connection requests. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DownloadFiles(IEnumerable<Guid> fileIds)
        {
            try
            {
                foreach (var fileId in fileIds)
                { 
                    var downloadCommand = await _communications.GetFileDownloadCommand(fileId);

                    ShowBallonText(string.Format("{0} has sent you a file.", downloadCommand.SourceUserName), string.Format("{0} wants to send you file {1}.", downloadCommand.SourceUserName, downloadCommand.FileName));
                    var ctrl = new FileDownloadProgressCtrl(_configuration, _communications, _logger)
                    {
                        FileDownloadCommand = downloadCommand,
                        NotifyDownloadStatus = NotifyFileDownloadStatus
                        
                    };
                    pnlFileTransferHistory.Controls.Add(ctrl);
                    ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    ctrl.Width = this.Width - 40;
                    int posY = 0;
                    if (_previousFileHistoryProgressCtrl != null)
                        posY = _previousFileHistoryProgressCtrl.Location.Y + _previousFileHistoryProgressCtrl.Height + 10;
                    ctrl.Location = new Point(5, posY);
                    pnlFileTransferHistory.ScrollControlIntoView(ctrl);

                    _previousFileHistoryProgressCtrl = ctrl;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred uploading file. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UploadFiles(IEnumerable<Guid> fileIds)
        {
            try
            {
                foreach (var fileId in fileIds)
                {
                    var uploadCommand = await _communications.GetFileUploadCommand(fileId);
                    string filePath = _filePaths[fileId];

                    var ctrl = new FileUploadProgressCtrl(_configuration, _communications, _logger)
                    {
                        FileUploadCommand = uploadCommand,
                        FilePath = filePath,
                        NotifyUploadStatus = NotifyFileUploadStatus
                    };
                    pnlFileTransferHistory.Controls.Add(ctrl);
                    ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    ctrl.Width = this.Width - 40;
                    int posY = 0;
                    if (_previousFileHistoryProgressCtrl != null)
                        posY = _previousFileHistoryProgressCtrl.Location.Y + _previousFileHistoryProgressCtrl.Height + 10;
                    ctrl.Location = new Point(5, posY);
                    pnlFileTransferHistory.ScrollControlIntoView(ctrl);

                    _previousFileHistoryProgressCtrl = ctrl;
                    await ctrl.StartUpload();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred uploading file. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotifyFileUploadStatus(FileUploadCommand model, FileTransferCompletionStatus status)
        {
            var messageCtrl = GetMessageHistoryCtrlForUser(model.DestinationUserName);
            if (messageCtrl == null)
                return;

            messageCtrl.AddFileUpload(model, status);
        }

        private void NotifyFileDownloadStatus(FileDownloadCommand model, FileTransferCompletionStatus status)
        {
            var messageCtrl = GetMessageHistoryCtrlForUser(model.SourceUserName);
            if (messageCtrl == null)
                return;

            messageCtrl.AddFileDownload(model, status);
        }

        private async Task GetMessages(IEnumerable<Guid> messageIds)
        {
            try
            {
                var messages = await _communications.GetMessage(messageIds);
                if (messages == null) 
                    return;

                foreach (var message in messages)
                {
                    var messageCtrl = GetMessageHistoryCtrlForUser(message.SourceUserName);
                    if (messageCtrl == null)
                        return;

                    messageCtrl.AddReceivedMessage(message);
                    ShowTabForUser(message.SourceUserName, TabSetFocusOptions.OnCreate);
                }

                var first = messages.First();
                ShowBallonText("New Message from " + first.SourceUserName, first.Message);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred getting messages. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private MessageHistoryCtrl GetMessageHistoryCtrlForUser(string userName)
        {
            return _messageHistoryCtrls.FirstOrDefault(x => x.DestinationUserName == userName);
        }

        private void ShowTabForUser(string userName, TabSetFocusOptions setFocusOption)
        {
            if (tabControl.TabPages.ContainsKey(userName))
            {
                if (setFocusOption == TabSetFocusOptions.Always)
                    tabControl.SelectTab(userName);
                return;
            }

            var ctrl = _messageHistoryCtrls.FirstOrDefault(x => x.DestinationUserName == userName);
            if (ctrl == null)
                return;

            TabPage newTab = new TabPage() { Text = userName, Name = userName };
            tabControl.TabPages.Add(newTab);
            newTab.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;

            if ((setFocusOption == TabSetFocusOptions.Always) || (setFocusOption == TabSetFocusOptions.OnCreate))
                tabControl.SelectTab(userName);
        }

        private MessageHistoryCtrl CheckCreateMessagesControlForUser(UserConnectionModel connection)
        {
            var control = _messageHistoryCtrls.FirstOrDefault(x => x.DestinationUserName == connection.UserName);
            if (control != null)
                return control;

            control = new MessageHistoryCtrl(_configuration, _communications, _logger)
            {
                UserConnection = connection,
                ApproveContactRequestAction = x => SetContactRequestStatus(x, ConnectionRequestStatusEnum.Approved),
                RejectContactRequestAction = x => SetContactRequestStatus(x, ConnectionRequestStatusEnum.Rejected),
                CloseTabAction = () =>
                {
                    tabControl.TabPages.RemoveByKey(connection.UserName);
                }
            };
            _messageHistoryCtrls.Add(control);
            return control;
        }

        private async Task<bool> SetContactRequestStatus(Guid connectionRequestId, ConnectionRequestStatusEnum status)
        {
            try
            {
                await _communications.SetContactRequestStatus(new ConnectionRequestUpdateStatusModel() 
                { 
                    Id = connectionRequestId, 
                    Status = status
                });
                return true;
            }
            catch (Exception ex)
            {
                string msg = string.Format("An error occurred approving connection request. {0}", ex.Message);
                _logger.LogError(msg);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void EnableDisableControls()
        {
            if (_communications.IsLoggedIn())
            {
                this.Text = "HIIT - Logged In";
                cmdLogin.Text = "Sign Out";
            }
            else
            {
                this.Text = "HIIT - NOT Logged In";
                cmdLogin.Text = "Sign In";
            }
        }

        private async void MessagesForm_Load(object sender, EventArgs e)
        {
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));

            try
            {
                EnableDisableControls();
                txtUserName.Text = _configuration.UserName;
                txtPassword.Text = _configuration.Password;
                txtDownloadFolder.Text = _configuration.DownloadFolder;
                chkSaveLogInDetails.Checked = _configuration.RememberLoginDetails;
                chkShowSysTrayIcon.Checked = _configuration.ShowSysTrayIcon;
                chkHideWhenMinimized.Checked = _configuration.HideWhenMinimized;
                chkAutoStart.Checked = _configuration.AutomaticLogin;
                if (_configuration.AutomaticLogin && (txtUserName.Text != "") && (txtPassword.Text != ""))
                    await Login();

                await WaitServerCommand();
            }
            catch (Exception ex)
            {
                string msg = string.Format("An error occurred loading configuration. {0}", ex.Message);
                _logger.LogError(msg);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MessagesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
#if !DEBUG
            e.Cancel = true;
            this.Hide();
#endif
            
        }

        private async void cmdSendFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboUser.SelectedItem == null)
                {
                    MessageBox.Show("A User must be selected", "Information Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtSendFilePath.Text == "")
                {
                    MessageBox.Show("A file must be specified", "Information Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(txtSendFilePath.Text))
                {
                    MessageBox.Show("The specified File is invalid", "Information Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FileInfo fileInfo = new FileInfo(txtSendFilePath.Text);

                FileOfferModel fileOffer = new FileOfferModel()
                {
                    OfferedToUserName = comboUser.SelectedItem.ToString(),
                    Description = txtSendFileDescription.Text,
                    FileName = Path.GetFileName(txtSendFilePath.Text),
                    Size = fileInfo.Length,
                    NumberOfFileParts = FileTransferHelper.GetNumberOfFileParts(txtSendFilePath.Text, Program.FilePartSize),
                    Hash = FileTransferHelper.GetSHA1Hash(txtSendFilePath.Text)

                };
                Guid fileId = await _communications.SendFileTransferOffer(fileOffer);
                _filePaths.Add(fileId, txtSendFilePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred sending file. " + ex.Message);
            }
        }

        private void cmdBrowseFile_Click(object sender, EventArgs e)
        {
            SelectFileToSendDialog.Multiselect = false;
            if (SelectFileToSendDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtSendFilePath.Text = SelectFileToSendDialog.FileName;
        }

        private void MessagesForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void ShowBallonText(string title, string message)
        {
            notifyIcon.ShowBalloonTip(15000, message, title, ToolTipIcon.None);
        }

        private async void cmdAddContact_Click(object sender, EventArgs e)
        {
            try
            {
                await _communications.SendConnectionRequest(new CreateConnectionRequestModel()
                {
                    ConnectToUserName = txtAddContact.Text,
                    IntroductionMessage = txtIntroductionMessage.Text
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred getting messages. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async void cmdLogin_Click(object sender, EventArgs e)
        {
            if (_communications.IsLoggedIn())
            {
                _communications.Logout();
                _waitCommandCancellationTokenSource.Cancel();
            }
            else
                await Login();

            EnableDisableControls();
        }

        private void cmdContacts_Click(object sender, EventArgs e)
        {

        }

        private void cmdViewFilesTransfers_Click(object sender, EventArgs e)
        {

        }

        private void cmdMessages_Click(object sender, EventArgs e)
        {

        }

        private void chkSaveLogInDetails_CheckedChanged(object sender, EventArgs e)
        {
            _configuration.RememberLoginDetails = chkSaveLogInDetails.Checked;
            _configuration.Save();
        }

        private void txtDownloadFolder_TextChanged(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtDownloadFolder.Text))
                Directory.CreateDirectory(txtDownloadFolder.Text);

            _configuration.DownloadFolder = txtDownloadFolder.Text;
            _configuration.Save();
        }

        private void chkShowSysTrayIcon_CheckedChanged(object sender, EventArgs e)
        {
            _configuration.ShowSysTrayIcon = chkShowSysTrayIcon.Checked;
            _configuration.Save();
        }

        private void chkHideWhenMinimized_CheckedChanged(object sender, EventArgs e)
        {
            _configuration.HideWhenMinimized = chkHideWhenMinimized.Checked;
            _configuration.Save();
        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            _configuration.AutomaticLogin = chkAutoStart.Checked;
            _configuration.Save();
        }

        private void cmdBrowseDownloadFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtDownloadFolder.Text = folderBrowserDialog.SelectedPath;
        }
        
    }
}
