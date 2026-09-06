using System.Data;
using TwitterImgSaverCmd.Commands;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd;

public class CommandFactory : ICommandFactory
{
    private readonly IDownloaderFactory _downloaderFactory;
    private readonly IConfiguration _configs;

    public CommandFactory(IDownloaderFactory downloaderFactory, IConfiguration configs)
    {
        _downloaderFactory = downloaderFactory;
        _configs = configs;
    }

    public ICommand CreateDownloadCommand(string address, string? filename = null, string? savePathOverride = null)
    {
        if (!Uri.TryCreate(address, UriKind.Absolute, out var uri))
        {
            throw new Exception("URL not valid");
        }
        var downloader = _downloaderFactory.GetDownloader(uri, savePathOverride);
        if (downloader is null)
        {
            throw new Exception("Domain not supported");
        }

        return new DownloadCommand(downloader, filename);
    }

    public ICommand CreateChdirCommand(string newDir)
    {
        return new ChdirCommand(newDir, _configs);
    }

    public ICommand CreateAddShortcutCommand(string keyword, string path)
    {
        return new AddShortcutCommand(keyword, path, _configs);
    }
}