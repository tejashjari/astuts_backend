using astute.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface ICompanyService
    {
        #region Company Master
        Task<int> InsertCompany(Company_Master company_Master);
        Task<int> UpdateCompany(Company_Master company_Master);
        Task<int> DeleteCompany(int companyId);
        Task<Company_Master> GetCompanyById(int companyId);
        Task<IList<Company_Master>> GetAllCompanies();
        #endregion

        #region Company Document
        Task<int> InsertCompanyDocument(Company_Document company_Document);
        Task<int> UpdateCompanyDocument(Company_Document company_Document);
        Task<int> DeleteCompanyDocument(int companyId, int catvalId, DateTime startDate);
        Task<Company_Document> GetCompanyDocument(int companyId, int catvalId, DateTime startDate);
        #endregion

        #region Company Media
        Task<int> InsertCompanyMedia(Company_Media company_Media);
        Task<int> UpdateCompanyMedia(Company_Media company_Media);
        Task<int> DeleteCompanyMedia(int companyId, int catvalId);
        Task<Company_Media> GetCompanyMedia(int companyId, int catvalId);
        #endregion

        #region Company Bank
        Task<int> InsertCompanyBank(Company_Bank company_Bank);
        Task<int> UpdateCompanyBank(Company_Bank company_Bank);
        Task<int> DeleteCompanyBank(int companyId, int bankId);
        Task<Company_Bank> GetCompanyBank(int companyId, int bankId);
        #endregion
    }
}
