using Budget.Api.Models;
using System.Collections.Generic;
using System;
namespace Budget.Api.Services
{
    public interface IEcheanceService
    {
        IEnumerable<Echeance> GetEcheances();

        void AddEcheance(Echeance echeance);
        Echeance GetEcheanceByID(Guid Id);

        void UpdateEcheance(Echeance echeance);

        void DeleteEcheance(Guid Id);
    }
}
