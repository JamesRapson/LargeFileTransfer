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
using System.IO;

namespace RS.FileTransfer.Client
{
    public partial class FileUploadProgressCtrl : UserControl
    {
        ConfigurationDetails _configuration = null;
        ServerCommunications _communications = null;
        Logger _logger = null;

        public FileUploadCommand FileUploadCommand { get; set; }

        public string FilePath { get; set; }

        public Action<FileUploadCommand, FileTransferCompletionStatus> NotifyUploadStatus { get; set; }

        public FileUploadProgressCtrl(ConfigurationDetails configuration, ServerCommunications communications, Logger logger)
        {
            _configuration = configuration;
            _communications = communications;
            _logger = logger;
            InitializeComponent();
        }

        private void FileUploadProgressCtrl_Load(object sender, EventArgs e)
        {
            lblFinishedStatus.Visible = false;
            lblFinishedStatus.MaximumSize = new Size(this.Width - 10, 300);
            lblFileName.Text = string.Format("Upload : {0}", FileUploadCommand.FileName);
            lblUser.Text = string.Format("To user : {0}", FileUploadCommand.DestinationUserName);
            lblDate.Text = DateTime.Now.ToString();
            lblFileDescription.Text = FileUploadCommand.Description;
        }

        public async Task StartUpload()
        {
            try
            {
                NotifyUploadStatus(FileUploadCommand, FileTransferCompletionStatus.Started);
                DateTime startTime = DateTime.Now;
                int numberOfFileParts = FileTransferHelper.GetNumberOfFileParts(FilePath, Program.FilePartSize);
                progress.Maximum = numberOfFileParts;

                int partIndex = 0;
                while (partIndex < numberOfFileParts)
                {
                    var data = FileTransferHelper.GetFilePartData(FilePath, partIndex, Program.FilePartSize);
                    await _communications.UploadFilePart(FileUploadCommand, data, partIndex);
                    partIndex++;
                    progress.Value++;
                }

                _logger.LogInformation(string.Format("File '{0}' uploaded successfully. Duration : {1}", FilePath, DateTime.Now.Subtract(startTime)));
                ShowFinishedStatus("Upload Completed", true);
                NotifyUploadStatus(FileUploadCommand, FileTransferCompletionStatus.Successful);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(string.Format("An error occurred uploading file '{0}' (Id : {1}). Error : {2}", FilePath, FileUploadCommand.FileId, ex.Message));
                ShowFinishedStatus("Upload failed. " + ex.Message, false);
            }
            NotifyUploadStatus(FileUploadCommand, FileTransferCompletionStatus.Error);
        }

        void ShowFinishedStatus(string message, bool success)
        {
            progress.Visible = false;
            cmdCancel.Visible = false;
            lblFinishedStatus.Visible = true;
            lblFinishedStatus.Text = message;
            if (success)
                lblFinishedStatus.ForeColor = Color.Green;
            else
                lblFinishedStatus.ForeColor = Color.Red;
        }

        private void cmdCancelUpload_Click(object sender, EventArgs e)
        {
            ShowFinishedStatus("Upload cancelled.", false);
            NotifyUploadStatus(FileUploadCommand, FileTransferCompletionStatus.Cancelled);
        }

        private void lblFileDescription_Click(object sender, EventArgs e)
        {

        }

    }
}

