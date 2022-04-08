using System;

namespace TwitterImgSaverCmd
{
    // File for any functionality related to download environment in general
    // Currently only contains the DownloadFactory class

    /// <summary>
    /// Class that creates instances of suitable Downloader subclass depending on input type
    /// </summary>
    public static class DownloadFactory
    {
        private const string DomainTwitter = "www.twitter.com";
        private const string DomainTwitterBase = "twitter.com";
        private const string DomainTwitterShortened = "t.co";
        private const string DomainTwimg = "pbs.twimg.com";

        public static IDownloader? GetDownloader(Uri uri, string savePath)
        {
            return uri.Host switch
            {
                DomainTwitter or DomainTwitterBase or DomainTwitterShortened => new TweetImagesDownloader(uri, savePath),
                DomainTwimg => new ImageDownloader(uri, savePath),
                _ => null,// return a IDownloader implementer that handles invalid cases?
            };
        } 
    }
}
