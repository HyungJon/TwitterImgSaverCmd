using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftCircuits.IniFileParser;

namespace TwitterImgSaverCmd.Configurations
{
    /// <inheritdoc />
    public class Configuration : IConfiguration
    {
        // Find a way to save this to an external .cfg file, for persistence

        private readonly IniFile _configFile = new();
        private readonly string _configFilePath = "config.ini";

        public string SaveDirectoryPath { get; set; }

        public Configuration()
        {
            // check path first, and create file
            using StreamWriter w = File.AppendText(_configFilePath);
        }

        /// <inheritdoc />
        public void LoadConfigs()
        {
            _configFile.Load(_configFilePath);

            SaveDirectoryPath = _configFile.GetSetting(IniFile.DefaultSectionName, nameof(SaveDirectoryPath), Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }

        /// <inheritdoc />
        public void SaveConfigs()
        {
            _configFile.SetSetting(IniFile.DefaultSectionName, nameof(SaveDirectoryPath), SaveDirectoryPath);

            _configFile.Save(_configFilePath);
        }




        // TODO: Is there a way to avoid having to pass the configs everywhere, even where it's never needed directly?
        // Probably by implementing a static ConfigManager class

        // TODO: Upon implementing configs save and UI, add RunUi configuration that determines whether this program will run with UI or with console
        // Also add a new command that updates this config
    }
}
