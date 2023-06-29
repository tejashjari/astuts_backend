using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Supplier_Value
    {
        [Key]
        public int Sup_Id { get; set; }
        public string Supp_Cat_name { get; set; }
        public int Cat_Val_id { get; set; }
        public bool Status { get; set; }
    }
}
