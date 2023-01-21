using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Image
{
    public  interface IDownloadableImage
    {
        Task DownloadAsync(string saveDirectoryPath, string? filenameToUse = null);
    }
}
