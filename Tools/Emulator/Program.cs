using System;
using System.IO.Ports;

namespace Emulator
{
    class Program
    {
        public static SerialPort _serialPort;

        static void Main(string[] args)
        {
            _serialPort = new SerialPort("COM7", 9600);

            try
            {
                _serialPort.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Порт успешно открыт. Слушаем данные.");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не удалось открыть порт.");

                Console.ReadLine();
            }
            finally
            {
                _serialPort.DiscardInBuffer();
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            _serialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var countByte = _serialPort.BytesToRead;
            var data = new byte[_serialPort.BytesToRead];
            _serialPort.Read(data, 0, countByte);

            if (data.Length != 3)
                return;

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Meteostation-01");
            _serialPort.WriteLine("Meteostation-01");
        }
    }
}
