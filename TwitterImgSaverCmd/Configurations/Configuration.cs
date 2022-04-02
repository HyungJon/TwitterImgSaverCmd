using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Configurations
{
    public class Configuration : IConfiguration
    {
        public string SaveDirectoryPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    }
}
