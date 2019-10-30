using System;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// A test project that only tests the workflow of downloading images in original size from given Twitter web page
    /// </summary>
    public class Program
    {
        private const string MSG_ERROR_INVALID_URL = "ERROR: not valid URL";
        private const string MSG_ERROR_UNSUPPORTED_DOMAIN = " ERROR: not a supported domain";

        // later make this configurable, in full version with UI
        private static string SaveDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        private static void Main(string[] args)
        {
            while (true) 
            {
                Console.Write("Enter URL: \n> ");
                string input = Console.ReadLine();
                if (input == string.Empty) break;

                ProcessInput(input);
            }
        }

        private static void ProcessInput(string input)
        {
            if (!Uri.TryCreate(input, UriKind.Absolute, out Uri uri))
            {
                Console.WriteLine(" " + MSG_ERROR_INVALID_URL);
                return;
            }

            IDownloader downloader = DownloadFactory.GetDownloader(uri, SaveDirectoryPath);

            if (downloader == null)
            {
                Console.WriteLine(" " + MSG_ERROR_UNSUPPORTED_DOMAIN);
                return;
            }

            downloader.PrepareDownloadSources();
            downloader.Download();
            // any way to totally enforce the condition that PrepareDownloadSources() is called before Download()?
        }
    }
}
