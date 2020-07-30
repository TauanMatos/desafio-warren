using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Model.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Client UserClient { get; set; }
    }
}
