using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Entities
{
    public class Account : EntityBase
    {
        public Account()
        {
            this.AccountBalance = 0;
            this.AccountMovements = new List<AccountMovement>();
        }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal AccountBalance { get; set; }
        public virtual IList<AccountMovement> AccountMovements { get; set; }
    }
}
