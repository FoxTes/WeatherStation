using System;
using System.IO.Ports;

namespace WeatherStation.Services.CommunicationService
{
    public class CommunicationService : ICommunicationService
    {
        private SerialPort _serialPort = null;

        public event EventHandler<string> DataRecived;

        public void Close(string namePort)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        public string[] GetNamePorts()
        {
            return SerialPort.GetPortNames();
        }

        public void Open(string namePort)
        {
            _serialPort = new SerialPort(namePort, 9600);

            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
                _serialPort.DataReceived += _serialPort_DataReceived;
            }          
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();

            DataRecived?.Invoke(e, indata);
        }

        public void SendData(byte[] inputArray)
        {         
            if (_serialPort.IsOpen)
                _serialPort.Write(inputArray, 0, inputArray.Length);
        }
    }
}
