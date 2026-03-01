using TwitterImgSaverCmd.Downloaders;

namespace TwitterImgSaverCmd.Commands;

public class DownloadCommand : Command
{
    private readonly IDownloader _downloader;
    private readonly string? _filename;

    public DownloadCommand(IDownloader downloader, string? filename = null)
    {
        _downloader = downloader;
        _filename = filename;
    }

    public override async Task PerformAsync()
    {
        await _downloader.DownloadAsync(_filename);
    }
}