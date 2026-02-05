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
            case CommandType.ChangeDir:
                if (parameters.Count > 2)
                    throw new InvalidOperationException("Could not parse input: incorrect number of parameters");
                // TODO: once saving to multiple directories is implemented, remove this restriction
                return new ChdirCommand(parameters[0], configs);
            case CommandType.AddShortcut:
                if (parameters.Count != 2)
                    throw new InvalidOperationException("Could not parse input: incorrect number of parameters");
                return new AddShortcutCommand(parameters[0].ToLower(), parameters[1], configs);
            case CommandType.Download:
                return ProcessDownloadCommand(parameters, configs);
                // return new AggregateCommand(parameters.Distinct().Select(p => new DownloadCommand(p, configs)), configs);
            default:
                throw new InvalidOperationException("Could not parse input: unrecognized command");
        }
    }

    private static (CommandType, IList<string>) GetCommandTypeAndParams(string[] parameters)
    {
        // this logic was implemented in case the parsing logic is updated so that there can be input without explicit command that isn't download
        // in which case the method that receives this should need to further parse the input to figure out what the command is
        return parameters[0].ToLower() switch
        {
            "chdir" => (CommandType.ChangeDir, parameters.Skip(1).ToList()),
            "download" => (CommandType.Download, parameters.Skip(1).ToList()),
            "addshortcut" => (CommandType.AddShortcut, parameters.Skip(1).ToList()),
            _ => (InferCommandType(parameters.ToList()), parameters.ToList())
        };
    }

    private static CommandType InferCommandType(IList<string> parameters)
    {
        return CommandType.Download; // Temporary code: just assume download
    }

    private static ICommand ProcessDownloadCommand(IList<string> parameters, IConfiguration configs)
    {
        string? filenameOverride = null;

        if (!Uri.TryCreate(parameters[0], UriKind.Absolute, out _))
        {
            filenameOverride = parameters[0];
            parameters.RemoveAt(0);
        }

        if (configs.SavePathShortcuts.TryGetValue(parameters.Last(), out var savePathOverride))
        {
            parameters.RemoveAt(parameters.Count - 1);
        }
        
        if (!parameters.Any())
        {
            throw new InvalidOperationException("No links to save provided");
        }

        if (parameters.Count == 1)
        {
            return new DownloadCommand(parameters[0], configs, filenameOverride, savePathOverride);
        }

        var downloadCommands = parameters.Select((addr, i) =>
            new DownloadCommand(addr, configs, filenameOverride + '_' + (i + 1), savePathOverride)).ToList();
        return new AggregateCommand(downloadCommands, configs);
    }

    private static (string?, IList<string>) ParseSourcesAndShortcut(IList<string> parameters, IConfiguration configs)
    {
        var last = parameters.Last();
                
        return configs.SavePathShortcuts.TryGetValue(last, out var shortcut)
            ? (shortcut, parameters.SkipLast(1).ToList())
            : (null, parameters);
    }
}