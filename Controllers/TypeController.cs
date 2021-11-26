using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Models;
using Budget.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Budget.Controllers
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
        public ActionResult<List<Budget.Models.Type>> GetTypes()
        {
            return _service.GetTypes().ToList();
        }

        [HttpGet("/api/Types/{id}")]
        public ActionResult<Budget.Models.Type> GetTypeByID(string id)
        {
            return _service.GetTypeByID(id);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Types")]
        public ActionResult<Budget.Models.Type> AddType(Budget.Models.Type type)
        {
            if (string.IsNullOrEmpty(type.Id))
            {
                type.Id = new Guid().ToString();
            }
            _service.AddType(type);
            return type;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Types/{id}")]
        public ActionResult<Budget.Models.Type> UpdateType(Budget.Models.Type type)
        {
            _service.UpdateType(type);
            return type;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Types/{id}")]
        public ActionResult<string> DeleteType(string id)
        {
            _service.DeleteType(id);
            return id;
        }
    }
}
