using Budget.Api.Models;
using System.Collections.Generic;

namespace Budget.Api.Services
{
    public interface IOrganismeService
    {
        IEnumerable<Organisme> GetOrganismes();

        void AddOrganisme(Organisme organismeItem);
        Organisme GetOrganismeByID(string Id);

        void UpdateOrganisme(Organisme organismeItem);

        void DeleteOrganisme(string Id);
    }
}
