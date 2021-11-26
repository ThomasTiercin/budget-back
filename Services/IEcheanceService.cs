using Budget.Models;
using System.Collections.Generic;

namespace Budget.Services
{
    public interface IEcheanceService
    {
        IEnumerable<Echeance> GetEcheances();

        void AddEcheance(Echeance echeance);
        Echeance GetEcheanceByID(string Id);

        void UpdateEcheance(Echeance echeance);

        void DeleteEcheance(string Id);
    }
}
