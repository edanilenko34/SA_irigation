using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SA.Irrigation.Common.Configuration;
using SA.Irrigation.Db.Entities;
using System.Data.Entity.Infrastructure;

namespace SA.Irrigation.Db
{
    public class IrrigationDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public DbSet<DeviceModel> Models { get; set; }
        public DbSet<Device> Devices { get; set; }
        public IQueryable<Sensor> Sensors =>
            Devices.Include(s => s.Model).Where(w => w.Model.Type == Common.Enums.DeviceType.Sensor).Select(s => (Sensor)s);
        public DbSet<Schedule> Schedules { get; set; }


        public IrrigationDbContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                optionsBuilder.UseSqlServer(_configuration.Get<DatabaseConfiguration>().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddQuartz(builder => builder.UseSqlServer());
        }

    }

}
