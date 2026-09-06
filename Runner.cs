namespace TwitterImgSaverCmd;

public class Runner : IRunner
{
    private readonly ICommandParser _commandParser;

    public Runner(ICommandParser commandParser)
    {
        _commandParser = commandParser;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Write("Enter URL: \n> ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) continue;
            if (input.ToLowerInvariant().Equals("exit")) break;

            try
            {
                var command = _commandParser.ParseCommand(input);

                await command.PerformAsync();
            }
            catch (Exception ex)
            {
                // TODO: add a dedicated logger that handles outputs, setting colors depending on output type message/warning/error/etc
                // TODO: also see if the indentation can be handled by the logger, instead of by each message printer manually adding spaces
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Error: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}