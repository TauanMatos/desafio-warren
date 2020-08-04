using DesafioWarren.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Interfaces
{
    public interface IAccountMovementRepository
    {
        void Remove(int id);
        void Save(AccountMovement obj);
        AccountMovement GetById(int id);
        IList<AccountMovement> GetAll();
        IList<AccountMovement> GetAccountMovementByClientId(int accountId);
        void Commit();
    }
}
