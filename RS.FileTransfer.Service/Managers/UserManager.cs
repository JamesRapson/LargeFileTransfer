using Newtonsoft.Json;
using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service
{
    public class User
    {
        public User()
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Enabled { get; set; }

        public DateTime RegistedDate { get; set; }

        public List<UserConnectionModel> Connections { get; set; }

        public bool CheckPassword(string password)
        {
            return (Password == password);
        }

    }

    public class UserManager
    {
        string _filePath = null;
        List<User> _users = new List<User>();

        public UserManager(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        public void Add(User user)
        {
            _users.Add(user);
            Save();
        }

        public User Get(string name)
        {
            string _name = name.ToLower();
            var user = _users.FirstOrDefault(x => x.UserName.ToLower() == _name);
            return user;
        }

        void Load()
        {
            _users.Clear();
            if (!File.Exists(_filePath))
                return;

            string jsonStr = File.ReadAllText(_filePath, Encoding.UTF8);
            List<User> deserializedUsers = JsonConvert.DeserializeObject<List<User>>(jsonStr);
        }

        public void Save()
        {
            File.Delete(_filePath);
            string jsonStr = JsonConvert.SerializeObject(_users);
            File.WriteAllText(_filePath, jsonStr, Encoding.UTF8);
        }
    }
}
