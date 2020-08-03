using DesafioWarren.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Interfaces
{
    public interface IDailyIncomeRepository
    {
        public void Remove(int id);
        public void Save(DailyIncome obj);
        public List<DailyIncome> GetDailyIncomeByClientId(int id);
        public IList<DailyIncome> GetAll();
        public void Commit();
    }
}
