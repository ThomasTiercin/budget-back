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
    public class MouvementController : ControllerBase
    {
        private IMouvementService _service;

        public MouvementController(IMouvementService service)
        {
            _service = service;

        }

        [HttpGet("/api/Mouvements")]
        public IEnumerable<Mouvement> GetMouvements()
        {
            return _service.GetMouvements().ToList();
        }

        [HttpGet("/api/Mouvements/{id}")]
        public ActionResult<Mouvement> GetMouvementByID(string id)
        {
            var result = _service.GetMouvementByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Mouvements")]
        public ActionResult<Mouvement> AddMouvement(Mouvement mouvement)
        {
            if (string.IsNullOrEmpty(mouvement.Id))
            {
                mouvement.Id = new Guid().ToString();
            }
            _service.AddMouvement(mouvement);
            return mouvement;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Mouvements/{id}")]
        public ActionResult<Mouvement> UpdateMouvement(Mouvement mouvement)
        {
            _service.UpdateMouvement(mouvement);
            return mouvement;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Mouvements/{id}")]
        public ActionResult<string> DeleteMouvement(string id)
        {
            _service.DeleteMouvement(id);
            return id;
        }
    }
}
