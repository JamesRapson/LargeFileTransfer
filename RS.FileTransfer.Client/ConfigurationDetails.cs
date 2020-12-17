using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Client
{
    public class ConfigurationDetails
    {
        static string RegistryKeyPath = "Software\\HIIT";

        public string ServerBaseUrl { get; set; }

        public Guid DeviceId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DownloadFolder { get; set; }

        public bool RememberLoginDetails { get; set; }

        public bool ShowSysTrayIcon { get; set; }

        public bool HideWhenMinimized { get; set; }

        public bool AutomaticLogin { get; set; }

        public ConfigurationDetails()
        {
            RememberLoginDetails = true;
            ShowSysTrayIcon = true;
            HideWhenMinimized = true;
            AutomaticLogin = false;

            Load();
            
            if (String.IsNullOrEmpty(ServerBaseUrl))
                ServerBaseUrl = "http://localhost:8080";
            if (String.IsNullOrEmpty(DownloadFolder))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                DownloadFolder = Path.Combine(path, "HIIT Downloads");
            }
            if (DeviceId == Guid.Empty)
                DeviceId = Guid.NewGuid();

#if DEBUG
            DeviceId = Guid.NewGuid();
#endif

            Save();
        }

        
        public void Save()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
            string _userName = RememberLoginDetails ? UserName : "";
            string _passsword = RememberLoginDetails ? Password : "";

            string configString = RegistryKeyPath + "." + DeviceId.ToString("N") + "." + _userName + "." + _passsword + "." + DownloadFolder + "." + RememberLoginDetails.ToString() + "." + ShowSysTrayIcon.ToString() + "." + HideWhenMinimized.ToString() + "." + AutomaticLogin.ToString();
            string encryptedConfigStr = EncryptionHelper.Encrypt(configString);
            key.SetValue("config", encryptedConfigStr);
        }

        public void Load()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            if (key == null)
                return;

            object obj = key.GetValue("config", null);
            string encryptedConfigStr = (string)obj;
            if (String.IsNullOrEmpty(encryptedConfigStr))
                return;

            string configString = EncryptionHelper.Decrypt(encryptedConfigStr);
            string[] parts = configString.Split(new char[] { '.' } );
            if (parts.Length < 9)
                return;

            RegistryKeyPath = parts[0];
            DeviceId = new Guid(parts[1]);
            UserName = parts[2];
            Password = parts[3];
            DownloadFolder = parts[4];
            RememberLoginDetails = bool.Parse(parts[5]);
            ShowSysTrayIcon = bool.Parse(parts[6]);
            HideWhenMinimized = bool.Parse(parts[7]);
            AutomaticLogin = bool.Parse(parts[8]);
        }
    }


}
