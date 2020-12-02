using WeatherStation.BusinessAccess.Sqlite.Managers;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public interface ISqliteService
    {
        IManagerDeviceDataRecive DeviceDataRecive { get; }
    }
}
