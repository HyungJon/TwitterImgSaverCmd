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

        public override void Perform()
        {
            try
            {
                _configs.SaveDirectoryPath = Path.GetFullPath(_newDir);
                Console.WriteLine(" Save folder changed to " + _configs.SaveDirectoryPath);
            }
            catch (Exception)
            {
                throw new Exception("Invalid save folder");
            }
        }
    }
}
