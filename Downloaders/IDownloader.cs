namespace TwitterImgSaverCmd
{
    public interface IDownloader
    {
        void PrepareDownloadSources();
        Task DownloadAsync();
    }
}
