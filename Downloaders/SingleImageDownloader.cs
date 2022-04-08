using System;
using System.Collections.Generic;

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

        protected override Task PrepareDownloadSources()
        {
            ImagesList = new List<TwitterImage> { new TwitterImage(_uri) };
            return Task.CompletedTask;
        }
    }
}
