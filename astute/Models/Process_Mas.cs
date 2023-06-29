using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Process_Mas
    {
        [Key]
        public int Process_Id { get; set; }
        public string Process_Name { get; set;}
        public string Process_Type { get; set;}
        public Int16 Order_No { get; set;}
        public Int16 Sort_No { get; set;}
        public bool status { get; set;}
    }
}
