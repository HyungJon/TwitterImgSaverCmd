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

            // command.Perform();

            // Remove below code when how to keep track of the directory path as configs is figured out
            switch (command.Type)
            {
                case CommandType.ChangeDir:
                    {
                        try
                        {
                            _configs.SaveDirectoryPath = Path.GetFullPath(command.Parameter);
                            Console.WriteLine(" Save folder changed to " + _configs.SaveDirectoryPath);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Invalid save folder");
                        }
                    }
                    break;
                case CommandType.Download:
                    {
                        if (!Uri.TryCreate(command.Parameter, UriKind.Absolute, out Uri uri))
                        {
                            throw new Exception("URL not valid");
                        }

                        IDownloader downloader = DownloadFactory.GetDownloader(uri, _configs.SaveDirectoryPath);

                        if (downloader == null)
                        {
                            throw new Exception("Domain not supported");
                        }

                        downloader.PrepareDownloadSources();
                        downloader.Download();
                        // any way to totally enforce the condition that PrepareDownloadSources() is called before Download()?
                    }
                    break;
                default:
                    throw new Exception("Command not supported");
            }
        }
    }
}
