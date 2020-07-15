using LUMTask.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUMTask.Domain.Repositories
{
    public interface IRepository<TEntity > where TEntity : class
    {
       
        void Add(TEntity entity);
           
        void Update(string Id,TEntity entity);

        void Remove(TEntity entity);

        TEntity Get(string Id);        
       
        void Complete();

        IEnumerable<TEntity> GetList(GetListRequest request);

    }
}
