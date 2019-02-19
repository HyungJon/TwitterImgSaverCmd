namespace TwitterImgSaverCmd
{
    public interface IDownloader
    {
        void PrepareDownloadSources();
        void Download();
    }
}
