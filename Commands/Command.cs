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
        protected readonly IConfiguration Configs;

        protected Command(IConfiguration configs)
        {
            Configs = configs;
        }

        public abstract Task PerformAsync();
    }
}
