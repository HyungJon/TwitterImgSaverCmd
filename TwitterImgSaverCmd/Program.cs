using System;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd
{
    /// <summary>
    /// A test project that only tests the workflow of downloading images in original size from given Twitter web page
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            var configs = new Configuration();

            try
            {
                configs.LoadConfigs();

                var runner = new Runner(configs);

                runner.Run();
            }
            finally
            {
                configs.SaveConfigs();
            }
        }
    }
}
