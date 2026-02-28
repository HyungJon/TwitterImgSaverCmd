using TwitterImgSaverCmd.Commands;

namespace TwitterImgSaverCmd;

public interface ICommandParser
{
    ICommand ParseCommand(string input);
}