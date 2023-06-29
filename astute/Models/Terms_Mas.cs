using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Terms_Mas
    {
        [Key]
        public string Terms { get; set; }
        public Int16 Term_Days { get; set; }
        public Int16 Order_No { get; set; }
        public Int16 Sort_No { get; set; }
        public bool Status { get; set; }
    }
}
