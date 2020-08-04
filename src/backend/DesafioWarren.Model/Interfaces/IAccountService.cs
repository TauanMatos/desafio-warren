using DesafioWarren.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesafioWarren.Model.Interfaces
{
    public interface IAccountService
    {
        AccountResponseDto Deposit(AccountRequestDto accountDto);
        AccountResponseDto Withdraw(AccountRequestDto accountDto);
        AccountResponseDto Payment(AccountRequestDto accountDto);
        AccountDto GetAccount(int clientID);
    }
}
