using TwitterImgSaverCmd.Downloaders;

namespace TwitterImgSaverCmd;

public interface IDownloaderFactory
{
    IDownloader? GetDownloader(Uri uri, string? savePath);
}