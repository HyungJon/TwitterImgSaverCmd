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
        public ChdirCommand(string parameter, IConfiguration configs) : base(CommandType.ChangeDir, parameter, configs) { }

        public override void Perform()
        {
            try
            {
                _configs.SaveDirectoryPath = Path.GetFullPath(Parameter);
                Console.WriteLine(" Save folder changed to " + _configs.SaveDirectoryPath);
            }
            catch (Exception)
            {
                throw new Exception("Invalid save folder");
            }
        }
    }
}
