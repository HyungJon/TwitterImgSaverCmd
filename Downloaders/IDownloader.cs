namespace TwitterImgSaverCmd
{
    public interface IDownloader
    {
        Task PrepareDownloadSources();
        Task DownloadAsync();
    }
}
