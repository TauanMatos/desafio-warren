using DesafioWarren.Model.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Entities
{
    public class AccountMovement : EntityBase
    {
        public AccountOperation AccountOperation { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
