using Budget.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Services
{
    public class EcheanceService : IEcheanceService
    {
        private readonly BudgetDbContext _dbContext;
        public EcheanceService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteEcheance(string Id)
        {
            var echeance = _dbContext.Echeance.Find(Id);
            _dbContext.Echeance.Remove(echeance);
            Save();
        }

        public Echeance GetEcheanceByID(string Id)
        {
            return _dbContext.Echeance.Include(b => b.Type).FirstOrDefault(b => b.Id == Id);
        }

        public IEnumerable<Echeance> GetEcheances()
        {
            return _dbContext.Echeance.Include(r => r.Type);
        }

        public void AddEcheance(Echeance echeance)
        {
            _dbContext.Add(echeance);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateEcheance(Echeance echeance)
        {
            _dbContext.Entry(echeance).State = EntityState.Modified;
            Save();
        }
    }
}
