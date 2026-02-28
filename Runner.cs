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
                Console.WriteLine(" Error: " + ex.Message);
            }
        }
    }
}