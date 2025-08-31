namespace TwitterImgSaverCmd.Commands;

public interface ICommand
{
    Task PerformAsync();
}