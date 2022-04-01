using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Commands
{
    public class DownloadCommand : Command
    {
        public DownloadCommand(string parameter) : base(CommandType.Download, parameter) // Can this use a type constraint instead?
        {

        }

        public override void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
