using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands;

public class AddShortcutCommand : Command
{
    private readonly string _keyword;
    private readonly string _path;
    
    public AddShortcutCommand(string keyword, string path, IConfiguration configs) : base(configs)
    {
        _keyword = keyword;
        _path = path;
    }

    public override Task PerformAsync()
    {
        if (!Directory.Exists(_path))
        {
            throw new InvalidOperationException($"Directory {_path} does not exist");
        }
        
        // add
        Configs.SavePathShortcuts.Add(_keyword, _path);
        return Task.CompletedTask;
    }
}