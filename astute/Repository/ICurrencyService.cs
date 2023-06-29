using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface ICurrencyService
    {
        Task<int> InsertCurrency(Currency_Mas currency_Mas);
        Task<int> UpdateCurrency(Currency_Mas currency_Mas);
        Task<int> DeleteCurrency(string currency);
        Task<Currency_Mas> GetCurrencyByCurrency(string currency);
        Task<IList<Currency_Mas>> GetAllCurrency();
    }
}
