using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Image
{
    public abstract class DownloadableImage : IDownloadableImage
    {
        protected readonly string _fileLink;
        protected readonly string? _tweetId;
        protected readonly int? _index;

        protected DownloadableImage(Uri uri, string? tweetId = null, int? index = null)
        {
            _fileLink = uri.AbsoluteUri;
            _tweetId = tweetId;
            _index = index;
        }

        protected abstract string GetOriginalSizeFileLink(string link);

        protected abstract string ParseFilenameFromLink(string link);

        protected abstract string GenerateOutputFileName(string outputFilenameBase);

        public async Task DownloadAsync(string saveDirectoryPath)
        {
            var fileOrigSizeLink = GetOriginalSizeFileLink(_fileLink);
            var outputFilenameBase = ParseFilenameFromLink(fileOrigSizeLink);
            var outputFilename = GenerateOutputFileName(outputFilenameBase);

            var outputPath = Path.Combine(saveDirectoryPath, outputFilename);
            Console.WriteLine("  Downloading from " + fileOrigSizeLink + " to " + outputPath);

            using var client = new HttpClient();
            var fileBytes = await client.GetByteArrayAsync(fileOrigSizeLink);
            File.WriteAllBytes(outputPath, fileBytes);
        }
    }
}
