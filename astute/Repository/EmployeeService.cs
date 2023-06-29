using astute.CoreServices;
using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class EmployeeService : IEmployeeService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        private readonly ICommonService _commonService;
        #endregion

        #region Ctor
        public EmployeeService(AstuteDbContext dbContext, ICommonService commonService)
        {
            _dbContext = dbContext;
            _commonService = commonService;
        }
        #endregion

        #region Utilities
        //private string Encrypt(string clearText)
        //{
        //    string encryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}

        //private string Decrypt(string cipherText)
        //{
        //    string encryptionKey = "MAKV2SPBNI99212";
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
        #endregion

        #region Methods
        #region Employee Master
        public async Task<int> InsertEmployee(Employee_Master employee_Master)
        {
            var encryptPassword = CoreService.Encrypt(employee_Master.Password);
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@employeeId", employee_Master.Employee_Id));
            parameter.Add(new SqlParameter("@initial", employee_Master.Initial));
            parameter.Add(new SqlParameter("@firstName", employee_Master.First_Name));
            parameter.Add(new SqlParameter("@middleName", employee_Master.Middle_Name));
            parameter.Add(new SqlParameter("@lastName", employee_Master.Last_Name));
            parameter.Add(new SqlParameter("@chineseName", employee_Master.Chinese_Name));
            parameter.Add(new SqlParameter("@address1", employee_Master.Address_1));
            parameter.Add(new SqlParameter("@address2", employee_Master.Address_2));
            parameter.Add(new SqlParameter("@address3", employee_Master.Address_3));
            parameter.Add(new SqlParameter("@cityId", employee_Master.City_Id));
            parameter.Add(new SqlParameter("@joindate", employee_Master.Join_date));
            parameter.Add(new SqlParameter("@employeeType", employee_Master.Employee_Type));
            parameter.Add(new SqlParameter("@birthDate", employee_Master.Birth_Date));
            parameter.Add(new SqlParameter("@gender", employee_Master.Gender));
            parameter.Add(new SqlParameter("@mobileNo", employee_Master.Mobile_No));
            parameter.Add(new SqlParameter("@personalEmail", employee_Master.Personal_Email));
            parameter.Add(new SqlParameter("@companyEmail", employee_Master.Company_Email));
            parameter.Add(new SqlParameter("@leaveDate", employee_Master.Leave_Date));
            parameter.Add(new SqlParameter("@pSNID", employee_Master.PSN_ID));
            parameter.Add(new SqlParameter("@bloodGroup", employee_Master.Blood_Group));
            parameter.Add(new SqlParameter("@contractStartDate", employee_Master.Contract_Start_Date));
            parameter.Add(new SqlParameter("@contractEndDate", employee_Master.Contract_End_Date));
            parameter.Add(new SqlParameter("@approveHolidays", employee_Master.Approve_Holidays));
            parameter.Add(new SqlParameter("@orderNo", employee_Master.Order_No));
            parameter.Add(new SqlParameter("@sortNo", employee_Master.Sort_No));
            parameter.Add(new SqlParameter("@iconUpload", employee_Master.Icon_Upload));
            parameter.Add(new SqlParameter("@userName", employee_Master.User_Name));
            parameter.Add(new SqlParameter("@password", encryptPassword));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Employee_Master_Insert_Update @employeeId, @initial, @firstName, @middleName, @lastName,
            @chineseName, @address1, @address2, @address3, @cityId, @joindate, @employeeType, @birthDate, @gender, @mobileNo, @personalEmail, @companyEmail,
            @leaveDate, @pSNID, @bloodGroup, @contractStartDate, @contractEndDate, @approveHolidays, @orderNo, @sortNo, @iconUpload, @userName, @password, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateEmployee(Employee_Master employee_Master)
        {
            var encryptPassword = CoreService.Encrypt(employee_Master.Password);
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@employeeId", employee_Master.Employee_Id));
            parameter.Add(new SqlParameter("@initial", employee_Master.Initial));
            parameter.Add(new SqlParameter("@firstName", employee_Master.First_Name));
            parameter.Add(new SqlParameter("@middleName", employee_Master.Middle_Name));
            parameter.Add(new SqlParameter("@lastName", employee_Master.Last_Name));
            parameter.Add(new SqlParameter("@chineseName", employee_Master.Chinese_Name));
            parameter.Add(new SqlParameter("@address1", employee_Master.Address_1));
            parameter.Add(new SqlParameter("@address2", employee_Master.Address_2));
            parameter.Add(new SqlParameter("@address3", employee_Master.Address_3));
            parameter.Add(new SqlParameter("@cityId", employee_Master.City_Id));
            parameter.Add(new SqlParameter("@joindate", employee_Master.Join_date));
            parameter.Add(new SqlParameter("@employeeType", employee_Master.Employee_Type));
            parameter.Add(new SqlParameter("@birthDate", employee_Master.Birth_Date));
            parameter.Add(new SqlParameter("@gender", employee_Master.Gender));
            parameter.Add(new SqlParameter("@mobileNo", employee_Master.Mobile_No));
            parameter.Add(new SqlParameter("@personalEmail", employee_Master.Personal_Email));
            parameter.Add(new SqlParameter("@companyEmail", employee_Master.Company_Email));
            parameter.Add(new SqlParameter("@leaveDate", employee_Master.Leave_Date));
            parameter.Add(new SqlParameter("@pSNID", employee_Master.PSN_ID));
            parameter.Add(new SqlParameter("@bloodGroup", employee_Master.Blood_Group));
            parameter.Add(new SqlParameter("@contractStartDate", employee_Master.Contract_Start_Date));
            parameter.Add(new SqlParameter("@contractEndDate", employee_Master.Contract_End_Date));
            parameter.Add(new SqlParameter("@approveHolidays", employee_Master.Approve_Holidays));
            parameter.Add(new SqlParameter("@orderNo", employee_Master.Order_No));
            parameter.Add(new SqlParameter("@sortNo", employee_Master.Sort_No));
            parameter.Add(new SqlParameter("@iconUpload", employee_Master.Icon_Upload ?? employee_Master.Icon_Upload));
            parameter.Add(new SqlParameter("@userName", employee_Master.User_Name));
            parameter.Add(new SqlParameter("@password", encryptPassword));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Employee_Master_Insert_Update @employeeId, @initial, @firstName, @middleName, @lastName,
            @chineseName, @address1, @address2, @address3, @cityId, @joindate, @employeeType, @birthDate, @gender, @mobileNo, @personalEmail, @companyEmail,
            @leaveDate, @pSNID, @bloodGroup, @contractStartDate, @contractEndDate, @approveHolidays, @orderNo, @sortNo, @iconUpload, @userName, @password, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteEmployee(int employeeId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Employee_Master {employeeId}"));
        }
        public async Task<IList<Employee_Master>> GetEmployees(int employeeId, string userName, string personalEmail)
        {   
            var empId = employeeId > 0 ? new SqlParameter("@employeeId", employeeId) : new SqlParameter("@employeeId", DBNull.Value);
            var uName = !string.IsNullOrEmpty(userName) ? new SqlParameter("@userName", userName) : new SqlParameter("@userName", DBNull.Value);
            var email = !string.IsNullOrEmpty(personalEmail) ? new SqlParameter("@email", personalEmail) : new SqlParameter("@email", DBNull.Value);

            var employees = await Task.Run(() => _dbContext.Employee_Master
                            .FromSqlRaw(@"exec Employee_Master_Select @employeeId, @userName, @email", empId, uName, email).ToListAsync());

            return employees;
        }
        public async Task<Employee_Master> EmployeeLogin(UserModel userModel)
        {
            try
            {
                var password = CoreService.Encrypt(userModel.Password);
                var parameter = new List<SqlParameter>();
                
                parameter.Add(new SqlParameter("@userName", userModel.UserName));
                parameter.Add(new SqlParameter("@password", password));

                var result = await Task.Run(() => _dbContext.Employee_Master
                            .FromSqlRaw(@"exec EmployeeLogin @userName, @password", parameter.ToArray())
                            .AsEnumerable()
                            .FirstOrDefault());
                
                return result;
            }
            catch (Exception ex)
            {
                var error = new Error_Log()
                {
                    Error_Message = ex.Message,
                    Module_Name = nameof(EmployeeLogin),
                    Arise_Date = DateTime.Now,
                    Error_Trace = ex.StackTrace
                };

                await _commonService.InsertErrorLog(error);
                throw;
            }
        }
        public async Task<bool> IsExistUserName(string userName)
        {
            var empId = new SqlParameter("@employeeId", DBNull.Value);
            var uName = new SqlParameter("@userName", userName);
            

            var employee = await Task.Run(() => _dbContext.Employee_Master
                            .FromSqlRaw(@"exec Employee_Master_Select @employeeId, @userName", empId, uName).ToListAsync());
            if (employee != null && employee.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Employee Document
        public async Task<int> InsertEmployeeDocument(Employee_Document employee_Document)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@employeeId", employee_Document.Employee_Id));
            parameter.Add(new SqlParameter("@documentType", employee_Document.Document_Type));
            parameter.Add(new SqlParameter("@documentExpiryDate", employee_Document.Document_Expiry_Date));
            parameter.Add(new SqlParameter("@documentUrl", employee_Document.Document_Url));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Employee_Document_Insert_Update @employeeId, @documentType, @documentExpiryDate, @documentUrl, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateEmployeeDocument(Employee_Document employee_Document)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@employeeId", employee_Document.Employee_Id));
            parameter.Add(new SqlParameter("@documentType", employee_Document.Document_Type));
            parameter.Add(new SqlParameter("@documentExpiryDate", employee_Document.Document_Expiry_Date));
            parameter.Add(new SqlParameter("@documentUrl", employee_Document.Document_Url));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Employee_Document_Insert_Update @employeeId, @documentType, @documentExpiryDate, @documentUrl, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteEmployeeDocument(int employeeId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Employee_Document {employeeId}"));
        }
        public async Task<IList<Employee_Document>> GetEmployeeDocuments(int employeeId)
        {   
            var param = new SqlParameter("@employeeId", employeeId);

            var employeeDocument = await Task.Run(() => _dbContext.Employee_Document
                            .FromSqlRaw(@"exec Employee_Document_Select @employeeId", param).ToListAsync());

            return employeeDocument;
        }
        public async Task<Employee_Document> GetEmployeeDocumentById(int employeeId)
        {
            var param = new SqlParameter("@employeeId", employeeId);

            var employeeDocument = await Task.Run(() => _dbContext.Employee_Document
                            .FromSqlRaw(@"exec Employee_Document_Select @employeeId", param).ToListAsync());

            return employeeDocument.FirstOrDefault();
        }
        #endregion

        #region Employee Salary
        public async Task<int> InsertEmployeeSalary(Employee_Salary employee_Salary)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@employeeId", employee_Salary.Employee_Id));
            parameter.Add(new SqlParameter("@salary", employee_Salary.Salary));
            parameter.Add(new SqlParameter("@start_Date", employee_Salary.Start_Date));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Employee_Salary_Insert_Update @employeeId, @salary, @start_Date, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateEmployeeSalary(Employee_Salary employee_Salary)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@employeeId", employee_Salary.Employee_Id));
            parameter.Add(new SqlParameter("@salary", employee_Salary.Salary));
            parameter.Add(new SqlParameter("@start_Date", employee_Salary.Start_Date));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Employee_Salary_Insert_Update @employeeId, @salary, @start_Date, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteEmployeeSalary(int employeeId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Employee_Salary {employeeId}"));
        }
        public async Task<IList<Employee_Salary>> GetEmployeeSalary(int employeeId)
        {
            var param = new SqlParameter("@employeeId", employeeId);

            var employeeDocument = await Task.Run(() => _dbContext.Employee_Salary
                            .FromSqlRaw(@"exec Employee_Salary_Select @employeeId", param).ToListAsync());

            return employeeDocument;
        }
        public async Task<Employee_Salary> GetEmployeeSalaryById(int employeeId)
        {
            var param = new SqlParameter("@employeeId", employeeId);

            var employeeDocument = await Task.Run(() => _dbContext.Employee_Salary
                            .FromSqlRaw(@"exec Employee_Salary_Select @employeeId", param).ToListAsync());

            return employeeDocument.FirstOrDefault();
        }
        #endregion
        #endregion
    }
}
