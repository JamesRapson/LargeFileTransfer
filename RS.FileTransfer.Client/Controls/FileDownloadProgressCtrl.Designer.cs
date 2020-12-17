namespace RS.FileTransfer.Client
{
    partial class FileDownloadProgressCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDate = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.cmdStartCancel = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblFileDescription = new System.Windows.Forms.Label();
            this.lblFinishedStatus = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.LinkLabel();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(306, 31);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(107, 17);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(81, 84);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(332, 28);
            this.progress.TabIndex = 10;
            // 
            // cmdStartCancel
            // 
            this.cmdStartCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStartCancel.Location = new System.Drawing.Point(3, 84);
            this.cmdStartCancel.Name = "cmdStartCancel";
            this.cmdStartCancel.Size = new System.Drawing.Size(72, 29);
            this.cmdStartCancel.TabIndex = 9;
            this.cmdStartCancel.Text = "Start";
            this.cmdStartCancel.UseVisualStyleBackColor = true;
            this.cmdStartCancel.Click += new System.EventHandler(this.cmdCancelDownload_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(5, 31);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(169, 17);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "Destination User Name";
            // 
            // lblFileDescription
            // 
            this.lblFileDescription.AutoSize = true;
            this.lblFileDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileDescription.Location = new System.Drawing.Point(5, 55);
            this.lblFileDescription.MaximumSize = new System.Drawing.Size(0, 30);
            this.lblFileDescription.Name = "lblFileDescription";
            this.lblFileDescription.Size = new System.Drawing.Size(114, 17);
            this.lblFileDescription.TabIndex = 7;
            this.lblFileDescription.Text = "File Description";
            // 
            // lblFinishedStatus
            // 
            this.lblFinishedStatus.AutoSize = true;
            this.lblFinishedStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinishedStatus.Location = new System.Drawing.Point(81, 90);
            this.lblFinishedStatus.MaximumSize = new System.Drawing.Size(0, 30);
            this.lblFinishedStatus.MinimumSize = new System.Drawing.Size(120, 12);
            this.lblFinishedStatus.Name = "lblFinishedStatus";
            this.lblFinishedStatus.Size = new System.Drawing.Size(134, 18);
            this.lblFinishedStatus.TabIndex = 12;
            this.lblFinishedStatus.Text = "Finished Status";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(3, 3);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(213, 20);
            this.lblFileName.TabIndex = 13;
            this.lblFileName.TabStop = true;
            this.lblFileName.Text = "Download - FileName";
            this.lblFileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFileName_LinkClicked);
            // 
            // lblFileSize
            // 
            this.lblFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileSize.Location = new System.Drawing.Point(335, 6);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(78, 17);
            this.lblFileSize.TabIndex = 14;
            this.lblFileSize.Text = "Size";
            this.lblFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FileDownloadProgressCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblFileSize);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblFinishedStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.cmdStartCancel);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblFileDescription);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FileDownloadProgressCtrl";
            this.Size = new System.Drawing.Size(423, 118);
            this.Load += new System.EventHandler(this.FileDownloadProgressCtrl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Button cmdStartCancel;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblFileDescription;
        private System.Windows.Forms.Label lblFinishedStatus;
        private System.Windows.Forms.LinkLabel lblFileName;
        private System.Windows.Forms.Label lblFileSize;
    }
}
