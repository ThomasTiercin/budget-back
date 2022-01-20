using Budget.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Budget.Api.Services
{
    public class OrganismeService : IOrganismeService
    {
        private readonly BudgetDbContext _dbContext;
        public OrganismeService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteOrganisme(Guid Id)
        {
            var organisme = _dbContext.Organisme.Find(Id);
            _dbContext.Organisme.Remove(organisme);
            Save();
        }

        public Organisme GetOrganismeByID(Guid Id)
        {
            return _dbContext.Organisme.Find(Id);
        }

        public IEnumerable<Organisme> GetOrganismes()
        {
            return _dbContext.Organisme.ToList();
        }

        public void AddOrganisme(Organisme organisme)
        {
            _dbContext.Add(organisme);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateOrganisme(Organisme organisme)
        {
            _dbContext.Entry(organisme).State = EntityState.Modified;
            Save();
        }
    }
}
