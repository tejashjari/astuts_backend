using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class CommonService : ICommonService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public CommonService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        #region Country Master
        public async Task<int> InsertCountry(Country_Mas country_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@countryId", country_Mas.Country_Id));
            parameter.Add(new SqlParameter("@country", country_Mas.Country));
            parameter.Add(new SqlParameter("@isdCode", country_Mas.Isd_Code));
            parameter.Add(new SqlParameter("@orderNo", country_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", country_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", country_Mas.Status));
            parameter.Add(new SqlParameter("@shortCode", country_Mas.Short_Code));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Country_Mas_Insert_Update @countryId, @country, @isdCode, @orderNo, @sortNo, @status, @shortCode, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateCountry(Country_Mas country_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@countryId", country_Mas.Country_Id));
            parameter.Add(new SqlParameter("@country", country_Mas.Country));
            parameter.Add(new SqlParameter("@isdCode", country_Mas.Isd_Code));
            parameter.Add(new SqlParameter("@orderNo", country_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", country_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", country_Mas.Status));
            parameter.Add(new SqlParameter("@shortCode", country_Mas.Short_Code));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec Country_Mas_Insert_Update @countryId, @country, @isdCode, @orderNo, @sortNo, @status, @shortCode, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteCountry(int countryId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Country_Mas {countryId}"));
        }
        //public async Task<IList<Country_Mas>> GetCountries(int countryId, string countryName, string isdCode, string shortCode)
        //{
        //    var result = new List<Country_Mas>();
        //    var parameter = new List<SqlParameter>();
        //    if (countryId > 0)
        //    {
        //        parameter.Add(new SqlParameter("@countryId", countryId));
        //        result = await Task.Run(() => _dbContext.Country_Mas
        //        .FromSqlRaw(@"exec Country_Mas_Select @countryId", parameter.ToArray()).ToListAsync());
        //    }
        //    if (!string.IsNullOrEmpty(countryName))
        //    {
        //        parameter.Add(new SqlParameter("@countryId", DBNull.Value));
        //        parameter.Add(new SqlParameter("@countryName", countryName));
        //        result = await Task.Run(() => _dbContext.Country_Mas
        //        .FromSqlRaw(@"exec Country_Mas_Select @countryId, @countryName", parameter.ToArray()).ToListAsync());
        //    }
        //    if (!string.IsNullOrEmpty(isdCode))
        //    {   
        //        parameter.Add(new SqlParameter("@countryId", DBNull.Value));
        //        parameter.Add(new SqlParameter("@countryName", DBNull.Value));
        //        parameter.Add(new SqlParameter("@isdCode", isdCode));
        //        result = await Task.Run(() => _dbContext.Country_Mas
        //        .FromSqlRaw(@"exec Country_Mas_Select @countryId, @countryName, @isdCode", parameter.ToArray()).ToListAsync());
        //    }
        //    if (!string.IsNullOrEmpty(shortCode))
        //    {
        //        parameter.Add(new SqlParameter("@countryId", DBNull.Value));
        //        parameter.Add(new SqlParameter("@countryName", DBNull.Value));
        //        parameter.Add(new SqlParameter("@isdCode", DBNull.Value));
        //        parameter.Add(new SqlParameter("@shortCode", shortCode));
        //        result = await Task.Run(() => _dbContext.Country_Mas
        //        .FromSqlRaw(@"exec Country_Mas_Select @countryId, @countryName, @isdCode, @shortCode", parameter.ToArray()).ToListAsync());
        //    }
        //    if(countryId == 0 && string.IsNullOrEmpty(countryName) && string.IsNullOrEmpty(isdCode) && string.IsNullOrEmpty(shortCode))
        //    {
        //        result = await Task.Run(() => _dbContext.Country_Mas
        //        .FromSqlRaw(@"exec Country_Mas_Select").ToListAsync());
        //    }
        //    return result;
        //}

        public async Task<IList<Country_Mas>> GetCountries(int countryId, string countryName, string isdCode, string shortCode)
        {
            var result = new List<Country_Mas>();

            var CountryId = countryId > 0 ? new SqlParameter("@countryId", countryId) : new SqlParameter("@countryId", DBNull.Value);
            var CountryName = !string.IsNullOrEmpty(countryName) ? new SqlParameter("@countryName", countryName) : new SqlParameter("@countryName", DBNull.Value);
            var IsdCode = !string.IsNullOrEmpty(isdCode) ? new SqlParameter("@isdCode", isdCode) : new SqlParameter("@isdCode", DBNull.Value);
            var ShortCode = string.IsNullOrEmpty(shortCode) ? new SqlParameter("@shortCode", shortCode) : new SqlParameter("@shortCode", DBNull.Value);

            result = await Task.Run(() => _dbContext.Country_Mas
                 .FromSqlRaw(@"exec Country_Mas_Select @countryId, @countryName, @isdCode, @shortCode", CountryId, CountryName, IsdCode, ShortCode).ToListAsync());

            return result;
        }
        public async Task<Country_Mas> GetCountryById(int countryId)
        {
            var param = new SqlParameter("@countryId", countryId);

            var country = await Task.Run(() => _dbContext.Country_Mas
                            .FromSqlRaw(@"exec Country_Mas_Select @countryId", param).ToListAsync());

            return country.FirstOrDefault();
        }
        #endregion

        #region State Master
        public async Task<int> InsertState(State_Mas state_Mas)
        {
            var parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@stateId", state_Mas.State_Id));
            parameter.Add(new SqlParameter("@state", state_Mas.State));
            parameter.Add(new SqlParameter("@stdCode", state_Mas.Std_Code));
            parameter.Add(new SqlParameter("@countryId", state_Mas.Country_id));
            parameter.Add(new SqlParameter("@orderNo", state_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", state_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", state_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec State_Mas_Insert_Update @stateId, @state, @stdCode, @countryId, @orderNo, @sortNo, @status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateState(State_Mas state_Mas)
        {
            var parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@stateId", state_Mas.State_Id));
            parameter.Add(new SqlParameter("@state", state_Mas.State));
            parameter.Add(new SqlParameter("@stdCode", state_Mas.Std_Code));
            parameter.Add(new SqlParameter("@countryId", state_Mas.Country_id));
            parameter.Add(new SqlParameter("@orderNo", state_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", state_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", state_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec State_Mas_Insert_Update @stateId, @state, @stdCode, @countryId, @orderNo, @sortNo, @status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteState(int stateId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_State_Mas {stateId}"));
        }

        public async Task<IList<State_Mas>> GetStates(int stateId, string state, int countryId, string stdCode)
        {
            var result = new List<State_Mas>();
            var parameter = new List<SqlParameter>();
            if (stateId > 0)
            {
                parameter.Add(new SqlParameter("@stateId", stateId));
                result = await Task.Run(() => _dbContext.State_Mas
                .FromSqlRaw(@"exec State_Mas_Select @stateId", parameter.ToArray()).ToListAsync());
            }
            if (!string.IsNullOrEmpty(state))
            {
                parameter.Add(new SqlParameter("@stateId", DBNull.Value));
                parameter.Add(new SqlParameter("@state", state));
                result = await Task.Run(() => _dbContext.State_Mas
                .FromSqlRaw(@"exec State_Mas_Select @stateId, @state", parameter.ToArray()).ToListAsync());
            }
            if (countryId > 0)
            {
                parameter.Add(new SqlParameter("@stateId", DBNull.Value));
                parameter.Add(new SqlParameter("@state", DBNull.Value));
                parameter.Add(new SqlParameter("@countryId", countryId));
                result = await Task.Run(() => _dbContext.State_Mas
                .FromSqlRaw(@"exec State_Mas_Select @stateId, @state, @countryId", parameter.ToArray()).ToListAsync());
            }
            if (!string.IsNullOrEmpty(stdCode))
            {
                parameter.Add(new SqlParameter("@stateId", DBNull.Value));
                parameter.Add(new SqlParameter("@state", DBNull.Value));
                parameter.Add(new SqlParameter("@countryId", DBNull.Value));
                parameter.Add(new SqlParameter("@stdCode", stdCode));
                result = await Task.Run(() => _dbContext.State_Mas
                .FromSqlRaw(@"exec State_Mas_Select @stateId, @state, @countryId, @stdCode", parameter.ToArray()).ToListAsync());
            }
            if (stateId == 0 && string.IsNullOrEmpty(state) && countryId == 0 && string.IsNullOrEmpty(state))
            {
                result = await Task.Run(() => _dbContext.State_Mas
                .FromSqlRaw(@"exec State_Mas_Select").ToListAsync());
            }
            return result;
        }
        #endregion

        #region City Master
        public async Task<int> InsertCity(City_Mas city_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@cityId", city_Mas.City_Id));
            parameter.Add(new SqlParameter("@city", city_Mas.City));
            parameter.Add(new SqlParameter("@stateId", city_Mas.State_Id));
            parameter.Add(new SqlParameter("@orderNo", city_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", city_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", city_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec City_Mas_Insert_Update @cityId, @city, @stateId, @orderNo, @sortNo, @status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateCity(City_Mas city_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@cityId", city_Mas.City_Id));
            parameter.Add(new SqlParameter("@city", city_Mas.City));
            parameter.Add(new SqlParameter("@stateId", city_Mas.State_Id));
            parameter.Add(new SqlParameter("@orderNo", city_Mas.Order_No));
            parameter.Add(new SqlParameter("@sortNo", city_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", city_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec City_Mas_Insert_Update @cityId, @city, @stateId, @orderNo, @sortNo, @status, @recordType", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteCity(int cityId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_City_Mas {cityId}"));
        }

        public async Task<IList<City_Mas>> GetCity(int cityId, string city, int stateId)
        {
            var result = new List<City_Mas>();
            var parameter = new List<SqlParameter>();
            if (cityId > 0)
            {
                parameter.Add(new SqlParameter("@cityId", cityId));
                result = await Task.Run(() => _dbContext.City_Mas
                .FromSqlRaw(@"exec City_Mas_Select @cityId", parameter.ToArray()).ToListAsync());
            }
            if (!string.IsNullOrEmpty(city))
            {
                parameter.Add(new SqlParameter("@cityId", DBNull.Value));
                parameter.Add(new SqlParameter("@city", city));
                result = await Task.Run(() => _dbContext.City_Mas
                .FromSqlRaw(@"exec City_Mas_Select @cityId, @city", parameter.ToArray()).ToListAsync());
            }
            if (stateId > 0)
            {
                parameter.Add(new SqlParameter("@cityId", DBNull.Value));
                parameter.Add(new SqlParameter("@city", DBNull.Value));
                parameter.Add(new SqlParameter("@stateId", stateId));
                result = await Task.Run(() => _dbContext.City_Mas
                .FromSqlRaw(@"exec City_Mas_Select @cityId, @city, @stateId", parameter.ToArray()).ToListAsync());
            }
            if (cityId == 0 && string.IsNullOrEmpty(city) && stateId == 0)
            {
                result = await Task.Run(() => _dbContext.City_Mas
                            .FromSqlRaw(@"exec City_Mas_Select").ToListAsync());
            }
           
            return result;
        }
        #endregion

        #region Error Log
        public async Task<int> InsertErrorLog(Error_Log error_Log)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Id", error_Log.Id));
            parameter.Add(new SqlParameter("@Error_Message", error_Log.Error_Message));
            parameter.Add(new SqlParameter("@Module_Name", error_Log.Module_Name));
            parameter.Add(new SqlParameter("@Arise_Date", error_Log.Arise_Date));
            parameter.Add(new SqlParameter("@Error_Trace", error_Log.Error_Trace));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@" exec Error_Log_Insert @Id, @Error_Message, @Module_Name, @Arise_Date, @Error_Trace", parameter.ToArray()));

            return result;
        }
        #endregion

        #region Years
        public async Task<IList<Year_Mas>> GetAllYears()
        {
            var result = await Task.Run(() => _dbContext.Year_Mas
                            .FromSqlRaw(@"exec Year_Mas_Select").ToListAsync());

            return result;
        }
        #endregion

        #region Quote Mas
        public async Task<Quote_Mas_Model> GetRandomQuote()
        {
            var result = await Task.Run(() => _dbContext.Quote_Mas_Model
                            .FromSqlRaw(@"exec Quote_Mas_Random_Select")
                            .AsEnumerable()
                            .FirstOrDefault());

            return result;
        }
        #endregion
        #endregion
    }
}
