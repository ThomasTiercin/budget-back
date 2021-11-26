using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Models;
using Budget.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Controllers
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
        public ActionResult<List<Compte>> GetComptes()
        {
            return _service.GetComptes().ToList();
        }

        [HttpGet("/api/Comptes/{id}")]
        public ActionResult<Compte> GetCompteByID(string id)
        {
            return _service.GetCompteByID(id);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Comptes")]
        public ActionResult<Compte> AddCompte(Compte compte)
        {
            if (string.IsNullOrEmpty(compte.Id))
            {
                compte.Id = new Guid().ToString();
            }
            _service.AddCompte(compte);
            return compte;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Comptes/{id}")]
        public ActionResult<Compte> UpdateCompte(Compte compte)
        {
            _service.UpdateCompte(compte);
            return compte;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Comptes/{id}")]
        public ActionResult<string> DeleteCompte(string id)
        {
            _service.DeleteCompte(id);
            return id;
        }
    }
}
