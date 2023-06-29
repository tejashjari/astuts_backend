using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IList<Country_Mas>> GetCountry(Country_Mas country_Mas)
        {
            var result = new List<Country_Mas>();

            var CountryId = country_Mas.Country_Id > 0 ? new SqlParameter("@countryId", country_Mas.Country_Id) : new SqlParameter("@countryId", DBNull.Value);
            var CountryName = !string.IsNullOrEmpty(country_Mas.Country) ? new SqlParameter("@countryName", country_Mas.Country) : new SqlParameter("@countryName", DBNull.Value);
            var IsdCode = !string.IsNullOrEmpty(country_Mas.Isd_Code) ? new SqlParameter("@isdCode", country_Mas.Isd_Code) : new SqlParameter("@isdCode", DBNull.Value);
            var ShortCode = !string.IsNullOrEmpty(country_Mas.Short_Code) ? new SqlParameter("@shortCode", country_Mas.Short_Code) : new SqlParameter("@shortCode", DBNull.Value);

            result = await Task.Run(() => _dbContext.Country_Mas
                 .FromSqlRaw(@"exec Country_Mas_Select @countryId, @countryName, @isdCode, @shortCode", CountryId, CountryName, IsdCode, ShortCode).ToListAsync());

            return result;
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

        public async Task<IList<State_Mas>> GetStates(State_Mas state_Mas)
        {
            var StateId = state_Mas.State_Id > 0 ? new SqlParameter("@stateId", state_Mas.State_Id) : new SqlParameter("@stateId", DBNull.Value);
            var State = !string.IsNullOrEmpty(state_Mas.State) ? new SqlParameter("@state", state_Mas.State) : new SqlParameter("@state", DBNull.Value);
            var CountryId = state_Mas.Country_id > 0 ? new SqlParameter("@countryId", state_Mas.Country_id) : new SqlParameter("@countryId", DBNull.Value);
            var StdCode = !string.IsNullOrEmpty(state_Mas.Std_Code) ? new SqlParameter("@stdCode", state_Mas.Std_Code) : new SqlParameter("@stdCode", DBNull.Value);

            var result = await Task.Run(() => _dbContext.State_Mas
                        .FromSqlRaw(@"exec State_Mas_Select @stateId, @state, @countryId, @stdCode", StateId, State, CountryId, StdCode).ToListAsync());

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
            var CityId = cityId > 0 ? new SqlParameter("@cityId", cityId) : new SqlParameter("@cityId", DBNull.Value);
            var City = !string.IsNullOrEmpty(city) ? new SqlParameter("@city", city) : new SqlParameter("@city", DBNull.Value);
            var StateId = cityId > 0 ? new SqlParameter("@stateId", cityId) : new SqlParameter("@stateId", DBNull.Value);


            var result = await Task.Run(() => _dbContext.City_Mas
                           .FromSqlRaw(@"exec City_Mas_Select @cityId, @city, @stateId", CityId, City, StateId).ToListAsync());

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
