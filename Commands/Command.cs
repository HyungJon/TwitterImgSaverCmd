using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands;

public abstract class Command : ICommand
{
    public abstract Task PerformAsync();
}