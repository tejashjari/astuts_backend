using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Pointer_Det
    {
        [Key]
        public int Pointer_id { get; set; }
        public string Sub_Pointer_Name { get; set; }
        public decimal From_Cts { get; set; }
        public decimal To_Cts { get; set; }
        public Int16 Order_No { get; set; }
        public Int16 Sort_No { get; set; }
        public bool Status { get; set; }
    }
}
