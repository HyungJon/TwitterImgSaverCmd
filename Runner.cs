using TwitterImgSaverCmd.Configurations;
using TwitterImgSaverCmd.Commands;

namespace TwitterImgSaverCmd;

public class Runner
{
    private readonly IConfiguration _configs;

    public Runner(IConfiguration configs)
    {
        _configs = configs;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Write("Enter URL: \n> ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) continue;
            else if (input.ToLower() == "exit") break;

            try
            {
                var command = ProcessInput(input);

                await command.PerformAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error: " + ex.Message);
            }
        }
    }

    private ICommand ProcessInput(string input) => CommandParser.ParseCommand(input, _configs);
}