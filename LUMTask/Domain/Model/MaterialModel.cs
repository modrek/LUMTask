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
        
        //[Display(Name = "Material Name")]
        //[Required(ErrorMessageResourceName = "PropertyValueRequired"), StringLength(100, ErrorMessageResourceName = "PropertyValueLength")]
        public string MaterialName { get; set; }

        //[Display(Name = "Author")]
        //[Required(ErrorMessageResourceName = "PropertyValueRequired"), StringLength(200, ErrorMessageResourceName = "PropertyValueLength")]
        public string Author { get; set; }

        //[Display(Name = "Material visible")]
        //[Required(ErrorMessageResourceName = "PropertyValueRequired")]
        public bool Visible { get; set; }

        //[Display(Name = "Type of phase")]
        //[Required(ErrorMessageResourceName = "PropertyValueRequired")]
        public TypeOfPhase Type { get; set; }


        //[Display(Name = "Note")]
        //[Required(ErrorMessageResourceName = "PropertyValueRequired")]
        public string Note{ get; set; }


        //Material Function Model Characteristics
        //•	Max Temperature
        //•	Min Temperature
        //Max and min temeparature can be anything between 4° and 80° including 0.1 steps. Please choose an appropriate data type.
        //[Display(Name = "Material Function")]
        public MaterialFunctionModel MaterialFunction { get; set; }

    }

    public class MaterialFunctionModel
    {
        public float Min { get; set; }
        public float Max { get; set; }
    }
}
