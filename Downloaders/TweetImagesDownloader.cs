using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Downloader to be used when a tweet link, normal or shortened, is provided
    /// </summary>
    public class TweetImagesDownloader : Downloader
    {
        private string tweetId = String.Empty;

        public TweetImagesDownloader(Uri uri, string saveDirectoryPath) : base(uri, saveDirectoryPath)
        {
            Console.WriteLine(" " + _uri + " is a tweet");
        }

        protected override async Task PrepareDownloadSources()
        {
            Console.WriteLine(" Querying tweet...");
            ImagesList = new List<TwitterImage>();

            using var client = new HttpClient();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(await client.GetStringAsync(_uri));

            // obtain from metadata for less dependency to page format
            var imageMetadata = htmlDoc.DocumentNode.SelectSingleNode("html")
                                                    .SelectSingleNode("head")
                                                    .SelectNodes("//meta[@property='og:image']");

            var urlMetadata = htmlDoc.DocumentNode.SelectSingleNode("html")
                                                    .SelectSingleNode("head")
                                                    .SelectSingleNode("//meta[@property='og:url']");
            var url = urlMetadata.Attributes["content"].Value;

            if (url is null) throw new InvalidOperationException("Failed to obtain image source");

            tweetId = url.Substring(url.LastIndexOf('/') + 1);
            Console.WriteLine("  Tweeter ID: " + tweetId);

            // does this need error handling?
            for (var i = 0; i < imageMetadata.Count; i++)
            {
                var metadata = imageMetadata[i];
                string imgLink = metadata.Attributes["content"].Value;
                Console.WriteLine("  Obtained the image link " + imgLink);

                ImagesList.Add(new TwitterImage(new Uri(imgLink), tweetId, (imageMetadata.Count > 1) ? i : (int?)null));
            }
        }
    }
}
