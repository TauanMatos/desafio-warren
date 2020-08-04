using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Dtos
{
    public class AccountResponseDto
    {
        public bool Success { get; set; }
        public string AccountBalance { get; set; }
        public string Message { get; set; }
    }
}
