using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Company_Document
    {
        [Key]
        public int Company_Id { get; set; }
        public int Cat_Val_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Expiry_Date { get; set; }
        public string Upload_Path { get; set; }
    }
}
