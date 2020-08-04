using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DesafioWarren.Model.Dtos
{
    public class AccountRequestDto
    {
        [Required]
        public int ClientId { get; set; }
        
        [Required]
        public decimal Value { get; set; }
    }
}
