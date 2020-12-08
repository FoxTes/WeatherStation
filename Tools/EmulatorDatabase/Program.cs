using System;
using System.IO;
using EmulatorDatabase.Data;
using EmulatorDatabase.Model;

namespace EmulatorDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] myPath = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            string pathDatabase = null;

            for (int i = 0; i < myPath.Length; i++)
                if (myPath[i].Contains("WeatherStation"))
                    pathDatabase = $@"{Path.Combine(myPath[0..(i + 1)])}\WeatherStation\bin\Debug\netcoreapp3.1\database\appdb.db";


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
                Console.WriteLine(pathDatabase);
                Console.WriteLine("");
            }

            using var db = new SqliteContext();
            Console.WriteLine("Add New Employee: ");
            db.DeviceRecords.Add(new DeviceRecord { Temperature = 20 });
            db.SaveChanges();
            Console.WriteLine("Employee has been added sucessfully.");
        }
    }
}
