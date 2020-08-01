using DesafioWarren.Model.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Dtos
{
    public class AccountMovementResponseDto
    {
        public int Id { get; set; }
        public string AccountOperation { get; set; }
        public DateTime OperationDate { get; set; }
        public string Amount { get; set; }
        public int AccountId { get; set; }
    }
}
