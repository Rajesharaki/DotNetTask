using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Models
{
    public class AuthenticateModel
    {
        public string Username { get; set; }

        public string Password { get; set; }    
    }
}
