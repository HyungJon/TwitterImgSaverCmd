using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Image
{
    /// <summary>
    /// Represents an image as obtained from a tweet link
    /// </summary>
    public class TweetImage : Image
    {
        public TweetImage(Uri uri, string tweetId, int? index = null) : base(uri, tweetId, index)
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

        private static string ParseExtension(string link)
        {
            return link[(link.LastIndexOf('.') + 1)..];
        }

        private static string ConvertExtensionToOrig(string extension)
        {
            var baseExtension = extension.Contains(':') ? extension[..extension.LastIndexOf(':')] : extension;
            return baseExtension + ":orig";
        }

        private static string DropOrigFromFilename(string filename)
        {
            filename = filename[..filename.LastIndexOf(':')];
            Console.WriteLine("   Image file name: " + filename);
            return filename;
        }
    }
}
