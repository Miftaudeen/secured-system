using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecuredSystem.Models
{
    public class AuditTable
    {
        [Key]
        public int AuditId { get; set; }
        [Display(Name="User")]
        public string Admin { get; set; }
        public string Customer { get; set; }
        public string Field { get; set; }
        [Display(Name = "Initial value")]
        public string InitialValue { get; set; }
        [Display(Name = "Final value")]
        public string FinalValue { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set;}
    }
}