using TwitterImgSaverCmd.Configurations;
using TwitterImgSaverCmd.Downloaders;

namespace TwitterImgSaverCmd;
// File for any functionality related to download environment in general
// Currently only contains the DownloadFactory class

/// <summary>
/// Class that creates instances of suitable Downloader subclass depending on input type
/// </summary>
public class DownloaderFactory : IDownloaderFactory
{
    private readonly IConfiguration _configs;

    public DownloaderFactory(IConfiguration configs)
    {
        _configs = configs;
    }

    private const string DomainTwitter = "www.twitter.com";
    private const string DomainTwitterBase = "twitter.com";
    private const string DomainTwitterShortened = "t.co";
    private const string DomainTwitterX = "x.com";
    private const string DomainTwimg = "pbs.twimg.com";
    
    public IDownloader? GetDownloader(Uri uri, string? savePath)
    {
        return uri.Host switch
        {
            DomainTwitter or DomainTwitterBase or DomainTwitterShortened or DomainTwitterX => new TweetImagesDownloader(uri, savePath ?? _configs.SaveDirectoryPath),
            DomainTwimg => new SingleImageDownloader(uri, savePath ?? _configs.SaveDirectoryPath),
            _ => null,// return a IDownloader implementer that handles invalid cases?
        };
    } 
}