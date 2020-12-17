using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS.FileTransfer.Client
{
    static class Program
    {
        public static readonly int FilePartSize = 10000000;

        static ConfigurationDetails _configuration = null;
        static ServerCommunications _communications = null;
        static Logger _logger = null;

        [STAThread]
        static void Main()
        {
            _logger = new Logger();
            try
            {
                _configuration = new ConfigurationDetails();
                _communications = new ServerCommunications(_configuration);
            }
            catch (Exception ex)
            {
                string msg = string.Format("An error occurred loading configuration. {0}", ex.Message);
                _logger.LogError(msg);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MessagesForm(_configuration, _communications, _logger));
        }
    }
}
