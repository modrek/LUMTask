using FluentValidation;
using LUMTask.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUMTask.Domain.Validators
{
    public class MaterialValidator : AbstractValidator<MaterialModel>
    {

        public MaterialValidator()
        {
            RuleFor(x => x.MaterialName).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.Note).NotEmpty();
            RuleFor(x => x.Visible).NotNull();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.MaterialFunction.Min).InclusiveBetween(Consts.MinValue-Consts.Step, Consts.MaxValue+ Consts.Step);
            RuleFor(x => x.MaterialFunction.Max).InclusiveBetween(Consts.MinValue - Consts.Step, Consts.MaxValue + Consts.Step);
            RuleFor(x => x.MaterialFunction.Min).LessThan(x => x.MaterialFunction.Max);
        }



    }
}
