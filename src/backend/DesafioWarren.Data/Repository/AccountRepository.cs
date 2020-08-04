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
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(WarrenDbContext warrenDbContext) : base(warrenDbContext)
        {
        }


        public void Remove(int id) => base.Delete(id);

        public void Save(Account obj) => base.Update(obj);

        public Account GetByClientId(int id)
        {
            var account = _warrenDbContext.Accounts.Include(account => account.Client).Where(a => a.Client.Id == id).FirstOrDefault();
            return account;
        }
        public void Commit() => base.SaveChanges();
        public IList<Account> GetAll() => base.Select();
    }
}
