using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface IProcessService
    {
        Task<int> InsertProcessMas(Process_Mas process_Mas);
        Task<int> UpdateProcessMas(Process_Mas process_Mas);
        Task<int> DeleteProcessMas(int proccessId);
        Task<Process_Mas> GetProcessById(int processId);
        Task<IList<Process_Mas>> GetAllProcessMas();
    }
}
