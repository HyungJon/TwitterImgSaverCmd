using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd.Commands
{
    public abstract class Command : ICommand
    {
        private readonly CommandType _type;
        public CommandType Type => _type;

        private readonly string _parameter;
        public string Parameter => _parameter;

        public Command(CommandType type, string parameter)
        {
            _type = type;
            _parameter = parameter;
        }

        public abstract void Perform();
    }
}
