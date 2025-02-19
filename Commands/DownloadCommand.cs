﻿using System;
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
        private readonly string? _filename;
        private readonly string? _savePathOverride;

        public DownloadCommand(string address, IConfiguration configs, string? filename = null, string? savePathOverride = null) : base(configs)
        {
            _address = address;
            _filename = filename;
            _savePathOverride = savePathOverride;
        }

        public override async Task PerformAsync()
        {
            if (!Uri.TryCreate(_address, UriKind.Absolute, out var uri))
            {
                throw new Exception("URL not valid");
            }

            var downloader = DownloadFactory.GetDownloader(uri, _savePathOverride ?? Configs.SaveDirectoryPath);

            if (downloader is null)
            {
                throw new Exception("Domain not supported");
            }

            await downloader.DownloadAsync(_filename);
        }
    }
}
