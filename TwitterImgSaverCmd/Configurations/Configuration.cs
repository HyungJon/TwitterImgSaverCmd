using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Configurations
{
    // 
    public class Configuration : IConfiguration
    {
        // Find a way to save this to an external .cfg file, for persistence
        public string SaveDirectoryPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    }
}
