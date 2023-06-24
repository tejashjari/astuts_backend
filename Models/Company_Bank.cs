using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Company_Bank
    {
        [Key]
        public int Company_Id { get; set; }
        public int Bank_Id { get; set; }
        public string Currency { get; set; }
        public int Account_Type { get; set; }
        public string Account_No { get; set; }
        public int Process_Id { get; set; }
        public bool Status { get; set; }
    }
}
