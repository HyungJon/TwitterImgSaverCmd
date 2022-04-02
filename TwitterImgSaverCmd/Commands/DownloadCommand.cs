using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd.Commands
{
    public class DownloadCommand : Command
    {
        public DownloadCommand(string parameter, IConfiguration configs) : base(CommandType.Download, parameter, configs) // Can this use a type constraint instead?
        {
            
        }

        public override void Perform()
        {
            if (!Uri.TryCreate(_parameter, UriKind.Absolute, out Uri uri))
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
        }
    }
}
