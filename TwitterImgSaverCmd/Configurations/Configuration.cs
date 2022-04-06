using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public class Configuration : IConfiguration
    {
        // Find a way to save this to an external .cfg file, for persistence
        public string SaveDirectoryPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        // TODO: Is there a way to avoid having to pass the configs everywhere, even where it's never needed?
        // Probably by implementing a static ConfigManager class
    }
}
