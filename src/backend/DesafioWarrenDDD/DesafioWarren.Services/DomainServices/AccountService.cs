using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities.Enum;
using DesafioWarren.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesafioWarren.Services.DomainServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMovementService _accountMovementService;
        public AccountService(IAccountRepository accountRepository, IAccountMovementService accountMovement)
        {
            this._accountRepository = accountRepository;
            this._accountMovementService = accountMovement;
        }

        public AccountResponseDto Deposit(AccountRequestDto accountDto)
        {
            var accountResponse = new AccountResponseDto();
            var account = this._accountRepository.GetById(accountDto.Id);
            if (account != null)
            {
                account.AccountBalance += accountDto.Value;

                this._accountRepository.Save(account);

                this._accountMovementService.InsertAccountMovement(account.Id, AccountOperation.Deposit, accountDto.Value);

                accountResponse.Success = true;
                accountResponse.AccountBalance = account.AccountBalance.ToString("0.##");
            }
            else
            {
                accountResponse.Success = false;
                accountResponse.Message = "Account not found";
            }

            return accountResponse;
        }

        public AccountResponseDto Withdraw(AccountRequestDto accountDto)
        {
            var accountResponse = new AccountResponseDto();
            var account = this._accountRepository.GetById(accountDto.Id);

            if (account != null)
            {
                if (account.AccountBalance >= accountDto.Value)
                {
                    account.AccountBalance -= accountDto.Value;
                    this._accountRepository.Save(account);
                    this._accountMovementService.InsertAccountMovement(account.Id, AccountOperation.Withdraw, accountDto.Value);
                    accountResponse.Success = true;
                    accountResponse.AccountBalance = account.AccountBalance.ToString("0.##");
                }
                else
                {
                    accountResponse.Success = false;
                    accountResponse.Message = "Account does not have enough balance";
                }
            }
            else
            {
                accountResponse.Success = false;
                accountResponse.Message = "Account not found";
            }
            return accountResponse;
        }

        public AccountResponseDto Payment(AccountRequestDto accountDto)
        {
            var accountResponse = new AccountResponseDto();
            var account = this._accountRepository.GetById(accountDto.Id);

            if (account != null)
            {
                if (account.AccountBalance >= accountDto.Value)
                {
                    account.AccountBalance -= accountDto.Value;
                    this._accountRepository.Save(account);
                    this._accountMovementService.InsertAccountMovement(account.Id, AccountOperation.Payment, accountDto.Value);
                    accountResponse.Success = true;
                    accountResponse.AccountBalance = account.AccountBalance.ToString("0.##");
                }
                else
                {
                    accountResponse.Success = false;
                    accountResponse.Message = "Account does not have enough balance to do this payment";
                }
            }
            else
            {
                accountResponse.Success = false;
                accountResponse.Message = "Account not found";
            }

            return accountResponse;
        }
    }
}
