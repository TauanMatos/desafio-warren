using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Entities.Enum;
using DesafioWarren.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioWarren.Services.DomainServices
{
    public class AccountMovementService : IAccountMovementService
    {
        private readonly IAccountMovementRepository _accountMovementRepository;
        public AccountMovementService(IAccountMovementRepository accountMovementRepository)
        {
            this._accountMovementRepository = accountMovementRepository;
        }

        public void InsertAccountMovement(int accountId, AccountOperation operation, decimal amount)
        {
            var accountMovement = new AccountMovement()
            {
                AccountId = accountId,
                AccountOperation = operation,
                Amount = amount,
                OperationDate = DateTime.Now,
            };

            this._accountMovementRepository.Save(accountMovement);
        }

        public IList<AccountMovementResponseDto> GetAccountMovement(int id)
        {
            var accountMovementList = this._accountMovementRepository.GetAccountMovementByClientId(id);

            var accountMovementDtoList = accountMovementList.Select(c => new AccountMovementResponseDto()
            {
                Id = c.Id,
                AccountId = c.AccountId,
                AccountOperation = Enum.GetName(typeof(AccountOperation), c.AccountOperation),
                Amount = c.Amount.ToString("F"),
                OperationDate = c.OperationDate.ToString("dd/MM/yyyy hh:mm tt") 
            }).ToList();

            return accountMovementDtoList;
        }
    }
}
