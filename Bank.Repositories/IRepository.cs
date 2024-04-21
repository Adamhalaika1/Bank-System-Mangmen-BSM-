using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        IQueryable<TEntity> GetAll();//Out memory query
        //IEnumerable<TEntity> GetAll2();//In memory query
        TEntity GetById(int id);





    }
}
