using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RS.FileTransfer.Common.Models;

namespace RS.FileTransfer.Client
{
    
    public partial class MessageHistoryCtrl : UserControl
    {
        ConfigurationDetails _configuration = null;
        ServerCommunications _communications = null;
        Logger _logger = null;
        Control _prevControl = null;

        Panel _pnlConnectionRequest = null;

        UserConnectionModel _userConnection = null;
        ConnectionRequestModel _connectionRequest = null;

        public Action CloseTabAction { get; set; }

        public Func<Guid, Task<bool>> ApproveContactRequestAction { get; set; }

        public Func<Guid, Task<bool>> RejectContactRequestAction { get; set; }

        public string DestinationUserName 
        {
            get { return _userConnection.UserName; }
        }

        public UserConnectionModel UserConnection
        {
            set 
            { 
                _userConnection = value;
                if (_userConnection != null)
                    UpdateControls();
            }
        }

        public ConnectionRequestModel ConnectionRequest
        {
            set
            {
                _connectionRequest = value;
                if (_userConnection != null)
                    UpdateControls();
            }
        }
        public MessageHistoryCtrl(ConfigurationDetails configuration, ServerCommunications communications, Logger logger)
        {
            _configuration = configuration;
            _communications = communications;
            _logger = logger;

            InitializeComponent();
        }

        private void MessageHistoryCtrl_Load(object sender, EventArgs e)
        {
            
        }

        void UpdateControls()
        {
            lblUserName.Text = "Chatting to " + DestinationUserName;
            txtSendMessage.Visible = _userConnection.Status == UserConnectionStatusEnum.Approved;
            cmdSendMessage.Visible = _userConnection.Status == UserConnectionStatusEnum.Approved;
            CheckAddConnectionRequestSection();
        }

        public void AddReceivedMessage(ReceivedMessageModel message)
        {
            AddLabelForMessage(_configuration.UserName, message.SourceUserName, message.Message, message.SentDate, true);
        }

        public void AddFileUpload(FileUploadCommand model, FileTransferCompletionStatus status)
        {
            string message = "";
            if (status == FileTransferCompletionStatus.Successful)
            {
                message = string.Format("Successfully uploaded file '{0}' to user {1}", model.FileName, model.DestinationUserName);
            }
            else if (status == FileTransferCompletionStatus.Error)
            {
                message = string.Format("An error occurred uploading file '{0}' to user {1}", model.FileName, model.DestinationUserName);
            }
            else if (status == FileTransferCompletionStatus.Cancelled)
            {
                message = string.Format("Upload of file '{0}' to user {1} was cancelled", model.FileName, model.DestinationUserName);
            }
            else
                return;

            var pnl = AddSectionPanel(40);
            pnlMessages.ScrollControlIntoView(pnl);
            _prevControl = pnl;

            Label lbl = new Label();
            lbl.Text = message;
            pnl.Controls.Add(lbl);
            lbl.AutoSize = true;
            lbl.Location = new Point(5, 5);
        }

        public void AddFileDownload(FileDownloadCommand model, FileTransferCompletionStatus status)
        {
            string message = "";
            if (status == FileTransferCompletionStatus.Successful)
            {
                message = string.Format("Successfully download file '{0}' from user {1}", model.FileName, model.SourceUserName);
            }
            else if (status == FileTransferCompletionStatus.Error)
            {
                message = string.Format("An error occurred downloading file '{0}' from user {1}", model.FileName, model.SourceUserName);
            }
            else if (status == FileTransferCompletionStatus.Cancelled)
            {
                message = string.Format("Download of file '{0}' from user {1} was cancelled", model.FileName, model.SourceUserName);
            }
            else
                return;

            var pnl = AddSectionPanel(40);
            pnlMessages.ScrollControlIntoView(pnl);
            _prevControl = pnl;

            Label lbl = new Label();
            lbl.Text = message;
            pnl.Controls.Add(lbl);
            lbl.AutoSize = true;
            lbl.Location = new Point(5, 5);
        }

        void AddLabelForMessage(string from, string to, string message, DateTime date, bool isReceived)
        {
            Label lbl = new Label();
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Text = message;
            lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lbl.MinimumSize = new Size(pnlMessages.Width - 60, 30);
            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(pnlMessages.Width - 60, 500);
            pnlMessages.Controls.Add(lbl);

            int posY = 5;
            if (_prevControl != null)
                posY = _prevControl.Location.Y + _prevControl.Height + 5;

            if (isReceived)
            {
                lbl.Location = new System.Drawing.Point(5, posY);
                lbl.BackColor = Color.PowderBlue;
                lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
            else
            {
                lbl.Location = new Point(35, posY);
                lbl.BackColor = Color.Salmon;
                lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            pnlMessages.ScrollControlIntoView(lbl);
            _prevControl = lbl;
        }

        void CheckAddConnectionRequestSection()
        {
            if (_connectionRequest == null)
            {
                if (_pnlConnectionRequest == null)
                    return;
                _pnlConnectionRequest.Controls.Clear();
                _pnlConnectionRequest = null;
                return;
            }

            if (_connectionRequest.RequestedByUserName == _configuration.UserName)
            {
                // Add connection requested by me to other user section
                if (_pnlConnectionRequest == null)
                    _pnlConnectionRequest = AddSectionPanel(40);
                _pnlConnectionRequest.Controls.Clear();

                Label lbl = new Label()
                {
                    Location = new Point(5, 5),
                    Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    AutoSize = true
                };
                _pnlConnectionRequest.Controls.Add(lbl);

                if (_connectionRequest.Status == ConnectionRequestStatusEnum.Approved)
                    lbl.Text = _connectionRequest.ConnectToUserName + " has connected with you.";
                else
                    lbl.Text = "You have requested to connect to " + _connectionRequest.ConnectToUserName;

            }
            else
            {
                // Add connection requested by other user to me section
                if (_pnlConnectionRequest == null)
                    _pnlConnectionRequest = AddSectionPanel(70);
                _pnlConnectionRequest.Controls.Clear();

                Label lbl = new Label()
                {
                    Location = new Point(5, 5),
                    Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    AutoSize = true
                };
                _pnlConnectionRequest.Controls.Add(lbl);
                if (_connectionRequest.Status == ConnectionRequestStatusEnum.Requested)
                {
                    lbl.Text = "User " + _connectionRequest.RequestedByUserName + " wants to connect with you";
                    Button button = new Button()
                    {
                        Text = "Approve",
                        Location = new Point(5, 35),
                        AutoSize = true
                    };
                    button.Click += ApproveContactRequest_Click;
                    _pnlConnectionRequest.Controls.Add(button);

                    button = new Button()
                    {
                        Text = "Ignore",
                        Location = new Point(90, 35),
                        AutoSize = true
                    };
                    button.Click += RejectContactRequest_Click;
                    _pnlConnectionRequest.Controls.Add(button);
                }
                else if (_connectionRequest.Status == ConnectionRequestStatusEnum.Approved)
                {
                    lbl.Text = "You have connected with " + _connectionRequest.RequestedByUserName;
                }
                else if (_connectionRequest.Status == ConnectionRequestStatusEnum.Rejected)
                {
                    lbl.Text = "You have Rejected connected to " + _connectionRequest.RequestedByUserName;

                    Button button = new Button()
                    {
                        Text = "Approve",
                        Location = new Point(5, 60),
                    };
                    button.Click += ApproveContactRequest_Click;
                    _pnlConnectionRequest.Controls.Add(button);  
                }
            }

            if (_prevControl == null)
                _prevControl = _pnlConnectionRequest;
        }

        async void ApproveContactRequest_Click(object sender, EventArgs e)
        {
            await ApproveContactRequestAction(_connectionRequest.Id);
        }

        async void RejectContactRequest_Click(object sender, EventArgs e)
        {
            await RejectContactRequestAction(_connectionRequest.Id);
        }

        Panel AddSectionPanel(int height)
        {
            Panel pnl = new Panel();
            pnl.Width = this.Width - 20;
            pnl.Height = height;
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlMessages.Controls.Add(pnl);
            int posY = 0;
            if (_prevControl != null)
                posY = _prevControl.Location.Y + _prevControl.Height + 5;
            pnl.Location = new Point(5, posY);
            return pnl;
        }

        async Task SendMessage()
        {
            try
            { 
                await _communications.SendMessage(new SendMessageModel() 
                {
                    DestinationUserName = DestinationUserName,
                    Message = txtSendMessage.Text,
                });

                AddLabelForMessage(_configuration.UserName, DestinationUserName, txtSendMessage.Text, DateTime.Now, false);
                txtSendMessage.Text = "";

            }
            catch (Exception ex)
            {
                string msg = string.Format("An error occurred sending message. {0}", ex.Message);
                _logger.LogError(msg);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void txtSendMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtSendMessage.Text == "")
                return;

            if (e.KeyChar != '\r')
                return;

            await SendMessage();
        }

        private async void cmdSendMessage_Click(object sender, EventArgs e)
        {
            if (txtSendMessage.Text == "")
                return;

            await SendMessage();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            CloseTabAction();
        }
    }
}
