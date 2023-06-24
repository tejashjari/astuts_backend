using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace astute.Models
{
    public partial class AstuteDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public AstuteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("AstuteConnection"));
        }

        public DbSet<Category_Master> Category_Master { get; set; }
        public DbSet<Category_Value> Category_Value { get; set; }
        public DbSet<Supplier_Value> Supplier_Value { get; set; }
        public DbSet<Employee_Master> Employee_Master { get; set; }
        public DbSet<Employee_Document> Employee_Document { get; set; }
        public DbSet<Employee_Salary> Employee_Salary { get; set; }
        public DbSet<Country_Mas> Country_Mas { get; set; }
        public DbSet<State_Mas> State_Mas { get; set; }
        public DbSet<City_Mas> City_Mas { get; set; }
        public DbSet<Terms_Mas> Terms_Mas { get; set; }
        public DbSet<Company_Master> Company_Master { get; set; }
        public DbSet<Company_Document> Company_Document { get; set; }
        public DbSet<Company_Media> Company_Media { get; set; }
        public DbSet<Company_Bank> Company_Bank { get; set; }
        public DbSet<Year_Mas> Year_Mas { get; set; }
        public DbSet<Error_Log> Error_Log { get; set; }
        public DbSet<Quote_Mas_Model> Quote_Mas_Model { get; set; }
        public DbSet<Process_Mas> Process_Mas { get; set; }
        public DbSet<Currency_Mas> Currency_Mas { get; set; }
        public DbSet<Pointer_Mas> Pointer_Mas { get; set; }
        public DbSet<Pointer_Det> Pointer_Det { get; set; }
    }
}
