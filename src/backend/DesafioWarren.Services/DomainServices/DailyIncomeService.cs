using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioWarren.Services.DomainServices
{
    public class DailyIncomeService : IDailyIncomeService
    {
        private readonly IDailyIncomeRepository _dailyIncomeRepository;
        private readonly IAccountRepository _accountRepository;
        private const decimal TaxaSelicAA = 2.15M;

        public DailyIncomeService(IDailyIncomeRepository dailyIncomeRepository, IAccountRepository accountRepository)
        {
            this._dailyIncomeRepository = dailyIncomeRepository;
            this._accountRepository = accountRepository;
        }


        public List<DailyIncomeDto> GetDailyIncome(int clientId)
        {
            this.CalculateDailyIncome(clientId);
            var dailyIncomeList = this._dailyIncomeRepository.GetDailyIncomeByClientId(clientId).OrderByDescending(a => a.Date);
            var dailyIncomeDtoList = dailyIncomeList.Select(c => new DailyIncomeDto()
            {
                Id = c.Id,
                BaseAmount = c.BaseAmount.ToString("F"),
                DailyYeld = c.DailyYeld.ToString("F"),
                Date = c.Date.ToString("dd/MM/yyyy")
            }).ToList();

            return dailyIncomeDtoList;
        }

        public void CalculateDailyIncome(int clientId)
        {
            var account = this._accountRepository.GetByClientId(clientId);
            var dailyIncomeList = this._dailyIncomeRepository.GetDailyIncomeByClientId(clientId);
            decimal rendimento;
            if (account.AccountBalance > 0)
            {
                if (dailyIncomeList.Count > 0)
                {
                    if (dailyIncomeList.Max(a => a.Date).Date != DateTime.Today)
                    {

                        var lastDailyIncome = dailyIncomeList.Max(a => a.Date).Date.AddDays(1);

                        while (lastDailyIncome <= DateTime.Today)
                        {
                            rendimento = this.CalculateYeld(account.AccountBalance);
                            this._dailyIncomeRepository.Save(new DailyIncome()
                            {
                                AccountId = account.Id,
                                BaseAmount = account.AccountBalance,
                                Date = lastDailyIncome.Date,
                                DailyYeld = rendimento,
                            });

                            account.AccountBalance += rendimento;
                            this._accountRepository.Save(account);

                            this._dailyIncomeRepository.Commit();
                            this._accountRepository.Commit();

                            lastDailyIncome = lastDailyIncome.Date.AddDays(1);
                        }

                    }
                }
                else
                {
                    rendimento = this.CalculateYeld(account.AccountBalance);
                    this._dailyIncomeRepository.Save(new DailyIncome()
                    {
                        AccountId = account.Id,
                        BaseAmount = account.AccountBalance,
                        Date = DateTime.Today,
                        DailyYeld = rendimento,
                    });

                    account.AccountBalance += rendimento;
                    this._accountRepository.Save(account);
                    this._accountRepository.Commit();
                }
            }
        }

        public void CalculateDailyIncomeFromOperation(Account account)
        {
            var todayIncome = this._dailyIncomeRepository.GetDailyIncomeByClientId(account.ClientId).Where(d => d.Date.Date == DateTime.Today).FirstOrDefault();
            if (todayIncome == null)
            {
                this.CalculateDailyIncome(account.ClientId);
            }
            else
            {
                account.AccountBalance = account.AccountBalance - todayIncome.DailyYeld;
                var rendimento = this.CalculateYeld(account.AccountBalance);

                account.AccountBalance += rendimento;


                this._accountRepository.Save(account);
                this._accountRepository.Commit();

                todayIncome.BaseAmount = account.AccountBalance;
                todayIncome.DailyYeld = rendimento;

                this._dailyIncomeRepository.Save(todayIncome);
                this._dailyIncomeRepository.Commit();
            }
        }

        private decimal CalculateYeld(decimal accountBallance)
        {
            int days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            var taxaSelicDiaria = ((TaxaSelicAA / 12) / days) / 100;
            var yeld = accountBallance * taxaSelicDiaria;
            return yeld;
        }
    }
}
