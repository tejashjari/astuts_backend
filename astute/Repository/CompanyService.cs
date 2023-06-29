using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class CompanyService : ICompanyService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public CompanyService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        #region Company Master
        public async Task<int> InsertCompany(Company_Master company_Master)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Master.Company_Id));
            parameter.Add(new SqlParameter("@Company_Name", company_Master.Company_Name));
            parameter.Add(new SqlParameter("@Address_1", company_Master.Address_1));
            parameter.Add(new SqlParameter("@Address_2", company_Master.Address_2));
            parameter.Add(new SqlParameter("@Address_3", company_Master.Address_3));
            parameter.Add(new SqlParameter("@City_Id", company_Master.City_Id));
            parameter.Add(new SqlParameter("@Phone_No", company_Master.Phone_No));
            parameter.Add(new SqlParameter("@Fax_No", company_Master.Fax_No));
            parameter.Add(new SqlParameter("@Email", company_Master.Email));
            parameter.Add(new SqlParameter("@Website", company_Master.Website));
            parameter.Add(new SqlParameter("@Order_No", company_Master.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", company_Master.Sort_No));
            parameter.Add(new SqlParameter("@Status", company_Master.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Master_Insert_Update @Company_Id, @Company_Name, @Address_1, @Address_2, @Address_3, @City_Id, @Phone_No, @Fax_No, @Email, @Website, @Order_No, @Sort_No, @Status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateCompany(Company_Master company_Master)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Master.Company_Id));
            parameter.Add(new SqlParameter("@Company_Name", company_Master.Company_Name));
            parameter.Add(new SqlParameter("@Address_1", company_Master.Address_1));
            parameter.Add(new SqlParameter("@Address_2", company_Master.Address_2));
            parameter.Add(new SqlParameter("@Address_3", company_Master.Address_3));
            parameter.Add(new SqlParameter("@City_Id", company_Master.City_Id));
            parameter.Add(new SqlParameter("@Phone_No", company_Master.Phone_No));
            parameter.Add(new SqlParameter("@Fax_No", company_Master.Fax_No));
            parameter.Add(new SqlParameter("@Email", company_Master.Email));
            parameter.Add(new SqlParameter("@Website", company_Master.Website));
            parameter.Add(new SqlParameter("@Order_No", company_Master.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", company_Master.Sort_No));
            parameter.Add(new SqlParameter("@Status", company_Master.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Master_Insert_Update @Company_Id, @Company_Name, @Address_1, @Address_2, @Address_3, @City_Id, @Phone_No, @Fax_No, @Email, @Website, @Order_No, @Sort_No, @Status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteCompany(int companyId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Company_Master {companyId}"));
        }

        public async Task<IList<Company_Master>> GetCompany(int companyId)
        {
            var CompanyId = companyId > 0 ? new SqlParameter("@companyId", companyId) : new SqlParameter("@companyId", DBNull.Value);

            var result = await Task.Run(() => _dbContext.Company_Master
                            .FromSqlRaw(@"exec Company_Master_Select @companyId", CompanyId).ToListAsync());

            return result;
        }
        #endregion

        #region Company Document
        public async Task<int> InsertCompanyDocument(Company_Document company_Document)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Document.Company_Id));
            parameter.Add(new SqlParameter("@Cat_Val_Id", company_Document.Cat_Val_Id));
            parameter.Add(new SqlParameter("@Start_Date", company_Document.Start_Date));
            parameter.Add(new SqlParameter("@Expiry_Date", company_Document.Expiry_Date));
            parameter.Add(new SqlParameter("@Upload_Path", company_Document.Upload_Path));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Document_Insert_Update @Company_Id, @Cat_Val_Id, @Start_Date, @Expiry_Date, @Upload_Path, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateCompanyDocument(Company_Document company_Document)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Document.Company_Id));
            parameter.Add(new SqlParameter("@Cat_Val_Id", company_Document.Cat_Val_Id));
            parameter.Add(new SqlParameter("@Start_Date", company_Document.Start_Date));
            parameter.Add(new SqlParameter("@Expiry_Date", company_Document.Expiry_Date));
            parameter.Add(new SqlParameter("@Upload_Path", company_Document.Upload_Path));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Document_Insert_Update @Company_Id, @Cat_Val_Id, @Start_Date, @Expiry_Date, @Upload_Path, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteCompanyDocument(int companyId, int catvalId, DateTime startDate)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", companyId));
            parameter.Add(new SqlParameter("@Cat_Val_Id", catvalId));
            parameter.Add(new SqlParameter("@Start_Date", startDate));

            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Company_Master {companyId}, {catvalId}, {startDate}"));
        }

        public async Task<Company_Document> GetCompanyDocument(int companyId, int catvalId, DateTime startDate)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", companyId));
            parameter.Add(new SqlParameter("@Cat_Val_Id", catvalId));
            parameter.Add(new SqlParameter("@Start_Date", startDate));

            var result = await Task.Run(() => _dbContext.Company_Document
                            .FromSqlRaw(@"exec Company_Document_Select @Company_Id, @Cat_Val_Id, @Start_Date", parameter.ToArray()).ToListAsync());

            return result.FirstOrDefault();
        }
        #endregion

        #region Company Media
        public async Task<int> InsertCompanyMedia(Company_Media company_Media)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Media.Company_Id));
            parameter.Add(new SqlParameter("@Cat_Val_Id", company_Media.Cat_Val_Id));
            parameter.Add(new SqlParameter("@Media_Detail", company_Media.Media_Detail));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Document_Insert_Update @Company_Id, @Cat_Val_Id, @Media_Detail, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateCompanyMedia(Company_Media company_Media)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Media.Company_Id));
            parameter.Add(new SqlParameter("@Cat_Val_Id", company_Media.Cat_Val_Id));
            parameter.Add(new SqlParameter("@Media_Detail", company_Media.Media_Detail));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Document_Insert_Update @Company_Id, @Cat_Val_Id, @Media_Detail, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteCompanyMedia(int companyId, int catvalId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Company_Media {companyId}, {catvalId}"));
        }

        public async Task<Company_Media> GetCompanyMedia(int companyId, int catvalId)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", companyId));
            parameter.Add(new SqlParameter("@Cat_Val_Id", catvalId));

            var result = await Task.Run(() => _dbContext.Company_Media
                            .FromSqlRaw(@"exec Company_Media_Select @Company_Id, @Cat_Val_Id", parameter.ToArray()).ToListAsync());

            return result.FirstOrDefault();
        }
        #endregion

        #region Company Bank
        public async Task<int> InsertCompanyBank(Company_Bank company_Bank)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Bank.Company_Id));
            parameter.Add(new SqlParameter("@Bank_Id", company_Bank.Bank_Id));
            parameter.Add(new SqlParameter("@Currency", company_Bank.Currency));
            parameter.Add(new SqlParameter("@Account_Type", company_Bank.Account_Type));
            parameter.Add(new SqlParameter("@Account_No", company_Bank.Account_No));
            parameter.Add(new SqlParameter("@Process_Id", company_Bank.Process_Id));
            parameter.Add(new SqlParameter("@Status", company_Bank.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Bank_Insert_Update @Company_Id, @Bank_Id, @Currency, @Account_Type, @Account_No, @Process_Id, @Status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateCompanyBank(Company_Bank company_Bank)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", company_Bank.Company_Id));
            parameter.Add(new SqlParameter("@Bank_Id", company_Bank.Bank_Id));
            parameter.Add(new SqlParameter("@Currency", company_Bank.Currency));
            parameter.Add(new SqlParameter("@Account_Type", company_Bank.Account_Type));
            parameter.Add(new SqlParameter("@Account_No", company_Bank.Account_No));
            parameter.Add(new SqlParameter("@Process_Id", company_Bank.Process_Id));
            parameter.Add(new SqlParameter("@Status", company_Bank.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Company_Bank_Insert_Update @Company_Id, @Bank_Id, @Currency, @Account_Type, @Account_No, @Process_Id, @Status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteCompanyBank(int companyId, int bankId)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", companyId));
            parameter.Add(new SqlParameter("@Bank_Id", bankId));

            return await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync($"Delete_Company_Bank @Company_Id, @Bank_Id", parameter.ToArray()));
        }
        public async Task<Company_Bank> GetCompanyBank(int companyId, int bankId)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Company_Id", companyId));
            parameter.Add(new SqlParameter("@BankId", bankId));

            var result = await Task.Run(() => _dbContext.Company_Bank
                           .FromSqlRaw(@"exec Company_Bank_Select @Company_Id, @BankId", parameter.ToArray())
                           .AsEnumerable()
                           .FirstOrDefault());

            return result;
        }
        #endregion
        #endregion
    }
}
