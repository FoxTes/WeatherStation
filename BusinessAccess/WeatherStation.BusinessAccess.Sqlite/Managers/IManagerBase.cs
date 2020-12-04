using System.Collections.Generic;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    /// <summary>
    /// Базовое представление для всех сущностей в БД.
    /// </summary>
    /// <typeparam name="TModel">Загружаемая сущность.</typeparam>
    public interface IManagerBase<TModel>
    {
        // TODO: Подумать над структурой.
        List<TModel> GetAllRecord();
        bool AdRecordd(TModel model);
        bool AddRangeRecord(List<TModel> models);
        bool UpdateRecord(TModel model);
        bool DeleteRecord(TModel model);
    }
}
