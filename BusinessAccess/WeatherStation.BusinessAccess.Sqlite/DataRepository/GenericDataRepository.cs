using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeatherStation.BusinessAccess.Sqlite.Database;

namespace WeatherStation.BusinessAccess.Sqlite.DataRepository
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        private readonly SqliteContext _sqliteContext;

        public GenericDataRepository(SqliteContext sqliteContext)
        {
            _sqliteContext = sqliteContext;
        }

        public virtual async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            var task = Task.Run(() =>
            {
                IQueryable<T> dbQuery = _sqliteContext.Set<T>();

                return navigationProperties.Aggregate(dbQuery, (current, navigationProperty) 
                        => current.Include(navigationProperty));
            });

            await Task.WhenAll(task).ConfigureAwait(false);
            var result = task.Result;

            return await result.AsNoTracking()
                               .ToListAsync();
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            using var context = new SqliteContext();
            IQueryable<T> dbQuery = context.Set<T>();

            dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) 
                    => current.Include(navigationProperty));

            return dbQuery.AsNoTracking()
                          .ToList();
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using var context = new SqliteContext();
            IQueryable<T> dbQuery = context.Set<T>();

            dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) 
                    => current.Include(navigationProperty));

            return dbQuery.AsNoTracking()
                          .AsEnumerable()
                          .Where(where)
                          .ToList();
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using var context = new SqliteContext();

            IQueryable<T> dbQuery = context.Set<T>();

            dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) 
                    => current.Include(navigationProperty));

            var item = dbQuery
                .AsNoTracking()
                .FirstOrDefault(@where);
            return item;
        }

        public void Add(params T[] items)
        {
            using var context = new SqliteContext();

            foreach (var item in items)
                context.Entry(item).State = EntityState.Added;
            context.SaveChanges();
        }


        public void Remove(params T[] items)
        {
            using var context = new SqliteContext();

            foreach (var item in items)
                context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            using var context = new SqliteContext();

            foreach (var item in items)
                context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
