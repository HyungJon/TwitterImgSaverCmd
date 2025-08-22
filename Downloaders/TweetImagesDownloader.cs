using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;
using TwitterImgSaverCmd.Image;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Downloader to be used when a tweet link, normal or shortened, is provided
    /// </summary>
    public class TweetImagesDownloader : Downloader
    {
        public TweetImagesDownloader(Uri uri, string saveDirectoryPath) : base(uri, saveDirectoryPath)
        {
            Console.WriteLine(" " + _uri + " is a tweet");
        }

        protected override async Task<IEnumerable<IDownloadableImage>> PrepareDownloadSourcesAsync()
        {
            var imageList = new List<IDownloadableImage>();

            Console.WriteLine(" Querying tweet...");

            using var client = new HttpClient();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(await client.GetStringAsync(_uri));

            // obtain from metadata for less dependency to page format
            var imageNodes = htmlDoc.DocumentNode.SelectNodes("//pbs.twimg.com");
            var imageMetadata = htmlDoc.DocumentNode.SelectSingleNode("html")
                                                    .SelectSingleNode("head")
                                                    .SelectNodes("//meta[@property='og:image']");

            var urlMetadata = htmlDoc.DocumentNode.SelectSingleNode("html")
                                                    .SelectSingleNode("head")
                                                    .SelectSingleNode("//meta[@property='og:url']");
            var url = urlMetadata.Attributes["content"].Value;

            if (url is null) throw new InvalidOperationException("Failed to obtain image source");

            var tweetId = url[(url.LastIndexOf('/') + 1)..];
            Console.WriteLine("  Tweeter ID: " + tweetId);

            // does this need error handling?
            for (var i = 0; i < imageMetadata.Count; i++)
            {
                var metadata = imageMetadata[i];
                string imgLink = metadata.Attributes["content"].Value;
                Console.WriteLine("  Obtained the image link " + imgLink);

                imageList.Add(new TweetImage(new Uri(imgLink), tweetId, (imageMetadata.Count > 1) ? i + 1 : null));
            }

            return imageList;
        }
    }
}
