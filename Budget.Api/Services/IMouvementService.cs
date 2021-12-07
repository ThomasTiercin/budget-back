using Budget.Api.Models;
using System.Collections.Generic;

namespace Budget.Api.Services
{
    public interface IMouvementService
    {
        IEnumerable<Mouvement> GetMouvements();

        void AddMouvement(Mouvement mouvement);
        Mouvement GetMouvementByID(string Id);

        void UpdateMouvement(Mouvement mouvement);

        void DeleteMouvement(string Id);
    }
}
