using DesafioWarren.Model.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
