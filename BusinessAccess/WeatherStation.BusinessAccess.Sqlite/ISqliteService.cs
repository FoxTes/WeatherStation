using WeatherStation.BusinessAccess.Sqlite.DataRepository.Impl;

namespace WeatherStation.BusinessAccess.Sqlite
{
    public interface ISqliteService
    {
        IDeviceRecordRepository DeviceRecord { get; }
    }
}
