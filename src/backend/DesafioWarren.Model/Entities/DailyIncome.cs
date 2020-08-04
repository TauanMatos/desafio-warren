using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Entities
{
    public class DailyIncome : EntityBase
    {
        public decimal BaseAmount { get; set; }
        public decimal DailyYeld { get; set; }
        public DateTime Date { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
