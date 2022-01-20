using Budget.Api.Models;
using System.Collections.Generic;
using System;
namespace Budget.Api.Services
{
    public interface IOrganismeService
    {
        IEnumerable<Organisme> GetOrganismes();

        void AddOrganisme(Organisme organismeItem);
        Organisme GetOrganismeByID(Guid Id);

        void UpdateOrganisme(Organisme organismeItem);

        void DeleteOrganisme(Guid Id);
    }
}
