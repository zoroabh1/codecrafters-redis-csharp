using System;
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
            Console.WriteLine("input from client : "+input);
            var command = input.Split("\r\n").ToList() ?? new();
            return GetCommandResult(command);
        }

        private string GetCommandResult(List<string> commandSplit)
        {
            List<String> response = new List<String>();
            var commandType = GetCommandType(commandSplit);
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

        private CommandType GetCommandType(List<string> commandSplit)
        {
            var commandList = Enum.GetValues(typeof(CommandType)).Cast<CommandType>().Select(v => v.ToString()).ToList();

            foreach ( var commandType in commandSplit)
            {
                if (commandList.Contains(commandType))
                {
                    Enum.TryParse(commandType, out CommandType result);

                    return result;
                }
            }

            return CommandType.ERROR;
        }
    }
}
