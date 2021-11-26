using Budget.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Services
{
    public class CompteService : ICompteService
    {
        private readonly BudgetDbContext _dbContext;
        public CompteService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteCompte(string Id)
        {
            var compte = _dbContext.Compte.Find(Id);
            _dbContext.Compte.Remove(compte);
            Save();
        }

        public Compte GetCompteByID(string Id)
        {
            return _dbContext.Compte.Find(Id);
        }

        public IEnumerable<Compte> GetComptes()
        {
            return _dbContext.Compte.ToList();
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
