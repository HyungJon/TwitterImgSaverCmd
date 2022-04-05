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
            var parameters = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // below logic could be improved
            // but I also want to avoid having to check the first element multiple times
            // once to check if it specified command, second if true to check the type of command

            // handle some special cases
            if (DetectSingleAddressCase(parameters))
                return new DownloadCommand(input, configs);

            if (DetectChdirCase(parameters))
                return new ChdirCommand(parameters[1].Trim(), configs);

            var downloadersCandidate = HandleDownloadWithCommandCase(parameters, configs);
            if (downloadersCandidate != null) return downloadersCandidate;

            // barring further complications in logic, anything beyond this can be treated as request to download from all given parameters
            downloadersCandidate = HandleMultipleDownloadsWithoutCommandCase(parameters, configs);
            if (downloadersCandidate != null) return downloadersCandidate;

            throw new InvalidOperationException("Could not parse input: unrecognized command");
        }

        private static (CommandType, IList<string>) GetCommandTypeAndParams(string[] parameters)
        {
            // may need update to below logic
            switch (parameters[0])
            {
                case "chdir":
                    return (CommandType.ChangeDir, parameters.Skip(1).ToList());
                case "download":
                    return (CommandType.Download, parameters.Skip(1).ToList());
                default:
                    return (CommandType.ChangeDir, parameters.ToList());
            }
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

        private static ICommand HandleMultipleDownloadsWithoutCommandCase(string[] parameters, IConfiguration configs)
            => new AggregateCommand(parameters.Select(p => new DownloadCommand(p, configs)), configs);
    }
}
