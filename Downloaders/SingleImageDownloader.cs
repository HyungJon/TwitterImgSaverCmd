using System;
using System.Collections.Generic;
using TwitterImgSaverCmd.Image;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Downloader to be used when a single image link is provided
    /// This is actually mostly obsolete with current Twitter structure, but is still supported in this project
    /// </summary>
    public class SingleImageDownloader : Downloader
    {
        public SingleImageDownloader(Uri uri, string saveDirectoryPath) : base(uri, saveDirectoryPath)
        {
            Console.WriteLine(" " + _uri + " is an image file");
        }

        protected override Task PrepareDownloadSources()
        {
            ImagesList = new List<IImage> { new DirectUrlImage(_uri) };
            return Task.CompletedTask;
        }
    }
}
