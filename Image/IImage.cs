using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Image
{
    public  interface IImage
    {
        Task DownloadAsync(string saveDirectoryPath);
    }
}
