using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Abstract class acting as container for common code, not to be used as independent class
    /// </summary>
    public abstract class Downloader : IDownloader
    {
        protected List<TwitterImage> ImagesList = new();
        protected string SaveDirectoryPath;
        protected Uri _uri;

        protected Downloader(Uri uri, string saveDirectoryPath)
        {
            _uri = uri;
            SaveDirectoryPath = saveDirectoryPath;
        }

        protected abstract Task PrepareDownloadSources();

        public async Task DownloadAsync()
        {
            await PrepareDownloadSources();

            if (ImagesList == null) throw new InvalidOperationException("Failed to obtain images from tweet");

            Console.WriteLine("");

            await Task.WhenAll(ImagesList.Select(image => image.DownloadAsync(SaveDirectoryPath)));
        }
    }
}
