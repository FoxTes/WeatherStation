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
            string[] myPath = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            string pathDatabase = Path.Combine(myPath[0], myPath[1], myPath[2], @"WeatherStation\bin\Debug\netcoreapp3.1\database\appdb.db");

            if (!File.Exists(pathDatabase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("База данных не существует!");
                Console.ReadKey();
                return;
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
