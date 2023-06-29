using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface ISupplierService
    {
        Task<int> InsertSupplierValue(Supplier_Value supplier_Value);
        Task<int> UpdateSupplierValue(Supplier_Value supplier_Value);
        Task<int> DeleteSupplierValue(int supId);
        Task<Supplier_Value> GetSupplierById(int supId);
        Task<IList<Supplier_Value>> GetSupplier_ValueByCatValId(int catValId);
    }
}
