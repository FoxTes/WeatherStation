using System.Collections.Generic;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    public interface IManagerBase<TModel>
    {
        List<TModel> GetAllRecord();
        bool AdRecordd(TModel model);
        bool AddRangeRecord(List<TModel> models);
        bool UpdateRecord(TModel model);
        bool DeleteRecord(TModel model);
    }
}
