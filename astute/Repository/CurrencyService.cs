using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class CurrencyService : ICurrencyService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public CurrencyService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<int> InsertCurrency(Currency_Mas currency_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Currency", currency_Mas.Currency));
            parameter.Add(new SqlParameter("@Currency_Name", currency_Mas.Currency_Name));
            parameter.Add(new SqlParameter("@Symbol", currency_Mas.Symbol));
            parameter.Add(new SqlParameter("@Order_No", currency_Mas.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", currency_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", currency_Mas.status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
                                                .ExecuteSqlRawAsync(@"exec Currency_Mas_Insert_Update @Currency, @Currency_Name, @Symbol, @Order_No, @Sort_No, @status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateCurrency(Currency_Mas currency_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Currency", currency_Mas.Currency));
            parameter.Add(new SqlParameter("@Currency_Name", currency_Mas.Currency_Name));
            parameter.Add(new SqlParameter("@Symbol", currency_Mas.Symbol));
            parameter.Add(new SqlParameter("@Order_No", currency_Mas.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", currency_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", currency_Mas.status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
                                                .ExecuteSqlRawAsync(@"exec Currency_Mas_Insert_Update @Currency, @Currency_Name, @Symbol, @Order_No, @Sort_No, @status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteCurrency(string currency)
        {
            var isReferencedParameter = new SqlParameter("@IsReferenced", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _dbContext.Database
                                .ExecuteSqlRawAsync("EXEC Delete_Currency_Mas @Currency, @IsReferenced OUT", new SqlParameter("@Currency", currency),
                                isReferencedParameter);

            var isReferenced = (bool)isReferencedParameter.Value;
            if (isReferenced)
                return 2;

            return result;
        }

        public async Task<Currency_Mas> GetCurrencyByCurrency(string currency)
        {
            var param = new SqlParameter("@Currency", currency);

            var result = await Task.Run(() => _dbContext.Currency_Mas
                            .FromSqlRaw(@"exec Currency_Mas_Select @Currency", param)
                            .AsEnumerable()
                            .FirstOrDefault());
            return result;
        }

        public async Task<IList<Currency_Mas>> GetAllCurrency()
        {
            var result = await Task.Run(() => _dbContext.Currency_Mas
                            .FromSqlRaw(@"exec Currency_Mas_Select")
                            .ToListAsync());
            return result;
        }
        #endregion
    }
}
