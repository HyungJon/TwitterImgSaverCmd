using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using TwitterImgSaverCmd.Image;

namespace TwitterImgSaverCmd.Downloaders;

/// <summary>
/// Downloader to be used when a tweet link, normal or shortened, is provided
/// </summary>
public class TweetImagesDownloader : Downloader
{
    private readonly ILogger<TweetImagesDownloader> _logger;
    private readonly ILoggerFactory _loggerFactory;
    
    // pbs.twimg.com only ever stores tweet photos as one of these; the classic <id>.<ext>:orig
    // form 404s unless the extension matches the media's real stored format, so it doubles as a type check
    private static readonly string[] CandidateExtensions = { "jpg", "png" };

    public TweetImagesDownloader(Uri uri, string saveDirectoryPath, ILoggerFactory loggerFactory) : base(uri, saveDirectoryPath)
    {
        _loggerFactory = loggerFactory;
        // Console.WriteLine(" " + _uri + " is a tweet");
        _logger = _loggerFactory.CreateLogger<TweetImagesDownloader>();

        _logger.LogInformation("{Uri} is a tweet", _uri);
    }

    protected override async Task<IEnumerable<IDownloadableImage>> PrepareDownloadSourcesAsync()
    {
        var imageList = new List<IDownloadableImage>();

        // Console.WriteLine(" Querying tweet...");
        _logger.LogInformation("Querying tweet...");

        using var client = new HttpClient();
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(await client.GetStringAsync(_uri));

        var urlMetadata = htmlDoc.DocumentNode.SelectSingleNode("html")
            .SelectSingleNode("head")
            .SelectSingleNode("//meta[@property='og:url']");
        var url = urlMetadata.Attributes["content"].Value;

        if (url is null) throw new InvalidOperationException("Failed to obtain image source");

        var tweetId = url[(url.LastIndexOf('/') + 1)..];
        // Console.WriteLine("  Tweeter ID: " + tweetId);
        _logger.LogInformation("Tweeter ID: {TweetId}", tweetId);

        var mediaIds = (htmlDoc.DocumentNode.SelectNodes($"//a[contains(@href, '/status/{tweetId}/photo/')]") ?? Enumerable.Empty<HtmlNode>())
            .Select(anchor => anchor.SelectSingleNode(".//img[contains(@src,'pbs.twimg.com/media/')]")) // get tweet media
            .Where(img => img is not null)
            .Select(img => img!.Attributes["src"].Value)
            .Select(src => src[(src.IndexOf("/media/", StringComparison.Ordinal) + "/media/".Length)..].Split('?')[0]) // make sure the media is attached images, not unrelated media (e.g. ads)
            .Distinct()
            .ToList();

        if (mediaIds.Count == 0) throw new InvalidOperationException("Failed to obtain image source");

        for (var i = 0; i < mediaIds.Count; i++)
        {
            var extension = await ResolveExtensionAsync(client, mediaIds[i]);
            var imgLink = $"https://pbs.twimg.com/media/{mediaIds[i]}.{extension}";
            // Console.WriteLine("  Obtained the image link " + imgLink);
            _logger.LogInformation("Obtained image link {ImgLink}", imgLink);

            imageList.Add(new TweetImage(new Uri(imgLink), tweetId, _loggerFactory.CreateLogger<TweetImage>(), (mediaIds.Count > 1) ? i + 1 : null));
        }

        return imageList;
    }

    private static async Task<string> ResolveExtensionAsync(HttpClient client, string mediaId)
    {
        foreach (var extension in CandidateExtensions)
        {
            using var request = new HttpRequestMessage(HttpMethod.Head, $"https://pbs.twimg.com/media/{mediaId}.{extension}:orig");
            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return extension;
            }
        }

        throw new InvalidOperationException($"Failed to determine file extension for media {mediaId}");
    }
}