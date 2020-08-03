using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Interfaces
{
    public interface IAccountMovementService
    {
        void InsertAccountMovement(int accountId, AccountOperation operation, decimal amount);
        IList<AccountMovementResponseDto> GetAccountMovement(int id);
    }
}
