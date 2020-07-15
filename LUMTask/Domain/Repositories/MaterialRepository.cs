using LUMTask.Domain.Model;
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
    public class MaterialRepository : Repository<MaterialModel>, IMaterialRepository
    {

        private IDocumentStoreHolder _session;
        public MaterialRepository(IDocumentStoreHolder session) :base(session)
        {
            _session = session;
        }

        // special method for Material
        public MaterialModel GetByName(string materialName)
        {            
            materialName = $"*{materialName}*";
            return _session.GetSession().Query<MaterialModel>().Search(x => x.MaterialName, materialName).FirstOrDefault();
        }

       
    }
}
