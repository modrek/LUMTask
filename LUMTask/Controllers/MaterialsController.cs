using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LUMTask.Domain.Model;
using LUMTask.Domain.Repositories;
using LUMTask.Helpers;
using LUMTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.IIS.Core;
using Raven.Client.Documents;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUMTask.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    //[Authorize]
    //[EnableCors("AllowOrigin")]
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
            return _materialRepository.Get(id);
        }

        [HttpGet()]
        public IEnumerable< MaterialModel> GetByName(string name)
        {
            return _materialRepository.GetByName(name);
        }

        // POST api/<MaterialController>
        [HttpPost]
        public IActionResult Post([FromBody] MaterialModel model)
        {

            _materialRepository.Add(model);
            _materialRepository.Complete();

            return Ok("Material added successfully.");
        }


        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] MaterialModel model)
        {
            var entity = _materialRepository.Get(id);
            if (entity == null)
                return NotFound();

            _materialRepository.Update(id, model);
            _materialRepository.Complete();

            return Ok("Materia updated successfully.");
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var entity = _materialRepository.Get(id);
            if (entity == null)
                return NotFound() ;
            _materialRepository.Remove(entity);
            _materialRepository.Complete();
            return Ok("Material deleted successfully.");
        }

        
    }
      
}
