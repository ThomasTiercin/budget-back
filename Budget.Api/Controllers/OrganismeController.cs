using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Api.Models;
using Budget.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganismeController : ControllerBase
    {
        private IOrganismeService _service;

        public OrganismeController(IOrganismeService service)
        {
            _service = service;

        }

        [HttpGet("/api/Organismes")]
        public IEnumerable<Organisme> GetOrganismes()
        {
            return _service.GetOrganismes();
        }

        [HttpGet("/api/Organismes/{id}")]
        public ActionResult<Organisme> GetOrganismeByID(Guid id)
        {
            var result = _service.GetOrganismeByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Organismes")]
        public ActionResult<Organisme> AddOrganisme(Organisme organisme)
        {
            if (organisme.Id == Guid.Empty)
            {
                organisme.Id = Guid.NewGuid();
            }
            _service.AddOrganisme(organisme);
            return organisme;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Organismes/{id}")]
        public ActionResult<Organisme> UpdateOrganisme(Organisme organisme)
        {
            _service.UpdateOrganisme(organisme);
            return organisme;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Organismes/{id}")]
        public ActionResult<Guid> DeleteOrganisme(Guid id)
        {
            _service.DeleteOrganisme(id);
            return id;
        }
    }
}
