using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Settings
    {
        private readonly IConfiguration Configuration;

        public Settings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetJwtSecret()
        {
            return Configuration["TokenSecret"];
        }

        public string GetStorageConnectionString()
        {
            return Configuration.GetConnectionString("Storage");
        }
    }
}
