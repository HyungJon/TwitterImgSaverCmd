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
        private readonly string _address;

        public DownloadCommand(string address, IConfiguration configs) : base(CommandType.Download, configs) // Can this use a type constraint instead?
        {
            _address = address;
        }

        public override void Perform()
        {
            if (!Uri.TryCreate(_address, UriKind.Absolute, out Uri uri))
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
    }
}
