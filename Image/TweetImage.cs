using Microsoft.Extensions.Logging;

namespace TwitterImgSaverCmd.Image;

/// <summary>
/// Represents an image as obtained from a tweet link
/// </summary>
public class TweetImage : DownloadableImage
{
    public TweetImage(Uri uri, string tweetId, ILogger<TweetImage> logger, int? index = null) : base(uri, logger, tweetId, index)
    {
        // format:
        // https://pbs.twimg.com/media/XXXXX.jpg:large
    }

    protected override string GetOriginalSizeFileLink(string link)
    {
        var extension = ParseExtension(link);
        return link.Replace(extension, ConvertExtensionToOrig(extension));
    }

    protected override string ParseFilenameFromLink(string link)
    {
        var filename = link[(link.LastIndexOf('/') + 1)..];
        return DropOrigFromFilename(filename);
    }

    protected override string GenerateOutputFileName(string outputFilenameBase) =>
        $"{_tweetId!}{ (_index.HasValue ? $"_{_index.Value}" : string.Empty) }.{ParseExtension(outputFilenameBase)}";

    private static string ConvertExtensionToOrig(string extension)
    {
        var baseExtension = extension.Contains(':') ? extension[..extension.LastIndexOf(':')] : extension;
        return baseExtension + ":orig";
    }

    private string DropOrigFromFilename(string filename)
    {
        filename = filename[..filename.LastIndexOf(':')];
        // Console.WriteLine("   Image file name: " + filename);
        Logger.LogInformation("Image file name: {Filename}", filename);
        return filename;
    }
}