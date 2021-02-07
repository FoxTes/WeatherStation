using WeatherStation.BusinessAccess.Sqlite.Database;
using WeatherStation.BusinessAccess.Sqlite.DataRepository.Impl;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public class SqliteService : ISqliteService
    {
        private readonly SqliteContext _sqliteContext;

        public SqliteService(SqliteContext sqliteContext)
        {
            _sqliteContext = sqliteContext;
        }

        public IDeviceRecordRepository DeviceRecord => new DeviceRecordRepository(_sqliteContext);
    }
}
