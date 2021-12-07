using Budget.Api.Models;
using System.Collections.Generic;

namespace Budget.Api.Services
{
    public interface ICategorieService
    {
        IEnumerable<Categorie> GetCategories();

        void AddCategorie(Categorie categorie);
        Categorie GetCategorieByID(string Id);

        void UpdateCategorie(Categorie categorie);

        void DeleteCategorie(string Id);
    }
}
