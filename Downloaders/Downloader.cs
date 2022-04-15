using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Image;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Abstract class acting as container for common code, not to be used as independent class
    /// </summary>
    public abstract class Downloader : IDownloader
    {
        protected string SaveDirectoryPath;
        protected Uri _uri;

        protected Downloader(Uri uri, string saveDirectoryPath)
        {
            _uri = uri;
            SaveDirectoryPath = saveDirectoryPath;
        }

        protected abstract Task<IEnumerable<IDownloadableImage>> PrepareDownloadSourcesAsync();

        public async Task DownloadAsync()
        {
            var imageSources = await PrepareDownloadSourcesAsync();

            if (imageSources == null) throw new InvalidOperationException("Failed to obtain images from tweet");

            Console.WriteLine("");

            await Task.WhenAll(imageSources.Select(image => image.DownloadAsync(SaveDirectoryPath)));
        }
    }
}
