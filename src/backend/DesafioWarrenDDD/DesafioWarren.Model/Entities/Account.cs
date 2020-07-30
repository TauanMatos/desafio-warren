using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Entities
{
    public class Account : EntityBase
    {
        public decimal AccountBallance { get; set; }
        public virtual IList<AccountMovement> AccountMovements { get; set; }
    }
}
