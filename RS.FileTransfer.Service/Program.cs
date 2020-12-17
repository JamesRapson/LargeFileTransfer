using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service
{
    class Program
    {
        public static UserManager UserManager { get; set; }
        public static ConnectionsManager ConnectionsManager { get; set; }
        public static ConnectionRequestsManager ConnectionRequestsManager { get; set; }

        static void Main(string[] args)
        {
            UserManager = new UserManager("users2.txt");
            ConnectionsManager = new ConnectionsManager();
            ConnectionRequestsManager = new ConnectionRequestsManager();

            UserManager.Add(new User()
            {
                UserName = "James",
                Password = "password",
                Role = "Administrators",
                Enabled = true
            });
            UserManager.Add(new User()
            {
                UserName = "Sarah",
                Password = "password",
                Role = "User",
                Enabled = true
            });
            UserManager.Add(new User()
            {
                UserName = "Dana",
                Password = "password",
                Role = "User",
                Enabled = true
            });
            UserManager.Add(new User()
            {
                UserName = "Elyse",
                Password = "password",
                Role = "User",
                Enabled = true
            });

            ConnectionsManager.Add("James", "Sarah");
            ConnectionRequestsManager.RequestConnection("James", "Dana", "Hello I want to chat");

            string baseUri = "http://localhost:8080";

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }
    }
}
