using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SA.Irrigation.Common.Configuration;
using SA.Irrigation.Db.Entities;

namespace SA.Irrigation.Db
{
    public class IrrigationDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public DbSet<DeviceModel> Models { get; set; }

    //    public IrrigationDbContext() : base() { }

        public IrrigationDbContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        //public IrrigationDbContext(DbContextOptions<IrrigationDbContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                optionsBuilder.UseSqlServer(_configuration.Get<DatabaseConfiguration>().ConnectionString);
            }
        }

    }

}
