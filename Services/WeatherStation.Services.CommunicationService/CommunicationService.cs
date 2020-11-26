using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStation.Services.CommunicationService
{
    public class CommunicationService : ICommunicationService
    {
        public async Task<string> SeachDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null)
        {
            foreach (var namePort in SerialPort.GetPortNames())
            {
                var serialPort = new SerialPort(namePort)
                {
                    WriteTimeout = 100
                };

                try
                {
                    if (!serialPort.IsOpen)
                        serialPort.Open();

                    try
                    {
                        var bufferData = new byte[3] { 0x05, 0x05, 0x05 };
                        serialPort.Write(bufferData, 0, bufferData.Length);

                        await Task.Delay(1000, cancellationToken);

                        var countByte = serialPort.BytesToRead;
                        var data = new byte[countByte];
                        serialPort.Read(data, 0, countByte);

                        if (countByte > 0)
                            return System.Text.Encoding.Default.GetString(data);
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
    }
}
