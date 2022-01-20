using Budget.Api.Models;
using System.Collections.Generic;
using System;
namespace Budget.Api.Services
{
    public interface ICompteService
    {
        IEnumerable<Compte> GetComptes();

        void AddCompte(Compte compte);
        Compte GetCompteByID(Guid id);
        IEnumerable<Compte> GetCompteByUserId(Guid id);
        void UpdateCompte(Compte compte);

        void DeleteCompte(Guid id);
    }
}
