using Microsoft.Extensions.Logging;

namespace TwitterImgSaverCmd;

public class Runner : IRunner
{
    private readonly ICommandParser _commandParser;
    private readonly ILogger<Runner> _logger;

    public Runner(ICommandParser commandParser, ILogger<Runner> logger)
    {
        _commandParser = commandParser;
        _logger = logger;
    }

    public async Task Run()
    {
        while (true)
        {
            _logger.LogInformation("Enter URL: \n> ");
            // Console.Write("Enter URL: \n> ");
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
                // Console.ForegroundColor = ConsoleColor.Red;
                // Console.WriteLine(" Error: " + ex.Message);
                // Console.ResetColor();
                _logger.LogError("{ExMessage}", ex.Message);
            }
        }
    }
}