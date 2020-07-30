using DesafioWarren.Data.Context;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Data.Repository
{
    public class AccountMovementRepository : Repository<AccountMovement>, IAccountMovementRepository
    {
        public AccountMovementRepository(WarrenDbContext warrenDbContext) : base(warrenDbContext)
        {
        }
    }
}
