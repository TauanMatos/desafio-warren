using DesafioWarren.Model.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public virtual Account ClientAccount { get; set; }
    }
}
