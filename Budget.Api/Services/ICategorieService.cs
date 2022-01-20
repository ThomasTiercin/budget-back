using Budget.Api.Models;
using System.Collections.Generic;
using System; 
namespace Budget.Api.Services
{
    public interface ICategorieService
    {
        IEnumerable<Categorie> GetCategories();

        void AddCategorie(Categorie categorie);
        Categorie GetCategorieByID(Guid Id);

        void UpdateCategorie(Categorie categorie);

        void DeleteCategorie(Guid Id);
    }
}
