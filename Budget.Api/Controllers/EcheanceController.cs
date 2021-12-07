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
    public class EcheanceController : ControllerBase
    {
        private IEcheanceService _service;

        public EcheanceController(IEcheanceService service)
        {
            _service = service;

        }

        [HttpGet("/api/Echeances")]
        public IEnumerable<Echeance> GetEcheances()
        {
            return _service.GetEcheances().ToList();
        }

        [HttpGet("/api/Echeances/{id}")]
        public ActionResult<Echeance> GetEcheanceByID(string id)
        {
            var result = _service.GetEcheanceByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Echeances")]
        public ActionResult<Echeance> AddEcheance(Echeance echeance)
        {
            if (string.IsNullOrEmpty(echeance.Id))
            {
                echeance.Id = new Guid().ToString();
            }
            _service.AddEcheance(echeance);
            return echeance;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Echeances/{id}")]
        public ActionResult<Echeance> UpdateEcheance(Echeance echeance)
        {
            _service.UpdateEcheance(echeance);
            return echeance;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Echeances/{id}")]
        public ActionResult<string> DeleteEcheance(string id)
        {
            _service.DeleteEcheance(id);
            return id;
        }
    }
}
