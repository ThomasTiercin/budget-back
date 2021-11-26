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
    public class MouvementController : ControllerBase
    {
        private IMouvementService _service;

        public MouvementController(IMouvementService service)
        {
            _service = service;

        }

        [HttpGet("/api/Mouvements")]
        public ActionResult<List<Mouvement>> GetMouvements()
        {
            return _service.GetMouvements().ToList();
        }

        [HttpGet("/api/Mouvements/{id}")]
        public ActionResult<Mouvement> GetMouvementByID(string id)
        {
            return _service.GetMouvementByID(id);
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
