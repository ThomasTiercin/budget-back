﻿using Budget.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Services
{
    public class MouvementService : IMouvementService
    {
        private readonly BudgetDbContext _dbContext;
        public MouvementService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteMouvement(string Id)
        {
            var mouvement = _dbContext.Mouvement.Find(Id);
            _dbContext.Mouvement.Remove(mouvement);
            Save();
        }

        public Mouvement GetMouvementByID(string Id)
        {
            return _dbContext.Mouvement.Find(Id);
        }

        public IEnumerable<Mouvement> GetMouvements()
        {
            return _dbContext.Mouvement.Include(r => r.Categorie).Include(r => r.Compte).Include(r => r.Organisme).Include(r => r.Echeance).ThenInclude(c => c.Type);
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
