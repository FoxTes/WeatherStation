using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStation.Services.CommunicationService
{
    public class CommunicationService : ICommunicationService
    {
        private SerialPort _serialPort = null;

        public event EventHandler<string> DataRecived;
        public void OpenPort(string namePort)
        {
            _serialPort = new SerialPort(namePort, 9600);
            _serialPort.WriteTimeout = 1000;

            _serialPort.Open();
            _serialPort.DataReceived += _serialPort_DataReceived;        
        }

        public void ClosePort(string namePort)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        public string[] GetNamePorts()
        {
            return SerialPort.GetPortNames();
        }

        public void SendData(byte[] inputArray)
        {
            if (_serialPort.IsOpen)
                _serialPort.Write(inputArray, 0, inputArray.Length);
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();

            DataRecived?.Invoke(e, indata);
        }

        public async Task<byte[]> ConnectDeviceAsync(CancellationToken ct)
        {
            byte[] dataReturn = null;

            await Task.Run(() =>
            {
                foreach (var item in SerialPort.GetPortNames())
                {
                    _serialPort = new SerialPort(item, 9600);
                    _serialPort.WriteTimeout = 1000;

                    bool isError = false;
                    try
                    {
                        _serialPort.Open();
                    }
                    catch (Exception)
                    {
                        isError = true;
                    }

                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }

                    if (!isError)
                    {
                        var dataSend = new byte[3] { 0x05, 0x05, 0x05 };
                        try
                        {
                            _serialPort.DiscardInBuffer();
                            _serialPort.Write(dataSend, 0, dataSend.Length);
                            Thread.Sleep(1000);
                        }
                        catch
                        {
                            isError = true;
                        }
                    }

                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }

                    if (!isError)
                    {
                        var countByte = _serialPort.BytesToRead;
                        var data = new byte[_serialPort.BytesToRead];
                        _serialPort.Read(data, 0, countByte);

                        if (countByte > 0)
                        {
                            dataReturn = data;
                            break;
                        }
                    }
                }
            });
            return dataReturn;
        }

    }
}
