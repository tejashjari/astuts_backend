using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class TermsService : ITermsService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public TermsService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<int> InsertTerms(Terms_Mas terms_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@terms", terms_Mas.Terms));
            parameter.Add(new SqlParameter("@termDays", terms_Mas.Term_Days));
            parameter.Add(new SqlParameter("@orderNo", terms_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", terms_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", terms_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Terms_Mas_Insert_Update @terms, @termDays, @orderNo, @sortNo, @status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateTerms(Terms_Mas terms_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@terms", terms_Mas.Terms));
            parameter.Add(new SqlParameter("@termDays", terms_Mas.Term_Days));
            parameter.Add(new SqlParameter("@orderNo", terms_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", terms_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", terms_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Terms_Mas_Insert_Update @terms, @termDays, @orderNo, @sortNo, @status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteTerms(string terms)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Terms_Mas {terms}"));
        }
        public async Task<IList<Terms_Mas>> GetAllTerms()
        {   
            var result = await Task.Run(() => _dbContext.Terms_Mas
                            .FromSqlRaw(@"exec Terms_Mas_Select").ToListAsync());

            return result;
        }
        public async Task<Terms_Mas> GetTerms(string terms)
        {
            var param = new SqlParameter("@terms", terms);
            var result = await Task.Run(() => _dbContext.Terms_Mas
                            .FromSqlRaw(@"exec Terms_Mas_Select @terms", param)
                            .AsEnumerable()
                            .FirstOrDefault());

            return result;
        }
        #endregion
    }
}
