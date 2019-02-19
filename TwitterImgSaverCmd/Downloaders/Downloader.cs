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
        protected List<TwitterImage> ImagesList;
        protected string SaveDirectoryPath;
        protected Uri _uri;

        protected Downloader(Uri uri, string saveDirectoryPath)
        {
            _uri = uri;
            SaveDirectoryPath = saveDirectoryPath;
        }

        // not ever supposed to be called, unless a child class calls base.PrepareDownloadSources()
        public virtual void PrepareDownloadSources() { throw new NotImplementedException(); }

        public void Download()
        {
            if (ImagesList == null)
            {
                // handle somehow?
                return;
            }

            Console.WriteLine("");
            List<Task> downloadTasks = new List<Task>();
            
            ImagesList.ForEach(image =>
            {
                downloadTasks.Add(image.DownloadAsync(SaveDirectoryPath));
            }); 

            Task.WaitAll(downloadTasks.ToArray());
        }
    }
}
