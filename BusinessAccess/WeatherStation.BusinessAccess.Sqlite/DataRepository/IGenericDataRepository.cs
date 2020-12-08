using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    /// <summary>
    /// Базовое представление для всех сущностей в БД.
    /// </summary>
    /// <typeparam name="T">Загружаемая сущность.</typeparam>
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
    }
}
