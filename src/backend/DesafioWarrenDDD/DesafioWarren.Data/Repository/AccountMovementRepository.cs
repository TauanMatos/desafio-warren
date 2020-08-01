using DesafioWarren.Data.Context;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioWarren.Data.Repository
{
    public class AccountMovementRepository : Repository<AccountMovement>, IAccountMovementRepository
    {
        public AccountMovementRepository(WarrenDbContext warrenDbContext) : base(warrenDbContext)
        {
        }


        public void Remove(int id) => base.Delete(id);


        public void Save(AccountMovement obj)
        {
            if (obj.Id == 0)
                base.Insert(obj);
            else
                base.Update(obj);
        }

        public AccountMovement GetById(int id) => base.Select(id);

        public IList<AccountMovement> GetAll() => base.Select();

        public IList<AccountMovement> GetAccountMovementByAccountId(int accountId)
        {
            var accountMovementsList = this._warrenDbContext.AccountMovements.Where(a => a.AccountId == accountId).ToList();
            return accountMovementsList;
        }
    }
}
