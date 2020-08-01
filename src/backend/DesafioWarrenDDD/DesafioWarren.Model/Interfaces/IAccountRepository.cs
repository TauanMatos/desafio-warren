using DesafioWarren.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Interfaces
{
    public interface IAccountRepository
    {
        public void Remove(int id);
        public void Save(Account obj);
        public Account GetById(int id);
        public IList<Account> GetAll();
    }
}
