using System;
using System.Collections.Generic;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Downloader to be used when a single image link is provided
    /// </summary>
    public class ImageDownloader : Downloader
    {
        public ImageDownloader(Uri uri, string saveDirectoryPath) : base(uri, saveDirectoryPath)
        {
            Console.WriteLine(" " + _uri + " is an image file");
        }

        public override Task PrepareDownloadSources()
        {
            return Task.FromResult(ImagesList = new List<TwitterImage> { new TwitterImage(_uri) });
        }
    }
}
