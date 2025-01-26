using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Image
{
    /// <summary>
    /// Represents an image as obtained from image link
    /// </summary>
    public class DirectUrlImage : DownloadableImage
    {
        public DirectUrlImage(Uri uri) : base(uri, null, null)
        {
            // format:
            // https://pbs.twimg.com/media/XXXXX?format=jpg&name=360x360
        }

        protected override string GetOriginalSizeFileLink(string link)
        {
            var sizeName = link[(link.LastIndexOf("name=", StringComparison.Ordinal) + 1)..];
            return link.Replace(sizeName, ConvertExtensionToOrig(sizeName));
        }

        protected override string ParseFilenameFromLink(string link)
        {
            var filenameStartIdx = link.LastIndexOf('/') + 1;
            var filenameEndIdx = link.IndexOf('?');
            var filename = link[filenameStartIdx..filenameEndIdx];

            var extensionStartIdx = link.IndexOf("=", StringComparison.Ordinal) + 1;
            var extensionEndIdx = link.IndexOf('&');
            var extension = link[extensionStartIdx..extensionEndIdx];

            return $"{filename}.{extension}";
        }

        protected override string GenerateOutputFileName(string outputFilenameBase) => outputFilenameBase;

        private static string ConvertExtensionToOrig(string extension)
        {
            var baseExtension = extension.Contains('=') ? extension[..extension.LastIndexOf('=')] : extension;
            return baseExtension + "=orig";
        }
    }
}
