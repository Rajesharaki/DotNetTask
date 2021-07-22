using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationProject.Models
{
    public class NewCustomer
    {
        public string CustomerType { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string CustomerName { get; set; }
        public string EmailAddress { get; set; }
    }
}
