using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Entity
{
   public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configBulder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBulder.AddJsonFile(path, false);
            var root = configBulder.Build();
            var appSetting = root.GetSection("ConnectionStrings:WebContext");
            sqlConnectionString = appSetting.Value;
        }
        public string sqlConnectionString { get; set; }
    }
}
