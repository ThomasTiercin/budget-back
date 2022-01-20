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
    public class CompteController : ControllerBase
    {
        private ICompteService _service;

        public CompteController(ICompteService service)
        {
            _service = service;

        }

        [HttpGet("/api/Comptes")]
        public IEnumerable<Compte> GetComptes()
        {
            return _service.GetComptes().ToList();
        }

        [HttpGet("/api/Comptes/{id}")]
        public ActionResult<Compte> GetCompteByID(Guid id)
        {
            var result = _service.GetCompteByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        
        [HttpPost("/api/Comptes")]
        public ActionResult<Compte> AddCompte(Compte compte)
        {
            if (compte.Id == Guid.Empty)
            {
                compte.Id = Guid.NewGuid();
            }
            _service.AddCompte(compte);
            return compte;
        }
        [HttpGet("/api/User/{id}/Comptes")]
        public ActionResult<List<Compte>> GetCompteByUserId(Guid id)
        {
            return _service.GetCompteByUserId(id).ToList();
        }
        
        [HttpPut("/api/Comptes/{id}")]
        public ActionResult<Compte> UpdateCompte(Compte compte)
        {
            _service.UpdateCompte(compte);
            return compte;
        }
        
        [HttpDelete("/api/Comptes/{id}")]
        public ActionResult<Guid> DeleteCompte(Guid id)
        {
            _service.DeleteCompte(id);
            return id;
        }
    }
}
