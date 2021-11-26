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
    public class EcheanceController : ControllerBase
    {
        private IEcheanceService _service;

        public EcheanceController(IEcheanceService service)
        {
            _service = service;

        }

        [HttpGet("/api/Echeances")]
        public ActionResult<List<Echeance>> GetEcheances()
        {
            return _service.GetEcheances().ToList();
        }

        [HttpGet("/api/Echeances/{id}")]
        public ActionResult<Echeance> GetEcheanceByID(string id)
        {
            return _service.GetEcheanceByID(id);
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
