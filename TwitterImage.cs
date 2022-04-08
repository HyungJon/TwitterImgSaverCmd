using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// Class encapsulating a Twitter image and containing logic for processing image URL to obtain useful information
    /// In actual app, to be extended to include actual image information (to show on UI), etc
    /// </summary>
    public class TwitterImage
    {
        private readonly string _fileLink;
        private readonly string? _tweetId;
        private readonly int? _index;

        public TwitterImage(Uri uri, string? tweetId = null, int? index = null)
        {
            _fileLink = uri.AbsoluteUri;
            _tweetId = tweetId;
            _index = index;
        }

        public async Task DownloadAsync(string saveDirectoryPath)
        {
            var fileOrigSizeLink = ConvertImageLinkToOrig(_fileLink);

            var fileName = DropOrigFromFilename(fileOrigSizeLink[(fileOrigSizeLink.LastIndexOf('/') + 1)..]);

            var fileNameToSave = (_tweetId != null)
                ? $"{_tweetId}{ (_index.HasValue ? $"_{_index.Value}" : string.Empty) }.{ParseExtension(fileName)}"
                : fileName;

            var outputPath = Path.Combine(saveDirectoryPath, fileNameToSave);
            Console.WriteLine("  Downloading from " + fileOrigSizeLink + " to " + outputPath);

            using var client = new HttpClient();
            var fileBytes = await client.GetByteArrayAsync(fileOrigSizeLink);
            File.WriteAllBytes(outputPath, fileBytes);
        }

        private static string ConvertImageLinkToOrig(string link)
        {
            string extension = ParseExtension(link);
            return link.Replace(extension, ConvertExtensionToOrig(extension));
        }

        private static string ParseExtension(string link)
        {
            return link[(link.LastIndexOf('.') + 1)..];
        }

        private static string ConvertExtensionToOrig(string extension)
        {
            string baseExtension = extension.Contains(':') ? extension.Substring(0, extension.LastIndexOf(':')) : extension;
            return baseExtension + ":orig";
        }

        private static string DropOrigFromFilename(string filename)
        {
            filename = filename.Substring(0, filename.LastIndexOf(':'));
            Console.WriteLine("   Image file name: " + filename);
            return filename;
        }
    }
}
