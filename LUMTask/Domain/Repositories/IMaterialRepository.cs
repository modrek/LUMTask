using LUMTask.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUMTask.Domain.Repositories
{
    public interface IMaterialRepository : IRepository<MaterialModel>
    {
        // special method for Material
        IEnumerable< MaterialModel> GetByName(string MaterialName);
        
    }
}
