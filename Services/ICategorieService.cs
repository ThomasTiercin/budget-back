using Budget.Models;
using System.Collections.Generic;

namespace Budget.Services
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
