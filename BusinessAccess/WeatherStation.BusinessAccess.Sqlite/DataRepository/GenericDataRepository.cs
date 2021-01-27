using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherStation.BusinessAccess.Sqlite.Data;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            using var context = new SqliteContext();

            var task = Task.Run(() =>
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty);

                return dbQuery;
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

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsNoTracking()
                          .ToList();
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using var context = new SqliteContext();
            IQueryable<T> dbQuery = context.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsNoTracking()
                          .Where(where)
                          .ToList();
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new SqliteContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty);

                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);
            }
            return item;
        }

        public void Add(params T[] items)
        {
            using var context = new SqliteContext();

            foreach (T item in items)
                context.Entry(item).State = EntityState.Added;
            context.SaveChanges();
        }


        public void Remove(params T[] items)
        {
            using var context = new SqliteContext();

            foreach (T item in items)
                context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            using var context = new SqliteContext();

            foreach (T item in items)
                context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
