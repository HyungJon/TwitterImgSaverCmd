using Microsoft.Extensions.Logging;
using TwitterImgSaverCmd.Image;

namespace TwitterImgSaverCmd.Downloaders;

/// <summary>
/// Downloader to be used when a single image link is provided
/// </summary>
public class SingleImageDownloader : Downloader
{
    private readonly ILogger<SingleImageDownloader> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public SingleImageDownloader(Uri uri, string saveDirectoryPath, ILoggerFactory loggerFactory) : base(uri, saveDirectoryPath)
    {
        _loggerFactory = loggerFactory;
        _logger = _loggerFactory.CreateLogger<SingleImageDownloader>();

        _logger.LogInformation("{Uri} is an image file", _uri);
        // Console.WriteLine(" " + _uri + " is an image file");
    }

    protected override Task<IEnumerable<IDownloadableImage>> PrepareDownloadSourcesAsync() =>
        Task.FromResult<IEnumerable<IDownloadableImage>>(new List<IDownloadableImage> { new DirectUrlImage(_uri, _loggerFactory.CreateLogger<DirectUrlImage>()) });
}