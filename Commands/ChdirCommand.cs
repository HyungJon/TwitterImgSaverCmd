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
            if (_newDir is null)
                throw new NullReferenceException("Provided folder path was null");

            if (!Path.IsPathRooted(_newDir) || Path.GetPathRoot(_newDir)!.Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                throw new ArgumentException("Must provide full folder path");
            // TODO: expand to make it work for Linux environment

            try
            {
                _configs.SaveDirectoryPath = Path.GetFullPath(_newDir);
                Console.WriteLine(" Save folder changed to " + _configs.SaveDirectoryPath);
            }
            catch (Exception)
            {
                throw new Exception("Invalid save folder");
            }

            return Task.CompletedTask;
        }
    }
}
