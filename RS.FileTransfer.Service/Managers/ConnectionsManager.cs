using RS.FileTransfer.Common.Models;
using RS.FileTransfer.Service.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service
{
    public class Connection
    {
        public string UserName1 { get; set; }

        public string UserName2 { get; set; }
    }

    public class ConnectionsManager
    {
        List<Connection> _connections = new List<Connection>();

        public void Add(string userName1, string userName2)
        {
            if (ConnectionExists(userName1, userName1))
                return;

            _connections.Add(new Connection()
                {
                    UserName1 = userName1,
                    UserName2 = userName2
                });
        }

        public bool ConnectionExists(string userName1, string userName2)
        {
            string _userName1 = userName1.ToLower();
            string _userName2 = userName2.ToLower();
            if (_connections.Exists(x =>
                    (
                        ((x.UserName1.ToLower() == _userName1) && (x.UserName2.ToLower() == _userName2)) ||
                        ((x.UserName1.ToLower() == _userName2) && (x.UserName2.ToLower() == _userName1))
                    )
                )
            )
                return true;
            else
                return false;
        }

        public List<string> GetContactsForUser(string userName)
        {
            string _userName = userName.ToLower();
            var list = _connections.Where(x => (x.UserName1.ToLower() == _userName) || (x.UserName2.ToLower() == _userName)).Select(x => { return x.UserName1.ToLower() == _userName ? x.UserName2 : x.UserName1; }).Distinct().ToList();
            list.Remove(userName);
            return list.ToList();
        }

        public void Load()
        {

        }

        public void Save()
        {

        }

    }
}
