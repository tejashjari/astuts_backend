using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class City_Mas
    {
        [Key]
        public int City_Id { get; set; }
        public string? City { get; set; }
        public int? State_Id { get; set; }
        public Int16? Order_No { get; set; }
        public Int16? Sort_No { get;set; }
        public bool Status { get; set; }
    }
}
