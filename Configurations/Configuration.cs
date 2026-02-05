using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftCircuits.IniFileParser;

namespace TwitterImgSaverCmd.Configurations
{
    /// <inheritdoc />
    public class Configuration : IConfiguration
    {
        private readonly IniFile _configFile = new();
        private const string ConfigFileName = "configs.ini";
        
        private readonly string _saveDirectoryPathDefault = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public string SaveDirectoryPath { get; set; }

        public Dictionary<string, string> SavePathShortcuts { get; set; } = new();

        // TODO: enable saving to multiple directories
        // either add a SecondarySaveDirectoryPath, or find a way to enable multiple elements in one value
        // separator is known to work, but hopefully an array would be better
        // biggest reason against separator is that different OS have different path name rules

        public Configuration()
        {
            SaveDirectoryPath = _saveDirectoryPathDefault;

            using var _ = File.AppendText(ConfigFileName); // check path first, and create file if not found
        }

        /// <inheritdoc />
        public void LoadConfigs()
        {
            _configFile.Load(ConfigFileName);

            SaveDirectoryPath = _configFile.GetSetting(IniFile.DefaultSectionName, nameof(SaveDirectoryPath)) ?? _saveDirectoryPathDefault;
            var shortcutsJsonString = _configFile.GetSetting(IniFile.DefaultSectionName, nameof(SavePathShortcuts)) ?? string.Empty;
            try
            {
                SavePathShortcuts = JsonConvert.DeserializeObject<Dictionary<string, string>>(shortcutsJsonString) ?? new Dictionary<string, string>();
            }
            catch (JsonException)
            {
                throw new InvalidOperationException("Shortcuts information in invalid format");
            }
        }

        /// <inheritdoc />
        public void SaveConfigs()
        {
            _configFile.SetSetting(IniFile.DefaultSectionName, nameof(SaveDirectoryPath), SaveDirectoryPath);
            _configFile.SetSetting(IniFile.DefaultSectionName, nameof(SavePathShortcuts), JsonConvert.SerializeObject(SavePathShortcuts));

            _configFile.Save(ConfigFileName);
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
