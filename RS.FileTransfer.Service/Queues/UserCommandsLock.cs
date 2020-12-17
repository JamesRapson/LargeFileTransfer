using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service.Queues
{
    public static class UserCommandsLock
    {
        static Dictionary<string, TaskCompletionSource<bool>> Locks { get; set; }

        static UserCommandsLock()
        {
            Locks = new Dictionary<string, TaskCompletionSource<bool>>();
        }

        public static async Task Wait(string userName, string deviceId)
        {
            string key = userName.ToLower() + "--" + deviceId.ToLower();
            if (Locks.ContainsKey(key))
                Locks.Remove(key);

            Locks.Add(key, new TaskCompletionSource<bool>());
            await Locks[key].Task;
            await Task.Delay(1);
        }

        public static void Wake(string userName)
        {
            string keyStart = userName.ToLower() + "--";
            var keys = Locks.Keys.Where(x => x.StartsWith(keyStart)).ToList();
            foreach(var key in keys)
                Locks[key].TrySetResult(true);
        }
    }

    /*
    public static class UserCommandsLock
    {
        static Dictionary<string, SemaphoreSlim> Locks { get; set; }

        static UserCommandsLock()
        {
            Locks = new Dictionary<string, SemaphoreSlim>();
        }

        public static async Task Wait(string userName)
        {
            if (!Locks.ContainsKey(userName))
                Locks.Add(userName, new SemaphoreSlim(0, 1));

            await Locks[userName].WaitAsync(60000);

        }

        public static void Wake(string userName)
        {
            if (Locks.ContainsKey(userName))
            {
                Locks[userName].Release();
            }
        }
    }
    */
}
