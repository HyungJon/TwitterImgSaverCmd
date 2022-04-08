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

        public DownloadCommand(string address, IConfiguration configs) : base(configs)
        {
            _address = address;
        }

        public override async Task PerformAsync()
        {
            if (!Uri.TryCreate(_address, UriKind.Absolute, out var uri))
            {
                throw new Exception("URL not valid");
            }

            var downloader = DownloadFactory.GetDownloader(uri, _configs.SaveDirectoryPath);

            if (downloader is null)
            {
                throw new Exception("Domain not supported");
            }

            await downloader.PrepareDownloadSources();
            await downloader.DownloadAsync();
            // any way to totally enforce the condition that PrepareDownloadSources() is called before Download()?
        }
    }
}
