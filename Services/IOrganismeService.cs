using Budget.Models;
using System.Collections.Generic;

namespace Budget.Services
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
