using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    public interface IManagerDeviceRecord : IManagerBase<DeviceRecord>
    {
        void CreateDatabase();
    }
}
