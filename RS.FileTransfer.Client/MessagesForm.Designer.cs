namespace RS.FileTransfer.Client
{
    partial class MessagesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagesForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabConfiguration = new System.Windows.Forms.TabPage();
            this.Settings = new System.Windows.Forms.GroupBox();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.txtDownloadFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkHideWhenMinimized = new System.Windows.Forms.CheckBox();
            this.cmdBrowseDownloadFolder = new System.Windows.Forms.Button();
            this.chkShowSysTrayIcon = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSaveLogInDetails = new System.Windows.Forms.CheckBox();
            this.cmdLogin = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdContacts = new System.Windows.Forms.Button();
            this.cmdMessages = new System.Windows.Forms.Button();
            this.cmdViewFilesTransfers = new System.Windows.Forms.Button();
            this.tabContacts = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtIntroductionMessage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddContact = new System.Windows.Forms.TextBox();
            this.cmdAddContact = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlContacts = new System.Windows.Forms.Panel();
            this.tabFiles = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSendFile = new System.Windows.Forms.Button();
            this.txtSendFileDescription = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdBrowseFile = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSendFilePath = new System.Windows.Forms.TextBox();
            this.pnlFileTransferHistory = new System.Windows.Forms.Panel();
            this.SelectFileToSendDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            this.Settings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabContacts.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabFiles.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabConfiguration);
            this.tabControl.Controls.Add(this.tabContacts);
            this.tabControl.Controls.Add(this.tabFiles);
            this.tabControl.Location = new System.Drawing.Point(1, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(577, 630);
            this.tabControl.TabIndex = 0;
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.Controls.Add(this.Settings);
            this.tabConfiguration.Controls.Add(this.groupBox3);
            this.tabConfiguration.Controls.Add(this.groupBox2);
            this.tabConfiguration.Location = new System.Drawing.Point(4, 27);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.Size = new System.Drawing.Size(569, 599);
            this.tabConfiguration.TabIndex = 2;
            this.tabConfiguration.Text = "Configuration";
            this.tabConfiguration.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.Settings.Controls.Add(this.chkAutoStart);
            this.Settings.Controls.Add(this.txtDownloadFolder);
            this.Settings.Controls.Add(this.label11);
            this.Settings.Controls.Add(this.chkHideWhenMinimized);
            this.Settings.Controls.Add(this.cmdBrowseDownloadFolder);
            this.Settings.Controls.Add(this.chkShowSysTrayIcon);
            this.Settings.Location = new System.Drawing.Point(3, 172);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(561, 135);
            this.Settings.TabIndex = 59;
            this.Settings.TabStop = false;
            this.Settings.Text = "Settings";
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new System.Drawing.Point(11, 105);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(494, 24);
            this.chkAutoStart.TabIndex = 52;
            this.chkAutoStart.Text = "Start and Sign In automatically when Windows starts";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.chkAutoStart.CheckedChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
            // 
            // txtDownloadFolder
            // 
            this.txtDownloadFolder.Location = new System.Drawing.Point(134, 23);
            this.txtDownloadFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtDownloadFolder.Name = "txtDownloadFolder";
            this.txtDownloadFolder.Size = new System.Drawing.Size(363, 27);
            this.txtDownloadFolder.TabIndex = 47;
            this.txtDownloadFolder.TextChanged += new System.EventHandler(this.txtDownloadFolder_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 20);
            this.label11.TabIndex = 46;
            this.label11.Text = "Download Folder :";
            // 
            // chkHideWhenMinimized
            // 
            this.chkHideWhenMinimized.AutoSize = true;
            this.chkHideWhenMinimized.Checked = true;
            this.chkHideWhenMinimized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideWhenMinimized.Location = new System.Drawing.Point(11, 79);
            this.chkHideWhenMinimized.Name = "chkHideWhenMinimized";
            this.chkHideWhenMinimized.Size = new System.Drawing.Size(218, 24);
            this.chkHideWhenMinimized.TabIndex = 51;
            this.chkHideWhenMinimized.Text = "Hide When Minimized";
            this.chkHideWhenMinimized.UseVisualStyleBackColor = true;
            this.chkHideWhenMinimized.CheckedChanged += new System.EventHandler(this.chkHideWhenMinimized_CheckedChanged);
            // 
            // cmdBrowseDownloadFolder
            // 
            this.cmdBrowseDownloadFolder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdBrowseDownloadFolder.Location = new System.Drawing.Point(505, 22);
            this.cmdBrowseDownloadFolder.Margin = new System.Windows.Forms.Padding(4);
            this.cmdBrowseDownloadFolder.Name = "cmdBrowseDownloadFolder";
            this.cmdBrowseDownloadFolder.Size = new System.Drawing.Size(38, 23);
            this.cmdBrowseDownloadFolder.TabIndex = 48;
            this.cmdBrowseDownloadFolder.Text = "...";
            this.cmdBrowseDownloadFolder.UseVisualStyleBackColor = true;
            this.cmdBrowseDownloadFolder.Click += new System.EventHandler(this.cmdBrowseDownloadFolder_Click);
            // 
            // chkShowSysTrayIcon
            // 
            this.chkShowSysTrayIcon.AutoSize = true;
            this.chkShowSysTrayIcon.Checked = true;
            this.chkShowSysTrayIcon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSysTrayIcon.Location = new System.Drawing.Point(11, 53);
            this.chkShowSysTrayIcon.Name = "chkShowSysTrayIcon";
            this.chkShowSysTrayIcon.Size = new System.Drawing.Size(258, 24);
            this.chkShowSysTrayIcon.TabIndex = 50;
            this.chkShowSysTrayIcon.Text = "Show Icon in System Tray";
            this.chkShowSysTrayIcon.UseVisualStyleBackColor = true;
            this.chkShowSysTrayIcon.CheckedChanged += new System.EventHandler(this.chkShowSysTrayIcon_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSaveLogInDetails);
            this.groupBox3.Controls.Add(this.cmdLogin);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUserName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(3, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(561, 154);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Login Credentials";
            // 
            // chkSaveLogInDetails
            // 
            this.chkSaveLogInDetails.AutoSize = true;
            this.chkSaveLogInDetails.Location = new System.Drawing.Point(100, 90);
            this.chkSaveLogInDetails.Name = "chkSaveLogInDetails";
            this.chkSaveLogInDetails.Size = new System.Drawing.Size(155, 24);
            this.chkSaveLogInDetails.TabIndex = 52;
            this.chkSaveLogInDetails.Text = "Remember Me";
            this.chkSaveLogInDetails.UseVisualStyleBackColor = true;
            this.chkSaveLogInDetails.CheckedChanged += new System.EventHandler(this.chkSaveLogInDetails_CheckedChanged);
            // 
            // cmdLogin
            // 
            this.cmdLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdLogin.Location = new System.Drawing.Point(217, 117);
            this.cmdLogin.Margin = new System.Windows.Forms.Padding(4);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(115, 28);
            this.cmdLogin.TabIndex = 44;
            this.cmdLogin.Text = "Sign In";
            this.cmdLogin.UseVisualStyleBackColor = true;
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "User Name :";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(100, 60);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(397, 27);
            this.txtPassword.TabIndex = 43;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(100, 29);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(397, 27);
            this.txtUserName.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 63);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Password :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmdContacts);
            this.groupBox2.Controls.Add(this.cmdMessages);
            this.groupBox2.Controls.Add(this.cmdViewFilesTransfers);
            this.groupBox2.Location = new System.Drawing.Point(3, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(561, 174);
            this.groupBox2.TabIndex = 58;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 20);
            this.label2.TabIndex = 56;
            this.label2.Text = "View all messages";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(403, 20);
            this.label4.TabIndex = 55;
            this.label4.Text = "View the progress and history of file transfers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(378, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "View, add and remove your list of contacts";
            // 
            // cmdContacts
            // 
            this.cmdContacts.Location = new System.Drawing.Point(15, 28);
            this.cmdContacts.Name = "cmdContacts";
            this.cmdContacts.Size = new System.Drawing.Size(157, 29);
            this.cmdContacts.TabIndex = 52;
            this.cmdContacts.Text = "Manage Contacts";
            this.cmdContacts.UseVisualStyleBackColor = true;
            this.cmdContacts.Click += new System.EventHandler(this.cmdContacts_Click);
            // 
            // cmdMessages
            // 
            this.cmdMessages.Location = new System.Drawing.Point(15, 124);
            this.cmdMessages.Name = "cmdMessages";
            this.cmdMessages.Size = new System.Drawing.Size(157, 29);
            this.cmdMessages.TabIndex = 54;
            this.cmdMessages.Text = "Messages";
            this.cmdMessages.UseVisualStyleBackColor = true;
            this.cmdMessages.Click += new System.EventHandler(this.cmdMessages_Click);
            // 
            // cmdViewFilesTransfers
            // 
            this.cmdViewFilesTransfers.Location = new System.Drawing.Point(15, 75);
            this.cmdViewFilesTransfers.Name = "cmdViewFilesTransfers";
            this.cmdViewFilesTransfers.Size = new System.Drawing.Size(157, 29);
            this.cmdViewFilesTransfers.TabIndex = 53;
            this.cmdViewFilesTransfers.Text = "File Transfers";
            this.cmdViewFilesTransfers.UseVisualStyleBackColor = true;
            this.cmdViewFilesTransfers.Click += new System.EventHandler(this.cmdViewFilesTransfers_Click);
            // 
            // tabContacts
            // 
            this.tabContacts.Controls.Add(this.groupBox4);
            this.tabContacts.Controls.Add(this.pnlContacts);
            this.tabContacts.Location = new System.Drawing.Point(4, 25);
            this.tabContacts.Name = "tabContacts";
            this.tabContacts.Size = new System.Drawing.Size(569, 601);
            this.tabContacts.TabIndex = 0;
            this.tabContacts.Text = "Contacts";
            this.tabContacts.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtIntroductionMessage);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtAddContact);
            this.groupBox4.Controls.Add(this.cmdAddContact);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(4, 482);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(562, 115);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Contact";
            // 
            // txtIntroductionMessage
            // 
            this.txtIntroductionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIntroductionMessage.Location = new System.Drawing.Point(103, 55);
            this.txtIntroductionMessage.Multiline = true;
            this.txtIntroductionMessage.Name = "txtIntroductionMessage";
            this.txtIntroductionMessage.Size = new System.Drawing.Size(363, 48);
            this.txtIntroductionMessage.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 20);
            this.label10.TabIndex = 5;
            this.label10.Text = "Message :";
            // 
            // txtAddContact
            // 
            this.txtAddContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddContact.Location = new System.Drawing.Point(103, 24);
            this.txtAddContact.Name = "txtAddContact";
            this.txtAddContact.Size = new System.Drawing.Size(363, 27);
            this.txtAddContact.TabIndex = 3;
            // 
            // cmdAddContact
            // 
            this.cmdAddContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddContact.Location = new System.Drawing.Point(472, 78);
            this.cmdAddContact.Name = "cmdAddContact";
            this.cmdAddContact.Size = new System.Drawing.Size(75, 23);
            this.cmdAddContact.TabIndex = 4;
            this.cmdAddContact.Text = "Add";
            this.cmdAddContact.UseVisualStyleBackColor = true;
            this.cmdAddContact.Click += new System.EventHandler(this.cmdAddContact_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "User Name :";
            // 
            // pnlContacts
            // 
            this.pnlContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContacts.Location = new System.Drawing.Point(0, 0);
            this.pnlContacts.Name = "pnlContacts";
            this.pnlContacts.Size = new System.Drawing.Size(569, 476);
            this.pnlContacts.TabIndex = 1;
            // 
            // tabFiles
            // 
            this.tabFiles.Controls.Add(this.groupBox1);
            this.tabFiles.Controls.Add(this.pnlFileTransferHistory);
            this.tabFiles.Location = new System.Drawing.Point(4, 25);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiles.Size = new System.Drawing.Size(569, 601);
            this.tabFiles.TabIndex = 1;
            this.tabFiles.Text = "File Transfers";
            this.tabFiles.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdSendFile);
            this.groupBox1.Controls.Add(this.txtSendFileDescription);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmdBrowseFile);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSendFilePath);
            this.groupBox1.Location = new System.Drawing.Point(3, 436);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 161);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send a File";
            // 
            // comboUser
            // 
            this.comboUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUser.FormattingEnabled = true;
            this.comboUser.Location = new System.Drawing.Point(101, 23);
            this.comboUser.Name = "comboUser";
            this.comboUser.Size = new System.Drawing.Size(450, 26);
            this.comboUser.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "User :";
            // 
            // cmdSendFile
            // 
            this.cmdSendFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdSendFile.Location = new System.Drawing.Point(225, 127);
            this.cmdSendFile.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSendFile.Name = "cmdSendFile";
            this.cmdSendFile.Size = new System.Drawing.Size(111, 28);
            this.cmdSendFile.TabIndex = 41;
            this.cmdSendFile.Text = "Send File";
            this.cmdSendFile.UseVisualStyleBackColor = true;
            this.cmdSendFile.Click += new System.EventHandler(this.cmdSendFile_Click);
            // 
            // txtSendFileDescription
            // 
            this.txtSendFileDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendFileDescription.Location = new System.Drawing.Point(101, 85);
            this.txtSendFileDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtSendFileDescription.Multiline = true;
            this.txtSendFileDescription.Name = "txtSendFileDescription";
            this.txtSendFileDescription.Size = new System.Drawing.Size(450, 35);
            this.txtSendFileDescription.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 88);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 43;
            this.label9.Text = "Description :";
            // 
            // cmdBrowseFile
            // 
            this.cmdBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseFile.Location = new System.Drawing.Point(514, 54);
            this.cmdBrowseFile.Margin = new System.Windows.Forms.Padding(4);
            this.cmdBrowseFile.Name = "cmdBrowseFile";
            this.cmdBrowseFile.Size = new System.Drawing.Size(37, 23);
            this.cmdBrowseFile.TabIndex = 46;
            this.cmdBrowseFile.Text = "...";
            this.cmdBrowseFile.UseVisualStyleBackColor = true;
            this.cmdBrowseFile.Click += new System.EventHandler(this.cmdBrowseFile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 20);
            this.label5.TabIndex = 44;
            this.label5.Text = "File :";
            // 
            // txtSendFilePath
            // 
            this.txtSendFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendFilePath.Location = new System.Drawing.Point(101, 54);
            this.txtSendFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtSendFilePath.Name = "txtSendFilePath";
            this.txtSendFilePath.Size = new System.Drawing.Size(405, 27);
            this.txtSendFilePath.TabIndex = 45;
            // 
            // pnlFileTransferHistory
            // 
            this.pnlFileTransferHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFileTransferHistory.AutoScroll = true;
            this.pnlFileTransferHistory.Location = new System.Drawing.Point(3, 3);
            this.pnlFileTransferHistory.Name = "pnlFileTransferHistory";
            this.pnlFileTransferHistory.Size = new System.Drawing.Size(560, 427);
            this.pnlFileTransferHistory.TabIndex = 47;
            // 
            // SelectFileToSendDialog
            // 
            this.SelectFileToSendDialog.FileName = "openFileDialog1";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "HIIT";
            this.notifyIcon.Visible = true;
            // 
            // MessagesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(582, 637);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MessagesForm";
            this.Text = "Messages";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessagesForm_FormClosing);
            this.Load += new System.EventHandler(this.MessagesForm_Load);
            this.Resize += new System.EventHandler(this.MessagesForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabConfiguration.ResumeLayout(false);
            this.Settings.ResumeLayout(false);
            this.Settings.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabContacts.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabFiles.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabContacts;
        private System.Windows.Forms.TabPage tabFiles;
        private System.Windows.Forms.OpenFileDialog SelectFileToSendDialog;
        private System.Windows.Forms.Panel pnlFileTransferHistory;
        private System.Windows.Forms.Button cmdBrowseFile;
        private System.Windows.Forms.TextBox txtSendFilePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSendFileDescription;
        private System.Windows.Forms.Button cmdSendFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboUser;
        private System.Windows.Forms.TabPage tabConfiguration;
        private System.Windows.Forms.GroupBox Settings;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkHideWhenMinimized;
        private System.Windows.Forms.Button cmdBrowseDownloadFolder;
        private System.Windows.Forms.CheckBox chkShowSysTrayIcon;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSaveLogInDetails;
        private System.Windows.Forms.Button cmdLogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdContacts;
        private System.Windows.Forms.Button cmdMessages;
        private System.Windows.Forms.Button cmdViewFilesTransfers;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button cmdAddContact;
        private System.Windows.Forms.TextBox txtAddContact;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlContacts;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtIntroductionMessage;
        private System.Windows.Forms.Label label10;
    }
}