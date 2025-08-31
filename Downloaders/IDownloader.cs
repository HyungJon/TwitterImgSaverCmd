namespace TwitterImgSaverCmd.Downloaders;

public interface IDownloader
{
    Task DownloadAsync(string? filenameToUse = null);
}