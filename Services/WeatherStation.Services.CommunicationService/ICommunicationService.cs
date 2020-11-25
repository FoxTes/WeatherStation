using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStation.Services.CommunicationService
{
    public interface ICommunicationService
    {
        event EventHandler<string> DataRecived;

        void OpenPort(string namePort);

        void ClosePort(string namePort);

        void SendData(byte[] inputArray);

        string[] GetNamePorts();

        Task<byte[]> ConnectDeviceAsync(CancellationToken ct);
    }
}
