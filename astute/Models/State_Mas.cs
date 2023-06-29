using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class State_Mas
    {
        [Key]
        public int State_Id { get; set; }
        public string State { get; set; }
        public string? Std_Code { get; set; }
        public int Country_id { get; set; }
        public Int16? Order_No { get; set; }
        public Int16? Sort_No { get; set; }
        public bool Status { get; set; }
    }
}
