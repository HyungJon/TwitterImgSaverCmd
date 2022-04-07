using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Commands
{
    public interface ICommand
    {
        void Perform();
    }
}
