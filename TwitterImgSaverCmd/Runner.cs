using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd
{
    public class Runner
    {
        private readonly IConfiguration _configs;

        public Runner(IConfiguration configs)
        {
            _configs = configs;
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("Enter URL: \n> ");
                string input = Console.ReadLine();
                if (input.Trim(' ') == string.Empty) break;

                try
                {
                    ProcessInput(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Error: " + ex.Message);
                }
            }
        }

        private void ProcessInput(string input)
        {
            var command = CommandParser.ParseCommand(input, _configs);

            command.Perform();
        }
    }
}
