using System;
using WeatherStation.BusinessAccess.Sqlite.Managers;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public class SqliteService : ISqliteService
    {
        public IManagerDeviceDataRecive DeviceDataRecive => throw new NotImplementedException();
    }
}
