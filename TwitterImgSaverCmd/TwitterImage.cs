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
        private readonly string _fileOrigSizeLink;
        private readonly string _fileName;
        private readonly string _fileNameToSave;

        public TwitterImage(Uri uri)
        {
            _fileLink = uri.AbsoluteUri;
            _fileOrigSizeLink = ConvertImageLinkToOrig(_fileLink);
            _fileName = _fileOrigSizeLink.Substring(_fileOrigSizeLink.LastIndexOf('/') + 1);
            _fileNameToSave = DropOrigFromFilename(_fileName);
        }

        public async Task DownloadAsync(string saveDirectoryPath)
        {
            string outputPath = Path.Combine(saveDirectoryPath, _fileNameToSave);
            Console.WriteLine("  Downloading from " + _fileOrigSizeLink + " to " + outputPath);

            using (WebClient webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(_fileOrigSizeLink, outputPath);
            }
        }

        private static string ConvertImageLinkToOrig(string link)
        {
            string extension = ParseExtension(link);
            return link.Replace(extension, ConvertExtensionToOrig(extension));
        }

        private static string ParseExtension(string link)
        {
            return link.Substring(link.LastIndexOf('.') + 1);
        }

        private static string ConvertExtensionToOrig(string extension)
        {
            string baseExtension = extension.Contains(':') ? extension.Substring(0, extension.LastIndexOf(':')) : extension;
            return baseExtension + ":orig";
        }

        private static string DropOrigFromFilename(string filename)
        {
            filename = filename.Substring(0, filename.LastIndexOf(':'));
            Console.WriteLine("   Output file name: " + filename);
            return filename;
        }
    }
}
