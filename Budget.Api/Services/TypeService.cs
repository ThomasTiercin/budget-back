using Budget.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Budget.Api.Services
{
    public class TypeService : ITypeService
    {
        private readonly BudgetDbContext _dbContext;
        public TypeService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteType(Guid Id)
        {
            var type = _dbContext.Type.Find(Id);
            _dbContext.Type.Remove(type);
            Save();
        }

        public Budget.Api.Models.Type GetTypeByID(Guid Id)
        {
            return _dbContext.Type.Find(Id);
        }

        public IEnumerable<Budget.Api.Models.Type> GetTypes()
        {
            return _dbContext.Type.ToList();
        }

        public void AddType(Budget.Api.Models.Type type)
        {
            _dbContext.Add(type);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateType(Budget.Api.Models.Type type)
        {
            _dbContext.Entry(type).State = EntityState.Modified;
            Save();
        }
    }
}
