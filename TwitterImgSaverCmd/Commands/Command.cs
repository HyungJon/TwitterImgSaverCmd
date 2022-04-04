using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands
{
    public abstract class Command : ICommand
    {
        protected readonly CommandType _type;
        public CommandType Type => _type;

        protected readonly IConfiguration _configs;

        public Command(CommandType type, IConfiguration configs)
        {
            _type = type;
            _configs = configs;
        }

        public abstract void Perform();
    }
}
