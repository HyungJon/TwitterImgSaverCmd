using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Commands;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd;

public enum CommandType
{
    Download, ChangeDir, AddShortcut,
    // consider:
    // addDir, rmDir
}

public static class CommandParser
{
    public static ICommand ParseCommand(string input, IConfiguration configs)
    {
        var arguments = input.Split('"')
            .Select((element, index) => index % 2 == 0  // If even index
                ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                : new[] { element })  // Keep the entire item
            .SelectMany(element => element).ToArray();
            
        var (commandType, parameters) = GetCommandTypeAndParams(arguments);

        if (!parameters.Any()) throw new InvalidOperationException("Unexpected failure: no parameter entered");

        switch (commandType)
        {
            case null:
                return ParseNonExplicitInput(parameters, configs);
            case CommandType.ChangeDir:
                if (parameters.Count > 2)
                    throw new InvalidOperationException("Could not parse input: incorrect number of parameters");
                // TODO: once saving to multiple directories is implemented, remove this restriction
                return new ChdirCommand(parameters[0], configs);
            case CommandType.Download:
                return new AggregateCommand(parameters.Distinct().Select(p => new DownloadCommand(p, configs)), configs);
            case CommandType.AddShortcut:
                if (parameters.Count != 2)
                    throw new InvalidOperationException("Could not parse input: incorrect number of parameters");
                return new AddShortcutCommand(parameters[0].ToLower(), parameters[1], configs);
        }

        throw new InvalidOperationException("Could not parse input: unrecognized command");
    }

    private static (CommandType?, IList<string>) GetCommandTypeAndParams(string[] parameters)
    {
        // this logic was implemented in case the parsing logic is updated so that there can be input without explicit command that isn't download
        // in which case the method that receives this should need to further parse the input to figure out what the command is
        return parameters[0].ToLower() switch
        {
            "chdir" => (CommandType.ChangeDir, parameters.Skip(1).ToList()),
            "download" => (CommandType.Download, parameters.Skip(1).ToList()),
            "addshortcut" => (CommandType.AddShortcut, parameters.Skip(1).ToList()),
            _ => (null, parameters.ToList())
        };
    }

    private static ICommand ParseNonExplicitInput(IList<string> parameters, IConfiguration configs)
    {
        // temp code: as commented above, this may need to be updated in the future if more commands are allowed to be non-explicit
        // C# can distinguish between web link and file path using new Uri(path).IsFile, consider enabling non-explicit chdir
        
        // TODO: due to this being the most-used input format, most functionality is revolving around this
        // TODO: simplifying the parsing has become a tech debt

        if (!Uri.TryCreate(parameters[0], UriKind.Absolute, out _))
        {
            var filename = parameters[0];

            var (savePath, imageLinks) = ParseSourcesAndShortcut(parameters.Skip(1).ToList(), configs);
                
            if (!imageLinks.Any())
            {
                throw new InvalidOperationException("No links to save provided");
            }

            if (imageLinks.Count == 1)
            {
                return new DownloadCommand(imageLinks[0], configs, filename, savePath);
            }

            var downloadCommands = new List<DownloadCommand>();
            for (var i = 0; i < imageLinks.Count; i++)
            {
                // note: in the old implementation where the tweet attachments list could be directly derived from a tweet metadata,
                // the index assignment was done in TweetImagesDownloader
                // this is inevitable now that the old method cannot be used any more, but it's possible that moving index assignment logic
                // further down (e.g. create a new Command subclass) may be preferable
                downloadCommands.Add(new DownloadCommand(imageLinks[i], configs, filename + '_' + (i + 1), savePath));
            }
            return new AggregateCommand(downloadCommands, configs);

        }

        return new AggregateCommand(parameters.Distinct().Select(p => new DownloadCommand(p, configs)), configs);
    }

    private static (string?, IList<string>) ParseSourcesAndShortcut(IList<string> parameters, IConfiguration configs)
    {
        var last = parameters.Last();
                
        return configs.SavePathShortcuts.TryGetValue(last, out var shortcut)
            ? (shortcut, parameters.SkipLast(1).ToList())
            : (null, parameters);
    }
}