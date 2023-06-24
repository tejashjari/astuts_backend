using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Employee_Salary
    {
        [Key]
        public int Employee_Id { get; set; }
        public int Salary { get; set; }
        public DateTime Start_Date { get; set; }
    }
}
