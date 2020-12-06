using EmulatorDatabase.Data;
using EmulatorDatabase.Model;
using System;
using System.IO;

namespace EmulatorDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(@"C:\Users\Georgy\Documents\WeatherStation\BusinessAccess\WeatherStation.BusinessAccess.Sqlite\Database\appdb.db"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("База данных не существует!");
                Console.ReadKey();
                //return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("База данных успешно найдена.");
                Console.WriteLine("");
            }

            using var db = new SqliteContext();
            Console.WriteLine("Add New Employee: ");
            db.DeviceRecords.Add(new DeviceRecord { Temperature = 20});
            db.SaveChanges();
            Console.WriteLine("Employee has been added sucessfully.");
        }
    }
}
