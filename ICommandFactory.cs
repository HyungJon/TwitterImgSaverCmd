using System.Data;
using TwitterImgSaverCmd.Commands;

namespace TwitterImgSaverCmd;

public interface ICommandFactory
{
    ICommand CreateDownloadCommand(string address, string? filename = null, string? savePathOverride = null);

    ICommand CreateChdirCommand(string newDir);

    ICommand CreateAddShortcutCommand(string keyword, string path);
}