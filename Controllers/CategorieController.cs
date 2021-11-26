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
    public class CategorieController : ControllerBase
    {
        private ICategorieService _service;

        public CategorieController(ICategorieService service)
        {
            _service = service;

        }

        [HttpGet("/api/Categories")]
        public ActionResult<List<Categorie>> GetCategories()
        {
            return _service.GetCategories().ToList();
        }

        [HttpGet("/api/Categories/{id}")]
        public ActionResult<Categorie> GetCategorieByID(string id)
        {
            return _service.GetCategorieByID(id);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Categories")]
        public ActionResult<Categorie> AddCategorie(Categorie categorie)
        {
            if (string.IsNullOrEmpty(categorie.Id))
            {
                categorie.Id = new Guid().ToString();
            }
            _service.AddCategorie(categorie);
            return categorie;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Categories/{id}")]
        public ActionResult<Categorie> UpdateCategorie(Categorie categorie)
        {
            _service.UpdateCategorie(categorie);
            return categorie;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Categories/{id}")]
        public ActionResult<string> DeleteCategorie(string id)
        {
            _service.DeleteCategorie(id);
            return id;
        }
    }
}
