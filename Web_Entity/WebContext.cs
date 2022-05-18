using Microsoft.EntityFrameworkCore;
using Web_Entity.Model;

namespace Web_Entity
{
   public class WebContext : DbContext
    {
        //public class OptionsBuild
        //{
        //    public OptionsBuild() {
        //        settings = new AppConfiguration();
        //        opsBuilder = new DbContextOptionsBuilder<WebContext>();
        //        opsBuilder.UseSqlServer(settings.sqlConnectionString);
        //        dbOption = opsBuilder.Options;
        //    }
        //    public DbContextOptionsBuilder<WebContext> opsBuilder { get; set; }
        //    public DbContextOptions<WebContext> dbOption { get; set; }
        //    private AppConfiguration settings { get; set; }
        //}

        //public static OptionsBuild ops = new OptionsBuild();


        public WebContext(DbContextOptions<WebContext> options):base(options) { }
        public DbSet<MUser> MUsers { get; set; }

    }
}
