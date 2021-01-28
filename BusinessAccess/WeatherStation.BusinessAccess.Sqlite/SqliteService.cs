using WeatherStation.BusinessAccess.Sqlite.Data;
using WeatherStation.BusinessAccess.Sqlite.Managers;
using WeatherStation.BusinessAccess.Sqlite.Managers.Impl;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public class SqliteService : ISqliteService
    {
        private SqliteContext _sqliteContext;

        public SqliteService(SqliteContext sqliteContext)
        {
            _sqliteContext = sqliteContext;
        }

        public IDeviceRecordRepository DeviceRecord => new DeviceRecordRepository(_sqliteContext);
    }
}
