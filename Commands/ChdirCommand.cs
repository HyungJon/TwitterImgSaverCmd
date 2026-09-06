using Microsoft.Extensions.Logging;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands;

public class ChdirCommand : Command
{
    private readonly IConfiguration _configs;
    private readonly string _newDir;
    private readonly ILogger<ChdirCommand> _logger;


    public ChdirCommand(string newDir, IConfiguration configs, ILogger<ChdirCommand> logger)
    {
        _newDir = newDir;
        _configs = configs;
        _logger = logger;
    }

    public override Task PerformAsync()
    {
        ValidateSavePath(_newDir);

        try
        {
            _configs.SaveDirectoryPath = Path.GetFullPath(_newDir);
            // Console.WriteLine(" Save folder changed to " + _configs.SaveDirectoryPath);
            _logger.LogInformation("Save folder changed to {ConfigsSaveDirectoryPath}", _configs.SaveDirectoryPath);
        }
        catch (Exception)
        {
            throw new Exception("Invalid save folder");
        }

        return Task.CompletedTask;
    }

    private static void ValidateSavePath(string path)
    {
        if (path is null)
            throw new NullReferenceException("Folder path cannot be null");

        if (!Path.IsPathRooted(path) || Path.GetPathRoot(path)!.Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            throw new ArgumentException("Must provide full folder path");
        // TODO: expand to make it work for Linux environment

        if (!Directory.Exists(path))
            throw new FileNotFoundException($"Save folder {path} could not be found");

        // TODO: check if program has write permission
    }
}