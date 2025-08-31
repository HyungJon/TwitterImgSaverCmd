using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands;

public abstract class Command : ICommand
{
    protected readonly IConfiguration Configs;

    protected Command(IConfiguration configs)
    {
        Configs = configs;
    }

    public abstract Task PerformAsync();
}