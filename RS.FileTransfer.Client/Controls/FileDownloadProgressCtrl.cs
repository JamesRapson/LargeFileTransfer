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
using System.Diagnostics;

namespace RS.FileTransfer.Client
{
    public partial class FileDownloadProgressCtrl : UserControl
    {
        ConfigurationDetails _configuration = null;
        ServerCommunications _communications = null;
        Logger _logger = null;
        FileTransferCompletionStatus _downloadStatus = FileTransferCompletionStatus.NotStarted;
        string _destinationPath = null;

        public FileTransferCompletionStatus DownloadStatus
        {
            get { return _downloadStatus; }
        }

        public string DestinationPath
        {
            get { return _destinationPath; }
        }

        public FileDownloadCommand FileDownloadCommand { get; set; }

        public NotifyIcon NotifyIcon { get; set; }

        public Action<FileDownloadCommand, FileTransferCompletionStatus> NotifyDownloadStatus { get; set; }

        public bool CancelDownload { get; set; }

        public FileDownloadProgressCtrl(ConfigurationDetails configuration, ServerCommunications communications, Logger logger)
        {
            _configuration = configuration;
            _communications = communications;
            _logger = logger;
            InitializeComponent();
            CancelDownload = false;
        }

        string GetFileSizeText(long lengthBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (lengthBytes >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                lengthBytes = lengthBytes / 1024;
            }
            return  String.Format("{0:0.##} {1}", lengthBytes, sizes[order]);
        }

        private void FileDownloadProgressCtrl_Load(object sender, EventArgs e)
        {
            lblFileName.Text = string.Format("Download : {0}", FileDownloadCommand.FileName);
            lblUser.Text = string.Format("From user : {0}", FileDownloadCommand.SourceUserName);
            lblDate.Text = DateTime.Now.ToString();
            lblFileDescription.Text = FileDownloadCommand.Description;
            lblFileSize.Text = GetFileSizeText(FileDownloadCommand.Size);
            SetDownloadStatus(FileTransferCompletionStatus.NotStarted);
        }
        

        void SetDownloadStatus(FileTransferCompletionStatus status)
        {
            _downloadStatus = status;
            if (_downloadStatus == FileTransferCompletionStatus.NotStarted)
            {
                cmdStartCancel.Visible = true;
                cmdStartCancel.Text = "Start";
                lblFinishedStatus.Visible = false;
                progress.Visible = false;
            }
            else if (_downloadStatus == FileTransferCompletionStatus.Started)
            {
                cmdStartCancel.Text = "Cancel";
                lblFinishedStatus.Visible = false;
                progress.Visible = true;
            }
            else if (_downloadStatus == FileTransferCompletionStatus.Successful)
            {
                cmdStartCancel.Visible = false;
                lblFinishedStatus.Visible = true;
                progress.Visible = false;
                lblFinishedStatus.Location = cmdStartCancel.Location;
            }
            else if (_downloadStatus == FileTransferCompletionStatus.Error)
            {
                cmdStartCancel.Visible = true;
                cmdStartCancel.Text = "Start";
                lblFinishedStatus.Visible = true;
                progress.Visible = false;
            }
            else if (_downloadStatus == FileTransferCompletionStatus.Cancelled)
            {
                cmdStartCancel.Visible = true;
                cmdStartCancel.Text = "Start";
                lblFinishedStatus.Visible = true;
                progress.Visible = false;
            }

            NotifyDownloadStatus(FileDownloadCommand, _downloadStatus);
        }

        async Task StartDownload()
        {
            try
            {

                SetDownloadStatus(FileTransferCompletionStatus.Started);

                lblFinishedStatus.Visible = false;
                DateTime startTime = DateTime.Now;
                
                var filePartPaths = new List<string>();
                string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HIIT", FileDownloadCommand.FileId.ToString());
                if (!Directory.Exists(downloadFolder))
                    Directory.CreateDirectory(downloadFolder);

                progress.Value = 0;
                progress.Maximum = FileDownloadCommand.NumberParts;
                for (int i = 0; i < FileDownloadCommand.NumberParts; i++)
                {
                    if (CancelDownload)
                        break;
                    
                    string tempFile = Path.Combine(downloadFolder, i + ".data");
                    await _communications.DownloadFilePart(FileDownloadCommand, tempFile, i);
                    filePartPaths.Add(tempFile);
                    progress.Value++;
                }

                if (CancelDownload)
                {
                    _logger.LogInformation(string.Format("File '{0}' download cancelled.", _destinationPath));

                    // clean up the downloaded files.
                    filePartPaths.ForEach(x => File.Delete(x));
                    Directory.Delete(downloadFolder, true);

                    ShowFinishedStatus("Download Cancelled", false);
                    SetDownloadStatus(FileTransferCompletionStatus.Cancelled);
                }
                else
                {
                    _destinationPath = Path.Combine(_configuration.DownloadFolder, FileDownloadCommand.FileName);
                    await FileTransferHelper.CombineFileParts(_destinationPath, filePartPaths);
                    _logger.LogInformation(string.Format("File '{0}' downloaded successfully. Duration : {1}", _destinationPath, DateTime.Now.Subtract(startTime)));

                    // clean up the downloaded files.
                    filePartPaths.ForEach(x => File.Delete(x));
                    Directory.Delete(downloadFolder, true);

                    
                    ShowFinishedStatus("Download Completed", true);
                    SetDownloadStatus(FileTransferCompletionStatus.Successful);
                }

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(string.Format("An error occurred downloading file '{0}' (Id : {1}). Error : {2}", FileDownloadCommand.FileName, FileDownloadCommand.FileId, ex.Message));
                ShowFinishedStatus("Download failed. " + ex.Message, false);
            }

            SetDownloadStatus(FileTransferCompletionStatus.Error);
        }

        void ShowFinishedStatus(string message, bool success)
        {
            lblFinishedStatus.Text = message;
            lblFinishedStatus.Visible = true;
            lblFinishedStatus.AutoSize = true;
            
            if (success)
                lblFinishedStatus.ForeColor = Color.Green;
            else
                lblFinishedStatus.ForeColor = Color.Red;
        }

        private async void cmdCancelDownload_Click(object sender, EventArgs e)
        {
            if ((_downloadStatus == FileTransferCompletionStatus.NotStarted) || 
                (_downloadStatus == FileTransferCompletionStatus.Cancelled) ||
                (_downloadStatus == FileTransferCompletionStatus.Error))
            {
                CancelDownload = false;
                await StartDownload();
            }
            else if (_downloadStatus == FileTransferCompletionStatus.Started)
            {
                CancelDownload = true;
            }
        }

        private void lblFileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_downloadStatus == FileTransferCompletionStatus.Successful)
            {
                Process.Start("explorer.exe", "/select, \"" + DestinationPath + "\"");
            }

        }
    }
}
