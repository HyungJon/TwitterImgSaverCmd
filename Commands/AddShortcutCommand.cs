using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands;

public class AddShortcutCommand : Command
{
    private readonly IConfiguration _configs;
    private readonly string _keyword;
    private readonly string _path;
    
    public AddShortcutCommand(string keyword, string path, IConfiguration configs)
    {
        _keyword = keyword;
        _path = path;
        _configs = configs;
    }

    public override Task PerformAsync()
    {
        if (!Directory.Exists(_path))
        {
            throw new InvalidOperationException($"Directory {_path} does not exist");
        }
        
        Console.WriteLine($"  Adding shortcut to folder {_path} as keyword {_keyword}");
        
        _configs.SavePathShortcuts.Add(_keyword, _path);
        _configs.SaveConfigs();
        return Task.CompletedTask;
    }
}