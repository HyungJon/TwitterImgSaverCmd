using Microsoft.Extensions.Logging;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands;

public class AddShortcutCommand : Command
{
    private readonly IConfiguration _configs;
    private readonly string _keyword;
    private readonly string _path;
    private readonly ILogger<AddShortcutCommand> _logger;
    
    public AddShortcutCommand(string keyword, string path, IConfiguration configs, ILogger<AddShortcutCommand> logger)
    {
        _keyword = keyword;
        _path = path;
        _configs = configs;
        _logger = logger;
    }

    public override Task PerformAsync()
    {
        if (!Directory.Exists(_path))
        {
            throw new InvalidOperationException($"Directory {_path} does not exist");
        }

        // Console.WriteLine($"  Adding shortcut to folder {_path} as keyword {_keyword}");\
        _logger.LogInformation("Adding shortcut to folder {Path} as keyword {Keyword}", _path, _keyword);
        
        _configs.SavePathShortcuts.Add(_keyword, _path);
        _configs.SaveConfigs();
        return Task.CompletedTask;
    }
}