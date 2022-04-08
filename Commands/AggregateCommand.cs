using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands
{
    /// <summary>
    /// Class to aggregate multiple commands and run them together
    /// mostly for downloading from multiple sources at once, but may have other uses
    /// </summary>
    public class AggregateCommand : Command
    {
        private readonly IEnumerable<ICommand> _commands;

        public AggregateCommand(IEnumerable<ICommand> commands, IConfiguration configs) : base(configs)
        {
            _commands = commands;
        }

        public override async Task PerformAsync()
        {
            await Task.WhenAll(_commands.ToList().Select(command => command.PerformAsync()));
        }
    }
}
