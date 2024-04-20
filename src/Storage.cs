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

        public static string SetData(string key, string value)
        {
            storage[key] = value;
            if(storage.ContainsKey(key))
            {
                Console.WriteLine("value got set : " + value);
            }
            return "+OK\r\n";
        }
    }
}
