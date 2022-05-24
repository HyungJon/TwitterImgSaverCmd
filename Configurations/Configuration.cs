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
        private readonly IniFile _configFile = new();
        private const string _configFileName = "configs.ini";
        
        private readonly string _saveDirectoryPathDefault = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public string SaveDirectoryPath { get; set; }

        // TODO: enable saving to multiple directories
        // either add a SecondarySaveDirectoryPath, or find a way to enable multiple elements in one value
        // separator is known to work, but hopefully an array would be better

        public Configuration()
        {
            SaveDirectoryPath = _saveDirectoryPathDefault;

            using StreamWriter _ = File.AppendText(_configFileName); // check path first, and create file if not found
        }

        /// <inheritdoc />
        public void LoadConfigs()
        {
            _configFile.Load(_configFileName);

            SaveDirectoryPath = _configFile.GetSetting(IniFile.DefaultSectionName, nameof(SaveDirectoryPath)) ?? _saveDirectoryPathDefault;
        }

        /// <inheritdoc />
        public void SaveConfigs()
        {
            _configFile.SetSetting(IniFile.DefaultSectionName, nameof(SaveDirectoryPath), SaveDirectoryPath);

            _configFile.Save(_configFileName);
        }


        // TODO: implement a config parser for configs that are to be treated as non-string formats

        // TODO: ini files allow for sections, look into sections (even if currently there's only one config and it goes under general)
        // TODO: if enough configs are added, separate into config values manager class and config file manager class

        // TODO: Is there a way to avoid having to pass the configs everywhere, even where it's never needed directly?
        // Probably by implementing a static ConfigManager class

        // TODO: Upon implementing configs save and UI, add RunUi configuration that determines whether this program will run with UI or with console
        // Also add a new command that updates this config
    }
}
