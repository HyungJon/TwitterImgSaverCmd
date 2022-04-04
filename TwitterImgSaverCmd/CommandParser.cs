using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Commands;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd
{
    public enum CommandType
    {
        Download, ChangeDir
    }

    public static class CommandParser
    {
        public static ICommand ParseCommand(string input, IConfiguration configs)
        {
            var parameters = input.Split(' ');

            // handle some special cases
            if (DetectSingleAddressCase(parameters))
                return new DownloadCommand(input, configs);

            if (DetectChdirCase(parameters))
                return new ChdirCommand(parameters[1].Trim(), configs);

            var downloadersCandidate = HandleDownloadWithCommandCase(parameters, configs);
            if (downloadersCandidate != null) return downloadersCandidate;




            throw new InvalidOperationException("Could not parse input: unrecognized command");
        }

        private static bool DetectSingleAddressCase(string[] parameters)
        {
            // return parameters.Length == 1 && Uri.TryCreate(parameters[0], UriKind.Absolute, out Uri _);
            return parameters.Length == 1; // for now, no other use case where just one parameter is supplied
        }

        private static bool DetectChdirCase(string[] parameters)
        {
            return parameters.Length == 2 && parameters[0] == "chdir";
        }

        private static ICommand HandleDownloadWithCommandCase(string[] parameters, IConfiguration configs)
        {
            if (parameters[0] == "download")
            {
                if (parameters.Length == 2)
                    return new DownloadCommand(parameters[1].Trim(), configs);
                else
                    return new AggregateCommand(parameters.Skip(1).Select(p => new DownloadCommand(p, configs)), configs);
            }

            return null;
        }
    }
}
