using EmulatorDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace EmulatorDatabase.Data
{
    public class SqliteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\Georgy\Documents\WeatherStation\BusinessAccess\WeatherStation.BusinessAccess.Sqlite\Database\appdb.db");

        public DbSet<DeviceRecord> DeviceRecords { get; set; }
    }
}
