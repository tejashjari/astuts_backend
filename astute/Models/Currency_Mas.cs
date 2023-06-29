using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Currency_Mas
    {
        [Key]
        public string Currency { get; set; }
        public string Currency_Name { get; set; }
        public string Symbol { get; set; }
        public Int16 Order_No { get; set; }
        public Int16 Sort_No { get; set; }
        public bool status { get; set; }
    }
}
