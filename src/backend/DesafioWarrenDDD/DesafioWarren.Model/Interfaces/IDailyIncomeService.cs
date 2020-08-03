using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Interfaces
{
    public interface IDailyIncomeService
    {
        List<DailyIncomeDto> GetDailyIncome(int clientId);
        void CalculateDailyIncome(int clientId);
        void CalculateDailyIncomeFromOperation(Account account);
    }
}
