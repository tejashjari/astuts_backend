using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface IPointerService
    {
        #region Pointer Master
        Task<int> InsertPointer(Pointer_Mas pointer_Mas);
        Task<int> UpdatePointer(Pointer_Mas pointer_Mas);
        Task<int> DeletePointer(int pointerId);
        Task<Pointer_Mas> GetPointerById(int pointerId);
        Task<IList<Pointer_Mas>> GetAllPointers();
        #endregion

        #region Pointer Detail
        Task<int> InsertPointerDetail(Pointer_Det pointer_Det);
        Task<int> UpdatePointerDetail(Pointer_Det pointer_Det);
        Task<int> DeletePointerDetail(int pointerId, string subPointerName);
        Task<Pointer_Det> GetPointerDetailByPointerIdAndSubPointerName(int pointerId, string subPointerName);
        Task<IList<Pointer_Det>> GetAllPointersDetail();
        #endregion
    }
}
