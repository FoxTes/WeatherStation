
using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    public interface IManagerDeviceRecord : IManagerBase<DeviceRecord>
    {
        // TODO: Убрать лишнее.
        void CreateDatabase();

        void RecordSaved(DeviceRecord deviceRecord);
    }
}
