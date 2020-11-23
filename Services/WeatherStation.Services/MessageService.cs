using WeatherStation.Services.Interfaces;

namespace WeatherStation.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
