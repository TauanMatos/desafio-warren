using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Interfaces;
using DesafioWarren.Services.DomainServices;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Test
{
    public class AccountTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<IAccountMovementRepository> _mockAccountMovementRepository;
        private Mock<IDailyIncomeRepository> _mockDailyIncomeRepository;

        [SetUp]
        public void Seed()
        {
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.GetByClientId(1)).Returns(new Model.Entities.Account()
            {
                AccountBalance = 0,
                AccountMovements = new List<AccountMovement>(),
                DailyIncome = new List<DailyIncome>(),
                Id = 1,
                ClientId = 1
            });

            mockAccountRepository.Setup(x => x.GetByClientId(2)).Returns(new Model.Entities.Account()
            {
                AccountBalance = 10000,
                AccountMovements = new List<AccountMovement>(),
                DailyIncome = new List<DailyIncome>(),
                Id = 2,
                ClientId = 2
            });


            this._mockAccountRepository = mockAccountRepository;

            var mockDailyIncomeRepository = new Mock<IDailyIncomeRepository>();
            mockDailyIncomeRepository.Setup(x => x.GetDailyIncomeByClientId(1)).Returns(new List<DailyIncome>());
            mockDailyIncomeRepository.Setup(x => x.GetDailyIncomeByClientId(2)).Returns(new List<DailyIncome>());

            this._mockDailyIncomeRepository = mockDailyIncomeRepository;

            var mockAccountMovementsRepository = new Mock<IAccountMovementRepository>();
            mockAccountMovementsRepository.Setup(x => x.GetAccountMovementByClientId(1)).Returns(new List<AccountMovement>()
            {
                new AccountMovement() { AccountId = 1, AccountOperation = Model.Entities.Enum.AccountOperation.Deposit, Amount = 10000, Id = 1, OperationDate = DateTime.Today }
            });

            this._mockAccountMovementRepository = mockAccountMovementsRepository;
        }

        [Test]
        public void DepositWithSucess()
        {
            DailyIncomeService dailyIncomeService = new DailyIncomeService(this._mockDailyIncomeRepository.Object, this._mockAccountRepository.Object);
            AccountMovementService accountMovementService = new AccountMovementService(this._mockAccountMovementRepository.Object);
            AccountService accountService = new AccountService(this._mockAccountRepository.Object, accountMovementService, dailyIncomeService);

            var result = accountService.Deposit(new Model.Dtos.AccountRequestDto() { ClientId = 1, Value = 10000 });

            Assert.AreEqual("10000,58", result.AccountBalance);
            Assert.AreEqual(true, result.Success);

        }


        [Test]
        public void WithdrawSucess()
        {
            DailyIncomeService dailyIncomeService = new DailyIncomeService(this._mockDailyIncomeRepository.Object, this._mockAccountRepository.Object);
            AccountMovementService accountMovementService = new AccountMovementService(this._mockAccountMovementRepository.Object);
            AccountService accountService = new AccountService(this._mockAccountRepository.Object, accountMovementService, dailyIncomeService);

            var result = accountService.Withdraw(new Model.Dtos.AccountRequestDto() { ClientId = 2, Value = 5000 });

            Assert.AreEqual("5000,29", result.AccountBalance);
            Assert.AreEqual(true, result.Success);

        }

        [Test]
        public void WithdrawNotEnoughBalance()
        {
            DailyIncomeService dailyIncomeService = new DailyIncomeService(this._mockDailyIncomeRepository.Object, this._mockAccountRepository.Object);
            AccountMovementService accountMovementService = new AccountMovementService(this._mockAccountMovementRepository.Object);
            AccountService accountService = new AccountService(this._mockAccountRepository.Object, accountMovementService, dailyIncomeService);

            var result = accountService.Withdraw(new Model.Dtos.AccountRequestDto() { ClientId = 2, Value = 11000 });

            Assert.AreEqual("Account does not have enough balance", result.Message);
            Assert.AreEqual(false, result.Success);

        }

        [Test]
        public void PaymentNotEnoughBalance()
        {
            DailyIncomeService dailyIncomeService = new DailyIncomeService(this._mockDailyIncomeRepository.Object, this._mockAccountRepository.Object);
            AccountMovementService accountMovementService = new AccountMovementService(this._mockAccountMovementRepository.Object);
            AccountService accountService = new AccountService(this._mockAccountRepository.Object, accountMovementService, dailyIncomeService);

            var result = accountService.Payment(new Model.Dtos.AccountRequestDto() { ClientId = 2, Value = 11000 });

            Assert.AreEqual("Account does not have enough balance to do this payment", result.Message);
            Assert.AreEqual(false, result.Success);

        }

        [Test]
        public void PaymentSucess()
        {
            DailyIncomeService dailyIncomeService = new DailyIncomeService(this._mockDailyIncomeRepository.Object, this._mockAccountRepository.Object);
            AccountMovementService accountMovementService = new AccountMovementService(this._mockAccountMovementRepository.Object);
            AccountService accountService = new AccountService(this._mockAccountRepository.Object, accountMovementService, dailyIncomeService);

            var result = accountService.Payment(new Model.Dtos.AccountRequestDto() { ClientId = 2, Value = 5000 });

            Assert.AreEqual("5000,29", result.AccountBalance);
            Assert.AreEqual(true, result.Success);

        }
    }
}
