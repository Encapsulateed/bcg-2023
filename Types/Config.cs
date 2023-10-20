using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types
{
    internal class Config
    {
     //   private readonly IFileProvider provider = new PhysicalFileProvider(@"..\\..\\conf.json");


        private ConfigurationBuilder _configurationBuilder;
        private IConfigurationRoot _config;


        public Config()
        {

            
            _configurationBuilder = new ConfigurationBuilder();
            _configurationBuilder.AddJsonFile(@"conf.json");
            _config = _configurationBuilder.Build();


        }

        public string? GetConnetion()
        {
            return _config.GetConnectionString("DB");
        }
        public string? GetTgToken()
        {
            return _config.GetConnectionString("TelegramToken");
        }
        public string? GetGoogleToken()
        {
            return _config.GetConnectionString("GoogleToken");
        }
    }
}
