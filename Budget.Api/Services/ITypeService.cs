using Budget.Api.Models;
using System.Collections.Generic;

namespace Budget.Api.Services
{
    public interface ITypeService
    {
        IEnumerable<Type> GetTypes();

        void AddType(Type type);
        Type GetTypeByID(string Id);

        void UpdateType(Type type);

        void DeleteType(string Id);
    }
}
