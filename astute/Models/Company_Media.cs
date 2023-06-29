using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Company_Media
    {
        [Key]
        public int Company_Id { get; set; }
        public int Cat_Val_Id { get; set; }
        public string Media_Detail { get; set; }
    }
}
