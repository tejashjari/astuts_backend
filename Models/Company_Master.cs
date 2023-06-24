using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Company_Master
    {
        [Key]
        public int Company_Id { get; set; }
        public string Company_Name { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_3 { get; set; }
        public int City_Id { get; set; }
        public string Phone_No { get; set; }
        public string Fax_No { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public Int16? Order_No { get; set; }
        public Int16? Sort_No { get; set; }
        public bool Status { get; set; }
    }
}
