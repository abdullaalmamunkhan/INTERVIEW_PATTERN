using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Web_Entity
{
   public class WebContextFactory:IDesignTimeDbContextFactory<WebContext>
    {
        public WebContext CreateDbContext(string[] args) {
            AppConfiguration appConfig = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<WebContext>();
            opsBuilder.UseSqlServer(appConfig.sqlConnectionString);
            return new WebContext(opsBuilder.Options);
        }
    }
}
