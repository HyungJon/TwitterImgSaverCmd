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

        protected override Task<IEnumerable<IImage>> PrepareDownloadSourcesAsync() => Task.FromResult(new List<IImage> { new DirectUrlImage(_uri) } as IEnumerable<IImage>);
    }
}
