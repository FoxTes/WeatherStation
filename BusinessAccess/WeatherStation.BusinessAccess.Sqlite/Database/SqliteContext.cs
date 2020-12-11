using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.BusinessAccess.Sqlite.Data
{
    public class SqliteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string[] myPath = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            string pathDatabase = null;

            for (int i = 0; i < myPath.Length; i++)
                if (myPath[i].Contains("WeatherStation"))
                {
                    pathDatabase = $@"{Path.Combine(myPath[0..(i + 1)])}\WeatherStation\bin\Debug\netcoreapp3.1\database\appdb.db";
                    break;
                }

            options.UseSqlite($"Data Source={pathDatabase}");
        }

        public DbSet<DeviceRecord> DeviceRecords { get; set; }
    }
}
