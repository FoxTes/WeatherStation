using Prism.Events;

namespace WeatherStation.Core
{
    public class MessageRequest : PubSubEvent<bool>
    {
    }

    public class MessageAnswer : PubSubEvent<string>
    {
    }
}
