using Bankbank.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;
        public readonly AppDbContext context;

        //Initialize 2 obj dbset and context.
        public Repository()
        {
            this.context = new AppDbContext();
            this.dbSet = this.context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
            context.SaveChanges();

        }
        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Update(entity);
            context.SaveChanges();
            return true;

        }
        public bool Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }
        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().FirstOrDefault();

        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
