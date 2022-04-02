using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
