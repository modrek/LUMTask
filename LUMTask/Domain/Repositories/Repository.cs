using LUMTask.Domain.Model;
using LUMTask.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Queries;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUMTask.Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private IDocumentStoreHolder _documentStoreHolder;

        public Repository(IDocumentStoreHolder documentStoreHolder)
        {
            _documentStoreHolder = documentStoreHolder;
        }

       

        public void Add(TEntity entity)
        {
            _documentStoreHolder.GetSession().Store(entity);           
        }

        
        public void Complete()
        {
            _documentStoreHolder.GetSession().SaveChanges();

        }

        public TEntity Get(string Id)
        {
            return _documentStoreHolder.GetSession().Load<TEntity>(Id);            
        }

        public IEnumerable<TEntity> GetList(GetListRequest request)
        {
            return _documentStoreHolder.GetSession().Query<TEntity>()
                .Take(request.PageSize).Skip((request.PageIndex - 1) * request.PageSize).ToList();
            
        }

        public void Remove(TEntity entity)
        {
            _documentStoreHolder.GetSession().Delete(entity);
        }

        public void Update(string Id ,TEntity entity)
        {
            var session= _documentStoreHolder.GetSession();
            
            var model = session.Load<TEntity>(Id);

            GeneralMethods.CopyAllClassProp(entity, model);


            session.SaveChanges();
        }

        

    }
}