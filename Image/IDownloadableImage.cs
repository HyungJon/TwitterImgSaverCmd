namespace TwitterImgSaverCmd.Image;

public  interface IDownloadableImage
{
    Task DownloadAsync(string saveDirectoryPath, string? filenameToUse = null);
}