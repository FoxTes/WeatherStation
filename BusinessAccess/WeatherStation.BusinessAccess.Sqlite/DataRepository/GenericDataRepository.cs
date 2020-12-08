using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WeatherStation.BusinessAccess.Sqlite.Data;

namespace WeatherStation.BusinessAccess.Sqlite.Managers
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
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
