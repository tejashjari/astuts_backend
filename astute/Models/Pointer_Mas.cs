using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Pointer_Mas
    {
        [Key]
        public int Pointer_Id { get; set; }
        public string Pointer_Name { get; set; }
        public decimal From_Cts { get; set; }
        public decimal To_Cts { get; set; }
        public int Pointer_Type { get; set; }
        public Int16 Order_No { get; set; }
        public Int16 Sort_No { get; set; }
        public bool Status { get; set; }
    }
}
