using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Services.Communication.Enum;
using WeatherStation.Services.Communication.Model;

namespace WeatherStation.Services.Communication
{
    public class CommunicationService : ICommunicationService
    {
        #region Event
        public event EventHandler<ConnectionStatus> ConnectionChanged;
        public event EventHandler<DataReceiveEventArgs> DataReceived;
        #endregion

        #region Filed
        private readonly Timer _timer;
        private SerialPort _serialPort;
        #endregion

        #region Property
        public bool IsConnected { get; private set; }
        #endregion

        #region Constructor
        public CommunicationService()
        {
            _timer = new Timer(OnTimerEven, null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region Method
        private async void OnTimerEven(object state)
        {
            var bufferData = new byte[] { 0x05, 0x01, 0x05 };
            _serialPort.Write(bufferData, 0, bufferData.Length);

            await Task.Delay(100);

            var countByte = _serialPort.BytesToRead;
            var data = new byte[countByte];
            _serialPort.Read(data, 0, countByte);

            if (countByte < 3)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _serialPort.Close();

                IsConnected = false;
                ConnectionChanged?.Invoke(this, ConnectionStatus.Disconnect);
            }
            else
            {
                var dataToSend = new DataReceiveEventArgs
                { 
                    Temperature = BitConverter.ToSingle(data, 3), 
                    AtmosphericPressure = BitConverter.ToSingle(data, 7),
                    Humidity = BitConverter.ToSingle(data, 11),
                };
                DataReceived?.Invoke(this, dataToSend);
            }
        }

        public async Task<string> SearchDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null)
        {
            foreach (var namePort in SerialPort.GetPortNames())
            {
                _serialPort = new SerialPort(namePort)
                {
                    WriteTimeout = 100
                };

                try
                {
                    _serialPort.Open();

                    var bufferData = new byte[] { 0x05, 0x05, 0x05 };
                    _serialPort.DiscardInBuffer();
                    _serialPort.Write(bufferData, 0, bufferData.Length);

                    await Task.Delay(1000, cancellationToken);

                    var countByte = _serialPort.BytesToRead;
                    var data = new byte[countByte];
                    _serialPort.Read(data, 0, countByte);

                    if (countByte > 0)
                    {
                        _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

                        IsConnected = true;
                        ConnectionChanged?.Invoke(this, ConnectionStatus.Connect);
                        return System.Text.Encoding.Default.GetString(data);
                    }

                    _serialPort.Close();
                }
                catch (OperationCanceledException)
                {
                    if (_serialPort.IsOpen)
                        _serialPort.Close();
                }
                catch (Exception)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }

            ConnectionChanged?.Invoke(this, ConnectionStatus.Disconnect);
            return null;
        }
        #endregion
    }
}
