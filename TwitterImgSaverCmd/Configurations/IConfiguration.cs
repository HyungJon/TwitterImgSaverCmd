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

        /// <summary>
        /// Load configs from file
        /// </summary>
        void LoadConfigs();

        /// <summary>
        /// Save current configs to file
        /// </summary>
        void SaveConfigs();

        string SaveDirectoryPath { get; set; }
        // TODO: look into supporting multiple paths
    }
}
