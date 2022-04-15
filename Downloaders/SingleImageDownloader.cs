using System;
using System.Collections.Generic;
using TwitterImgSaverCmd.Image;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Downloader to be used when a single image link is provided
    /// </summary>
    public class SingleImageDownloader : Downloader
    {
        public SingleImageDownloader(Uri uri, string saveDirectoryPath) : base(uri, saveDirectoryPath)
        {
            Console.WriteLine(" " + _uri + " is an image file");
        }

        protected override Task<IEnumerable<IDownloadableImage>> PrepareDownloadSourcesAsync() => Task.FromResult(new List<IDownloadableImage> { new DirectUrlImage(_uri) } as IEnumerable<IDownloadableImage>);
    }
}
