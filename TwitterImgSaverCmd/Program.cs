using System;
using System.IO;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// A test project that only tests the workflow of downloading images in original size from given Twitter web page
    /// </summary>
    public class Program
    {
        private static string SaveDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        private static void Main(string[] args)
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

        private static void ProcessInput(string input)
        {
            var command = CommandParser.ParseCommand(input);

            switch (command.Type)
            {
                case CommandType.ChangeDir:
                    {
                        try
                        {
                            SaveDirectoryPath = Path.GetFullPath(command.Parameter);
                            Console.WriteLine(" Save folder changed to " + SaveDirectoryPath);
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

                        IDownloader downloader = DownloadFactory.GetDownloader(uri, SaveDirectoryPath);

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
