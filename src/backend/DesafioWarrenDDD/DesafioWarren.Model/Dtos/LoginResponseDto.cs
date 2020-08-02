using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Dtos
{
    public class LoginResponseDto
    {
        public bool Authenticated { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public int ClientId { get; set; }
    }
}
