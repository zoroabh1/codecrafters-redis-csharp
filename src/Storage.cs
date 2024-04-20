using codecrafters_redis.src.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_redis.src
{
    public static class Storage
    {

        private static Dictionary<string, string> storage = new();

        public static string Getdata(string key)
        {
            if (storage.ContainsKey(key))
            {
                var value = storage[key];
                return $"${value.Length}\r\n{value}\r\n";
            }
            else
            {
                return "$-1\r\n";
            }
        }

        public static string SetData(SetCommand command)
        {
            storage[command.Key] = command.Value;
            if (command.Expiry != 0)
            {
                Task.Run(async () => await DeleteData(command.Key,command.Expiry));
            }
            return "+OK\r\n";
        }

        private static async Task DeleteData(string key, int expiryInMilliseconds=0)
        {
            await Task.Delay(expiryInMilliseconds);
            storage.Remove(key);
        }
    }
}
