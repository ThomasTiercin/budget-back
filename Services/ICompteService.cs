using Budget.Models;
using System.Collections.Generic;

namespace Budget.Services
{
    public interface ICompteService
    {
        IEnumerable<Compte> GetComptes();

        void AddCompte(Compte compte);
        Compte GetCompteByID(string id);

        void UpdateCompte(Compte compte);

        void DeleteCompte(string id);
    }
}
