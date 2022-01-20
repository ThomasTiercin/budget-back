using Budget.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Budget.Api.Services
{
    public class MouvementService : IMouvementService
    {
        private readonly BudgetDbContext _dbContext;
        public MouvementService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteMouvement(Guid Id)
        {
            var mouvement = _dbContext.Mouvement.Find(Id);
            _dbContext.Mouvement.Remove(mouvement);
            Save();
        }

        public Mouvement GetMouvementByID(Guid Id)
        {
            return _dbContext.Mouvement.Find(Id);
        }

        public IEnumerable<Mouvement> GetMouvements()
        {
            return _dbContext.Mouvement.Include(r => r.User).Include(r => r.Categorie).Include(r => r.Compte).Include(r => r.Organisme).Include(r => r.Echeance).ThenInclude(c => c.Type);
        }

        public IEnumerable<Mouvement> GetMouvementByUserId(Guid Id)
        {
            return _dbContext.Mouvement.Include(r => r.User).Include(r => r.Categorie).Include(r => r.Compte).Include(r => r.Organisme).Include(r => r.Echeance).ThenInclude(c => c.Type).Where(b => b.UserId == Id);
        }
        public void AddMouvement(Mouvement mouvement)
        {
            _dbContext.Add(mouvement);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateMouvement(Mouvement mouvement)
        {
            _dbContext.Entry(mouvement).State = EntityState.Modified;
            Save();
        }
    }
}
