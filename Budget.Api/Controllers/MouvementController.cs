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
        public ActionResult<Mouvement> GetMouvementByID(Guid id)
        {
            var result = _service.GetMouvementByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        
        [HttpPost("/api/Mouvements")]
        public ActionResult<Mouvement> AddMouvement(Mouvement mouvement)
        {
            if (mouvement.Id == Guid.Empty)
            {
                mouvement.Id = Guid.NewGuid();
            }
            _service.AddMouvement(mouvement);
            return mouvement;
        }
        [HttpGet("/api/User/{id}/Mouvements")]
        public ActionResult<List<Mouvement>> GetMouvementByUserId(Guid id)
        {
            return _service.GetMouvementByUserId(id).ToList();
        }
        
        [HttpPut("/api/Mouvements/{id}")]
        public ActionResult<Mouvement> UpdateMouvement(Mouvement mouvement)
        {
            _service.UpdateMouvement(mouvement);
            return mouvement;
        }
        
        [HttpDelete("/api/Mouvements/{id}")]
        public ActionResult<Guid> DeleteMouvement(Guid id)
        {
            _service.DeleteMouvement(id);
            return id;
        }
    }
}
