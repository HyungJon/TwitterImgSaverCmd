using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterImgSaverCmd
{
    public class CommandType
    {
        private readonly CommandType _type;
        public CommandType Type => _type;

        private readonly string _parameter;
        public string Parameter => _parameter;

        public CommandType(CommandType type, string parameter)
        {
            _type = type;
            _parameter = parameter;
        }
    }
}
