using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd
{
    public enum CommandType
    {
        Download, ChangeDir
    }

    public static class CommandParser
    {
        public static Command ParseCommand(string input)
        {
            var parameters = input.Split(' ');

            if (parameters.Length == 1)
                return new Command(CommandType.Download, input);
            else if (parameters.Length > 2)
                throw new Exception("Could not parse input: unexpected number of parameters");

            var cmdType = parameters[0].Trim();
            if (cmdType == "chdir")
            {
                return new Command(CommandType.ChangeDir, parameters[1].Trim());
            }
            else if (cmdType == "download" || cmdType == "")
            {
                return new Command(CommandType.Download, parameters[1].Trim());
            }
            else
            {
                throw new Exception("Could not parse input: unrecognized command");
            }
        }
    }
}
