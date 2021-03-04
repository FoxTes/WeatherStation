using WeatherStation.BusinessAccess.Sqlite.Database;
using WeatherStation.BusinessAccess.Sqlite.DataRepository.Impl;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public class SqliteService : ISqliteService
    {
        public IDeviceRecordRepository DeviceRecord => new DeviceRecordRepository();
    }
}
