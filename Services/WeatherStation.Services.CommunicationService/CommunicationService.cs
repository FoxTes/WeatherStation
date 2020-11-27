﻿using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Services.CommunicationService.Data;
using WeatherStation.Services.CommunicationService.Enum;

namespace WeatherStation.Services.CommunicationService
{
    public class CommunicationService : ICommunicationService
    {
        #region Event
        public event EventHandler<ConnectionStatus> ConnectionChanged;
        public event EventHandler<DataModel> DataRecived;
        #endregion

        #region Filed
        private Timer _timer = null;
        private SerialPort _serialPort = null;
        #endregion

        #region Property
        public bool IsConnected { get; private set; }
        #endregion

        #region Constructor
        public CommunicationService()
        {
            _timer = new Timer(new TimerCallback(OnTimerEven), null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region Method
        private async void OnTimerEven(object state)
        {
            var bufferData = new byte[3] { 0x05, 0x01, 0x05 };
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
                var dataToSend = new DataModel { MainTemperature = BitConverter.ToSingle(data, 3) };
                DataRecived?.Invoke(this, dataToSend);
            }
        }

        public async Task<string> SeachDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null)
        {
            foreach (var namePort in SerialPort.GetPortNames())
            {
                _serialPort = new SerialPort(namePort)
                {
                    WriteTimeout = 100
                };

                try
                {
                    if (!_serialPort.IsOpen)
                        _serialPort.Open();

                    try
                    {
                        var bufferData = new byte[3] { 0x05, 0x05, 0x05 };
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
                    }
                    catch (Exception)
                    {
                        await Task.Delay(1000, cancellationToken);
                    }
                }
                catch (Exception)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
            return null;
        }

        public async Task<bool> SeachDeviceAsync(CancellationToken cancellationToken, Action<string> callBack, IProgress<byte> progress = null)
        {
            foreach (var namePort in SerialPort.GetPortNames())
            {
                _serialPort = new SerialPort(namePort)
                {
                    WriteTimeout = 100
                };

                try
                {
                    if (!_serialPort.IsOpen)
                        _serialPort.Open();

                    try
                    {
                        var bufferData = new byte[3] { 0x05, 0x05, 0x05 };
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

                            callBack?.Invoke(System.Text.Encoding.Default.GetString(data));
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        await Task.Delay(1000, cancellationToken);
                    }
                }
                catch (Exception)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
            return false;
        }
        #endregion
    }
}
