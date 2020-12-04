using WeatherStation.BusinessAccess.Sqlite.Managers;
using WeatherStation.BusinessAccess.Sqlite.Managers.Impl;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public class SqliteService : ISqliteService
    {
        public IManagerDeviceRecord DeviceRecord => new ManagerDeviceRecord();
    }
}
