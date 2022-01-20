using Budget.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Budget.Api.Services
{
    public class CompteService : ICompteService
    {
        private readonly BudgetDbContext _dbContext;
        public CompteService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteCompte(Guid Id)
        {
            var compte = _dbContext.Compte.Find(Id);
            _dbContext.Compte.Remove(compte);
            Save();
        }

        public Compte GetCompteByID(Guid Id)
        {
            return _dbContext.Compte.Find(Id);
        }

        public IEnumerable<Compte> GetCompteByUserId(Guid Id)
        {
            return _dbContext.Compte.Include(r => r.User).Where(b => b.UserId == Id);
        }
        public IEnumerable<Compte> GetComptes()
        {
            return _dbContext.Compte.Include(r => r.User);
        }

        public void AddCompte(Compte compte)
        {
            _dbContext.Add(compte);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCompte(Compte compte)
        {
            _dbContext.Entry(compte).State = EntityState.Modified;
            Save();
        }
    }
}
