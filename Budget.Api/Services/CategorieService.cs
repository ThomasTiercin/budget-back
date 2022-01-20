using Budget.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Budget.Api.Services
{
    public class CategorieService : ICategorieService
    {
        private readonly BudgetDbContext _dbContext;
        public CategorieService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteCategorie(Guid Id)
        {
            var categorie = _dbContext.Categorie.Find(Id);
            _dbContext.Categorie.Remove(categorie);
            Save();
        }

        public Categorie GetCategorieByID(Guid Id)
        {
            return _dbContext.Categorie.Find(Id);
        }

        public IEnumerable<Categorie> GetCategories()
        {
            return _dbContext.Categorie.ToList();
        }

        public void AddCategorie(Categorie categorie)
        {
            _dbContext.Add(categorie);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCategorie(Categorie categorie)
        {
            _dbContext.Entry(categorie).State = EntityState.Modified;
            Save();
        }
    }
}
