﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_redis.src
{
    public enum CommandType
    {
        PING,
        ECHO,
        ERROR
    }
    public class RESP
    {

        public string ParseCommand(byte[] bytes)
        {
            string input = Encoding.UTF8.GetString(bytes);
            //Console.WriteLine("input from client : "+input);
            var command = input.Split("\r\n").ToList() ?? new();
            return GetCommandResult(command);
        }

        private string GetCommandResult(List<string> commandSplit)
        {
            var responseString = "";
            if(commandSplit.Count > 2)
            {
                var commandType = GetCommandType(commandSplit);
                Console.WriteLine($"got command type : {commandType}");
                switch (commandType)
                {
                    case CommandType.PING:
                        return "+PONG\r\n";
                    case CommandType.ECHO:
                        return $"${commandSplit[4].Length}\r\n{commandSplit[4]}\r\n";
                    case CommandType.ERROR:
                    default:
                        return "";
                }
            }

            return responseString;
            
        }

        private CommandType GetCommandType(List<string> commandSplit)
        {
            var commandList = new List<string>()
            {
                "PING","ECHO"
            };

            foreach (var commandType in commandSplit)
            {
                if (commandList.Contains(commandType.ToUpper()))
                {
                    Enum.TryParse(commandType, out CommandType result);

                    return result;
                }
            }

            return CommandType.ERROR;
        }
    }
}
