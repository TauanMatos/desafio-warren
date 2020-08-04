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
    public class DailyIncomeRepository : Repository<DailyIncome>, IDailyIncomeRepository
    {
        public DailyIncomeRepository(WarrenDbContext warrenDbContext) : base(warrenDbContext)
        {
        }


        public void Remove(int id) => base.Delete(id);

        public void Save(DailyIncome obj)
        {
            if (obj.Id == 0)
                base.Insert(obj);
            else
                base.Update(obj);
        }

        public List<DailyIncome> GetDailyIncomeByClientId(int id)
        {
            var accountMovementsList = this._warrenDbContext.DailyIncomes.Include(a => a.Account).Where(a => a.Account.ClientId == id).OrderBy(a => a.Date).ToList();
            return accountMovementsList;
        }

        public void Commit() => base.SaveChanges();
        public IList<DailyIncome> GetAll() => base.Select();
    }
}
