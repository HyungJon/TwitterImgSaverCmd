using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands
{
    public class ChdirCommand : Command
    {
        private readonly string _newDir;

        public ChdirCommand(string newDir, IConfiguration configs) : base(configs)
        {
            _newDir = newDir;
        }

        public override Task PerformAsync()
        {
            ValidateSavePath(_newDir);

            try
            {
                Configs.SaveDirectoryPath = Path.GetFullPath(_newDir);
                Console.WriteLine(" Save folder changed to " + Configs.SaveDirectoryPath);
            }
            catch (Exception)
            {
                throw new Exception("Invalid save folder");
            }

            return Task.CompletedTask;
        }

        private static void ValidateSavePath(string path)
        {
            if (path is null)
                throw new NullReferenceException("Folder path cannot be null");

            if (!Path.IsPathRooted(path) || Path.GetPathRoot(path)!.Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                throw new ArgumentException("Must provide full folder path");
            // TODO: expand to make it work for Linux environment

            if (!Directory.Exists(path))
                throw new FileNotFoundException($"Save folder {path} could not be found");

            // TODO: check if program has write permission
        }
    }
}
