namespace RS.FileTransfer.Client
{
    partial class UserContactCtrl
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
            this.lblPendingApproval = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblPendingApproval
            // 
            this.lblPendingApproval.AutoSize = true;
            this.lblPendingApproval.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingApproval.Location = new System.Drawing.Point(6, 31);
            this.lblPendingApproval.Name = "lblPendingApproval";
            this.lblPendingApproval.Size = new System.Drawing.Size(168, 17);
            this.lblPendingApproval.TabIndex = 1;
            this.lblPendingApproval.Text = "Connection Requested";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(5, 2);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(112, 20);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.TabStop = true;
            this.lblUserName.Text = "User Name";
            this.lblUserName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUserName_LinkClicked);
            // 
            // UserContactCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblPendingApproval);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserContactCtrl";
            this.Size = new System.Drawing.Size(396, 57);
            this.Load += new System.EventHandler(this.UserContactCtrl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPendingApproval;
        private System.Windows.Forms.LinkLabel lblUserName;
    }
}
