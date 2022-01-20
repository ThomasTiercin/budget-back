using Budget.Api.Models;
using System.Collections.Generic;
using System;
namespace Budget.Api.Services
{
    public interface ITypeService
    {
        IEnumerable<Budget.Api.Models.Type> GetTypes();

        void AddType(Budget.Api.Models.Type type);
        Budget.Api.Models.Type GetTypeByID(Guid Id);

        void UpdateType(Budget.Api.Models.Type type);

        void DeleteType(Guid Id);
    }
}
