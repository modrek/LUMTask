
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUMTask.Domain.Model
{
    public class GetListRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Expression { get; set; }
    }


}
