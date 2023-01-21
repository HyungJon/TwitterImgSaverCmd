namespace TwitterImgSaverCmd
{
    public interface IDownloader
    {
        Task DownloadAsync(string? filenameToUse = null);
    }
}
