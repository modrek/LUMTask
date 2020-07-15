using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LUMTask.Domain.Model;
using LUMTask.Domain.Repositories;
using LUMTask.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.IIS.Core;
using Raven.Client.Documents;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUMTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialsController(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        [HttpGet("{id}")]
        public MaterialModel GetById(string id)
        {
            return _materialRepository.Get($"MaterialModels/{id}");
        }

        [HttpGet()]
        public MaterialModel GetByName(string name)
        {
            return _materialRepository.GetByName(name);
        }

        // POST api/<MaterialController>
        [HttpPost]
        public IActionResult Post([FromBody] MaterialModel model)
        {
            if (!ModelState.IsValid)           // Invokes the build-in
                return BadRequest(ModelState); //

            Validate(model);

            if (!ModelState.IsValid)
                return Ok(String.Join("\n", ModelState.Values.Where(x => x.ValidationState == ModelValidationState.Invalid).Select(x => x.Errors[0].ErrorMessage).ToList()));

            _materialRepository.Add(model);
            _materialRepository.Complete();

            return Ok("Material added successfully.");
        }



        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] MaterialModel model)
        {
            Validate(model);

            if (!ModelState.IsValid)
                return Ok(String.Join("\n", ModelState.Values.Where(x => x.ValidationState == ModelValidationState.Invalid).Select(x => x.Errors[0].ErrorMessage).ToList()));


            _materialRepository.Update($"MaterialModels/{id}", model);
            _materialRepository.Complete();

            return Ok("Materia updated successfully.");
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = _materialRepository.Get($"MaterialModels/{id}");
            if (entity == null)
                throw new Exception("Material Not Found");
            _materialRepository.Remove(entity);
            _materialRepository.Complete();
        }

        private void Validate(MaterialModel model)
        {
            if (string.IsNullOrEmpty(model.MaterialName))
                ModelState.AddModelError("MaterialName", "MaterialName is required.");

            if (string.IsNullOrEmpty(model.Author))
                ModelState.AddModelError("Author", "Author is required.");

            if (string.IsNullOrEmpty(model.Note))
                ModelState.AddModelError("Note", "Note is required.");

            if (!Enum.IsDefined(typeof(TypeOfPhase), model.Type))
                ModelState.AddModelError("Type", "Type must be ......");

            if (model.Visible == null)
                ModelState.AddModelError("Visible", "Visible is required.");


            if (!Between(model.MaterialFunction.Min))
                ModelState.AddModelError("MaterialFunction.Min", "MaterialFunction.Min must be in valid range.");

            if (!Between(model.MaterialFunction.Max))
                ModelState.AddModelError("MaterialFunction.Max", "MaterialFunction.Max must be in valid range.");
        }
        public static bool Between(float number)
        {
            return number >= (Consts.MinValue - Consts.Step) && number <= (Consts.MaxValue +  Consts.Step);
        }
    }
      
}
