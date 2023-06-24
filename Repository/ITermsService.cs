using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface ITermsService
    {
        Task<int> InsertTerms(Terms_Mas terms_Mas);
        Task<int> UpdateTerms(Terms_Mas terms_Mas);
        Task<int> DeleteTerms(string terms);
        Task<IList<Terms_Mas>> GetAllTerms();
        Task<Terms_Mas> GetTerms(string terms);
    }
}
