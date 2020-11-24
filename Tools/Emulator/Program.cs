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
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
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

            Console.WriteLine("Press any key to continue...");
            Console.WriteLine();
            Console.ReadKey();
            _serialPort.Close();
        }

        private static void DataReceivedHandler(
                            object sender,
                            SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            var countByte = sp.ReadBufferSize;

            if (countByte != 3)
                return;
                
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received");

            _serialPort.WriteLine("Метиостанция- 1");

            Console.Write(indata.ToString());
        }
    }
}
