using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Configurations
{
    public interface IConfiguration
    {
        // If more are added, consider splitting to individual configs or by category
        // and creating a ConfigurationManager class to manage the configs
        // It could be a static class

        string SaveDirectoryPath { get; set; }
    }
}
