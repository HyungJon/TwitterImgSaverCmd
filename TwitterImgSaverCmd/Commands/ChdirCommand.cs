using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Commands
{
    public class ChdirCommand : Command
    {
        public ChdirCommand(string parameter) : base(CommandType.ChangeDir, parameter) { }

        public override void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
