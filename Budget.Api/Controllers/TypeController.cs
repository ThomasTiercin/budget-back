using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private ITypeService _service;

        public TypeController(ITypeService service)
        {
            _service = service;

        }

        [HttpGet("/api/Types")]
        public IEnumerable<Budget.Api.Models.Type> GetTypes()
        {
            return _service.GetTypes().ToList();
        }

        [HttpGet("/api/Types/{id}")]
        public ActionResult<Budget.Api.Models.Type> GetTypeByID(Guid id)
        {
            var result = _service.GetTypeByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Types")]
        public ActionResult<Budget.Api.Models.Type> AddType(Budget.Api.Models.Type type)
        {
            if (type.Id == Guid.Empty)
            {
                type.Id = Guid.NewGuid();
            }
            _service.AddType(type);
            return type;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Types/{id}")]
        public ActionResult<Budget.Api.Models.Type> UpdateType(Budget.Api.Models.Type type)
        {
            _service.UpdateType(type);
            return type;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Types/{id}")]
        public ActionResult<Guid> DeleteType(Guid id)
        {
            _service.DeleteType(id);
            return id;
        }
    }
}
