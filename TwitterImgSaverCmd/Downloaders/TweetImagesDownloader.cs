using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

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

        public override void PrepareDownloadSources()
        {
            Console.WriteLine(" Querying tweet...");
            ImagesList = new List<TwitterImage>();
            
            using (WebClient webClient = new WebClient())
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(webClient.DownloadString(_uri));

                // obtain from metadata for less dependency to page format
                var imageMetadata = htmlDoc.DocumentNode.SelectSingleNode("html")
                                                        .SelectSingleNode("head")
                                                        .SelectNodes("//meta[@property='og:image']");

                // does this need error handling?
                foreach (var metadata in imageMetadata)
                {
                    string imgLink = metadata.Attributes["content"].Value;
                    Console.WriteLine("  Obtained the image link " + imgLink);

                    ImagesList.Add(new TwitterImage(new Uri(imgLink)));
                }
            }
        }
    }
}
