using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_DAL.DataBaseMecanism
{
    public interface ISPConnection
    {
        public string GetConnectionString();
    }

    public class SPConnection : ISPConnection
    {
        private IConfiguration _config;
        public SPConnection(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }
    }
}
