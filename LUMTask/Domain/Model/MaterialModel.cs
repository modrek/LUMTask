using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUMTask.Domain.Model
{
    public class MaterialModel
    {
        string _Id;
        public string Id
        {
            get
            {//for prevent auto generte by RavenDB
                if (string.IsNullOrEmpty(_Id))
                    return Guid.NewGuid().ToString();
                else
                    return _Id;
            }

            set
            {
                _Id = value;
            }
        }
        public string MaterialName { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        public TypeOfPhase Type { get; set; }

        public string Note{ get; set; }

        public MaterialFunctionModel MaterialFunction { get; set; }

    }

    public class MaterialFunctionModel
    {
        public float Min { get; set; }
        public float Max { get; set; }
    }
}
