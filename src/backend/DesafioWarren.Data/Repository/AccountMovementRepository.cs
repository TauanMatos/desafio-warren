using DesafioWarren.Data.Context;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public void Commit() => base.SaveChanges();
        public IList<AccountMovement> GetAccountMovementByClientId(int accountId)
        {
            var accountMovementsList = this._warrenDbContext.AccountMovements.Include(a => a.Account).Where(a => a.Account.ClientId == accountId).OrderBy(a => a.OperationDate).ToList();
            return accountMovementsList;
        }
    }
}
