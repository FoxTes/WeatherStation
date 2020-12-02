using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Emulator
{
    class Program
    {
        private static SerialPort _serialPort = null;
        private static Random _rnd = null;

        private static int _countData = 0;
        private static bool _statusEmulator = true;

        static void Main(string[] args)
        {
            _serialPort = new SerialPort("COM7", 9600);
            _rnd = new Random();

            try
            {
                _serialPort.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Порт успешно открыт. Слушаем данные.");
                Console.WriteLine("");
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

            Console.WriteLine("Нажмите 'SPACE', чтобы изменить статус работы эмулятора.\nДля выхода нажмите 'ESC'.");

            ConsoleKeyInfo keyPressed;
            do
            {
                keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.Spacebar)
                {
                    _statusEmulator = !_statusEmulator;
                    if (_statusEmulator)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Работа эмулятора возобновлена.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Работа эмулятора приостановлена.");
                    }
                }
                 
            } while (keyPressed.Key != ConsoleKey.Escape);

            _serialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var countByte = _serialPort.BytesToRead;
            var data = new byte[_serialPort.BytesToRead];
            _serialPort.Read(data, 0, countByte);

            if ((data.Length != 3) || (_statusEmulator == false)) 
                return;

            if ((data[0] == 0x05) && (data[2] == 0x05))
                switch (data[1])
                {
                    case 0x01:
                        Console.WriteLine("Answer - Acknow");

                        var dataTemp = (22d + _rnd.NextDouble() * 0.1);
                        if (_rnd.NextDouble() < 0.2d)
                            dataTemp += Math.Sin(_countData++) * 0.25;
                            if (_countData > 25)
                                _countData = 0;
                        var temperature = BitConverter.GetBytes((float)dataTemp);
                        var atmosphericPressure = BitConverter.GetBytes((float)(760d + _rnd.NextDouble()));
                        var Humidity = BitConverter.GetBytes((float)(50d + _rnd.NextDouble() * 5d + Math.Sin(_countData++) * 3));

                        var dataAnswer = new List<byte>();

                        dataAnswer.AddRange(new byte[3] { 0x01, 0x01, 0x01 });
                        dataAnswer.AddRange(temperature);
                        dataAnswer.AddRange(atmosphericPressure);
                        dataAnswer.AddRange(Humidity);

                        _serialPort.Write(dataAnswer.ToArray(), 0, dataAnswer.Count);
                        break;

                    case 0x05:
                        Console.WriteLine("Answer - Meteostation-01");

                        _serialPort.WriteLine("Meteostation-01");
                        break;
                }

            Console.Write("DataRecived: ");
            foreach (var item in data)
            {
                Console.Write(item + " ,");
            }
            Console.WriteLine();
        }
    }
}
