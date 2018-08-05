using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecuredSystem.Models
{
    public enum Flag
    {
        Urgent, Normal, Resolved
    }
    public class Complaint
    {
        public int ComplaintID { get; set; }         
        public int CustomerID { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public Flag? Flag { get; set; }   
        public virtual Customer Customer { get; set; }
    }
}
