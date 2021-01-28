using WeatherStation.BusinessAccess.Sqlite.Data;
using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.BusinessAccess.Sqlite.Managers.Impl
{
    internal class DeviceRecordRepository : GenericDataRepository<DeviceRecord>, IDeviceRecordRepository 
    {
        public DeviceRecordRepository(SqliteContext sqliteContext) : base (sqliteContext)
        {

        }
    }
}
