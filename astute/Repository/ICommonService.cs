using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface ICommonService
    {
        #region Country Master
        Task<int> InsertCountry(Country_Mas country_Mas);
        Task<int> UpdateCountry(Country_Mas country_Mas);
        Task<int> DeleteCountry(int countryId);
        Task<IList<Country_Mas>> GetCountry(Country_Mas country_Mas);
        #endregion

        #region State Master
        Task<int> InsertState(State_Mas state_Mas);
        Task<int> UpdateState(State_Mas state_Mas);
        Task<int> DeleteState(int stateId);
        Task<IList<State_Mas>> GetStates(State_Mas state_Mas);
        #endregion

        #region City Master
        Task<int> InsertCity(City_Mas city_Mas);
        Task<int> UpdateCity(City_Mas city_Mas);
        Task<int> DeleteCity(int cityId);
        Task<IList<City_Mas>> GetCity(int cityId, string city, int stateId);
        #endregion

        #region Error Log
        Task<int> InsertErrorLog(Error_Log error_Log);
        #endregion

        #region Years
        Task<IList<Year_Mas>> GetAllYears();
        #endregion

        #region Quote Mas
        Task<Quote_Mas_Model> GetRandomQuote();
        #endregion
    }
}
