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

        public static IDownloader GetDownloader(Uri uri, string savePath)
        {
            switch (uri.Host)
            {
                case DomainTwitter:
                case DomainTwitterBase:
                case DomainTwitterShortened:
                    return new TweetImagesDownloader(uri, savePath);
                case DomainTwimg:
                    return new ImageDownloader(uri, savePath);
                default:
                    return null; // return a IDownloader implementer that handles invalid cases?
                // is there any more robust way to do this?
            }
        } 
    }
}
