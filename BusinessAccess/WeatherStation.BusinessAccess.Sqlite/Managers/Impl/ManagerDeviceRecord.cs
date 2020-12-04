using System.Collections.Generic;
using Dapper;
using WeatherStation.BusinessAccess.Sqlite.Data;
using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.BusinessAccess.Sqlite.Managers.Impl
{
    internal class ManagerDeviceRecord : SqLiteBaseRepository, IManagerDeviceRecord
    {
        public bool AddRangeRecord(List<DeviceRecord> models)
        {
            throw new System.NotImplementedException();
        }

        public bool AdRecordd(DeviceRecord model)
        {
            throw new System.NotImplementedException();
        }

        public void CreateDatabase()
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            cnn.Execute(
                @"create table DeviceRecord
                (
                    Id                                  integer primary key AUTOINCREMENT,
                    Temperature                         integer
                )");
        }

        public bool DeleteRecord(DeviceRecord model)
        {
            throw new System.NotImplementedException();
        }

        public List<DeviceRecord> GetAllRecord()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateRecord(DeviceRecord model)
        {
            throw new System.NotImplementedException();
        }
    }
}
