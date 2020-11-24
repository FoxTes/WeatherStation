using System;

namespace WeatherStation.Services.CommunicationService
{
    public interface ICommunicationService
    {
        event EventHandler<string> DataRecived;

        void Open(string namePort);

        void Close(string namePort);

        void SendData(byte[] inputArray);

        string[] GetNamePorts();
    }
}
