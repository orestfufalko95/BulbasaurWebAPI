using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BulbasaurWebAPI.dal.Interface;

namespace BulbasaurWebAPI.dal.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(params object[] id)
        {
            return Find(Context.Set<TEntity>(), id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            // Note that here I've repeated Context.Set<TEntity>() in every method and this is causing
            // too much noise. I could get a reference to the DbSet returned from this method in the 
            // constructor and store it in a private field like _entities. This way, the implementation
            // of our methods would be cleaner:
            // 
            // _entities.ToList();
            // _entities.Where();
            // _entities.SingleOrDefault();
            // 
            // I didn't change it because I wanted the code to look like the videos. But feel free to change
            // this on your own.
            return Context.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
       
        public  TEntity Find<TEntity>( DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            //var context = set.GetInfrastructure<IServiceProvider>().GetService<IDbContextServices>().CurrentContext.Context;
            var context = Context;
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var keys = entityType.GetKeys();
            var entries = context.ChangeTracker.Entries<TEntity>();
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            IQueryable<TEntity> query = context.Set<TEntity>();

            //first, check if the entity exists in the cache
            var i = 0;

            //iterate through the key properties
            foreach (var property in keys.SelectMany(x => x.Properties))
            {
                var keyValue = keyValues[i];

                //try to get the entity from the local cache
                entries = entries.Where(e => keyValue.Equals(e.Property(property.Name).CurrentValue));

                //build a LINQ expression for loading the entity from the store
                var expression = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, property.Name),
                            Expression.Constant(keyValue)),
                        parameter) as Expression<Func<TEntity, bool>>;

                query = query.Where(expression);

                i++;
            }

            var entity = entries.Select(x => x.Entity).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }

            //second, try to load the entity from the data store
            entity = query.FirstOrDefault();

            return entity;
        }
    }
}