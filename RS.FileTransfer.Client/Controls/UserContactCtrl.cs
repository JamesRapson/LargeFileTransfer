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
    public partial class UserContactCtrl : UserControl
    {
        public UserConnectionModel UserConnection { get; set; }

        public Action<UserConnectionModel> ConnectionSelected { get; set; }

        public UserContactCtrl()
        {
            InitializeComponent();
        }

        private void UserContactCtrl_Load(object sender, EventArgs e)
        {
            lblUserName.Text = UserConnection.UserName;
            if (UserConnection.Status == UserConnectionStatusEnum.RequestedByMe)
            {
                lblPendingApproval.Visible = true;
                lblPendingApproval.Text = "Connection Requested";
            }
            else if (UserConnection.Status == UserConnectionStatusEnum.RequestedByOther)
            {
                lblPendingApproval.Visible = true;
                lblPendingApproval.Text = "Wants to connect";
            }
            else if (UserConnection.Status == UserConnectionStatusEnum.Rejected)
            {
                lblPendingApproval.Visible = true;
                lblPendingApproval.Text = "Rejected";
            }
            else if (UserConnection.Status == UserConnectionStatusEnum.Blocked)
            {
                lblPendingApproval.Visible = true;
                lblPendingApproval.Text = "Blocked";
            }
            else
                lblPendingApproval.Visible = false;
        }

        private void lblUserName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((UserConnection.Status == UserConnectionStatusEnum.Approved) || (UserConnection.Status == UserConnectionStatusEnum.RequestedByOther))
                ConnectionSelected(UserConnection);
        }
    }
}
