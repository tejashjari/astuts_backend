using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Employee_Document
    {
        [Key]
        public int Employee_Id { get; set; }
        public int Document_Type { get; set; }
        public DateTime Document_Expiry_Date { get; set; }
        public string Document_Url { get; set; }
    }
}
