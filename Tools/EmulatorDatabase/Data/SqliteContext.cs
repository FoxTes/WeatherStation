using System;
using System.IO;
using EmulatorDatabase.Model;
using Microsoft.EntityFrameworkCore;

namespace EmulatorDatabase.Data
{
    public class SqliteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string[] myPath = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            string pathDatabase = Path.Combine(myPath[0], myPath[1], myPath[2], @"WeatherStation\bin\Debug\netcoreapp3.1\database\appdb.db");

            options.UseSqlite($"Data Source={pathDatabase}");
        }

        public DbSet<DeviceRecord> DeviceRecords { get; set; }
    }
}
