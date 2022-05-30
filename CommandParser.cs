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
        Download, ChangeDir,
        // consider:
        // addDir, rmDir
    }

    public static class CommandParser
    {
        public static ICommand ParseCommand(string input, IConfiguration configs)
        {
            var (commandType, parameters) = GetCommandTypeAndParams(input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            if (!parameters.Any()) throw new InvalidOperationException("Unexpected failure: no parameter entered");

            switch (commandType)
            {
                case null:
                    return ParseNonExplicitInput(parameters, configs);
                case CommandType.ChangeDir:
                    if (parameters.Count > 2)
                        throw new InvalidOperationException("Could not parse input: incorrect number of parameters");
                        // actually, could consider saving to multiple locations, using add/remove/clear directory command instead
                        // also could just ignore the rest 
                    else
                        return new ChdirCommand(parameters[0], configs);
                case CommandType.Download:
                    return new AggregateCommand(parameters.Distinct().Select(p => new DownloadCommand(p, configs)), configs);
            }

            throw new InvalidOperationException("Could not parse input: unrecognized command");
        }

        private static (CommandType?, IList<string>) GetCommandTypeAndParams(string[] parameters)
        {
            // this logic was implemented in case the parsing logic is updated so that there can be input without explicit command that isn't download
            // in which case the method that receives this should need to further parse the input to figure out what the command is
            return parameters[0] switch
            {
                "chdir" => (CommandType.ChangeDir, parameters.Skip(1).ToList()),
                "download" => (CommandType.Download, parameters.Skip(1).ToList()),
                _ => (null, parameters.ToList())
            };
        }

        private static ICommand ParseNonExplicitInput(IList<string> parameters, IConfiguration configs)
        {
            // temp code: as commented above, this may need to be updated in the future if more commands are allowed to be non-explicit
            // C# can distinguish between web link and file path using new Uri(path).IsFile, consider enabling non-explicit chdir
            return new AggregateCommand(parameters.Distinct().Select(p => new DownloadCommand(p, configs)), configs);
        }
    }
}
