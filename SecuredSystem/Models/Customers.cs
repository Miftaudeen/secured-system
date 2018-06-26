using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SecuredSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Surname { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        [Display(Name = "Next of Kin")]
        public string NextOfKin { get; set; }
        [Display(Name = "Account Number")]
        [DataType(DataType.CreditCard)]
        public string AccountNumber { get; set; }
        public string PIN { get; set; }
    }
}