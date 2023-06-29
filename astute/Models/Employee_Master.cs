using System;
using System.ComponentModel.DataAnnotations;

namespace astute.Models
{
    public partial class Employee_Master
    {
        [Key]
        public int Employee_Id { get; set; }
        public string Initial { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name {get; set; }
        public string Last_Name { get; set;}
        public string Chinese_Name { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_3 { get; set; }
        public int City_Id { get; set; }
        public DateTime Join_date { get; set; }
        public string Employee_Type { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Gender { get; set; }
        public string Mobile_No { get; set; }
        public string Personal_Email { get; set; }
        public string Company_Email { get; set; }
        public DateTime Leave_Date { get; set; }
        public string PSN_ID { get; set; }
        public string Blood_Group { get; set; }
        public DateTime Contract_Start_Date { get; set; }
        public DateTime Contract_End_Date { get;set; }
        public Int16 Approve_Holidays { get; set; }
        public Int16 Order_No { get; set; }
        public Int16 Sort_No { get;set; }
        public string Icon_Upload { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
    }
}
