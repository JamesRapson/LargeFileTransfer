namespace RS.FileTransfer.Client
{
    partial class FileUploadProgressCtrl
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
            this.lblFileDescription = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblFinishedStatus = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFileDescription
            // 
            this.lblFileDescription.AutoSize = true;
            this.lblFileDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileDescription.Location = new System.Drawing.Point(5, 53);
            this.lblFileDescription.MaximumSize = new System.Drawing.Size(0, 30);
            this.lblFileDescription.Name = "lblFileDescription";
            this.lblFileDescription.Size = new System.Drawing.Size(114, 17);
            this.lblFileDescription.TabIndex = 1;
            this.lblFileDescription.Text = "File Description";
            this.lblFileDescription.Click += new System.EventHandler(this.lblFileDescription_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(4, 28);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(169, 17);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Destination User Name";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(348, 88);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(72, 26);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancelUpload_Click);
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(3, 88);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(339, 25);
            this.progress.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(303, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(117, 17);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinishedStatus
            // 
            this.lblFinishedStatus.AutoSize = true;
            this.lblFinishedStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinishedStatus.Location = new System.Drawing.Point(5, 88);
            this.lblFinishedStatus.MaximumSize = new System.Drawing.Size(0, 30);
            this.lblFinishedStatus.MinimumSize = new System.Drawing.Size(120, 14);
            this.lblFinishedStatus.Name = "lblFinishedStatus";
            this.lblFinishedStatus.Size = new System.Drawing.Size(134, 18);
            this.lblFinishedStatus.TabIndex = 13;
            this.lblFinishedStatus.Text = "Finished Status";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(4, 3);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(217, 20);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "Operation - File Name";
            // 
            // FileUploadProgressCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblFinishedStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblFileDescription);
            this.Controls.Add(this.lblFileName);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(2, 101);
            this.Name = "FileUploadProgressCtrl";
            this.Size = new System.Drawing.Size(423, 118);
            this.Load += new System.EventHandler(this.FileUploadProgressCtrl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileDescription;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblFinishedStatus;
        private System.Windows.Forms.Label lblFileName;
    }
}
