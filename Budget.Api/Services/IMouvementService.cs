using Budget.Api.Models;
using System.Collections.Generic;
using System;
namespace Budget.Api.Services
{
    public interface IMouvementService
    {
        IEnumerable<Mouvement> GetMouvements();

        void AddMouvement(Mouvement mouvement);
        Mouvement GetMouvementByID(Guid Id);
        IEnumerable<Mouvement> GetMouvementByUserId(Guid id);
        void UpdateMouvement(Mouvement mouvement);

        void DeleteMouvement(Guid Id);
    }
}
