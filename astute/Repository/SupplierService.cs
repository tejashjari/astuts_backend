using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class SupplierService : ISupplierService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public SupplierService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<int> InsertSupplierValue(Supplier_Value supplier_Value)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SupId", supplier_Value.Sup_Id));
            parameter.Add(new SqlParameter("@SuppCatname", supplier_Value.Supp_Cat_name));
            parameter.Add(new SqlParameter("@CatValId", supplier_Value.Cat_Val_id));
            parameter.Add(new SqlParameter("@Status", supplier_Value.Status));
            parameter.Add(new SqlParameter("@RecordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
          .ExecuteSqlRawAsync(@"exec Supplier_Value_Insert_Update @SupId, @SuppCatname, @CatValId, @Status, @RecordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateSupplierValue(Supplier_Value supplier_Value)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SupId", supplier_Value.Sup_Id));
            parameter.Add(new SqlParameter("@SuppCatname", supplier_Value.Supp_Cat_name));
            parameter.Add(new SqlParameter("@CatValId", supplier_Value.Cat_Val_id));
            parameter.Add(new SqlParameter("@Status", supplier_Value.Status));
            parameter.Add(new SqlParameter("@RecordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
          .ExecuteSqlRawAsync(@"exec Supplier_Value_Insert_Update @SupId, @SuppCatname, @CatValId, @Status, @RecordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeleteSupplierValue(int supId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"Delete_Supplier_Value {supId}"));
        }
        public async Task<Supplier_Value> GetSupplierById(int supId)
        {
            var param = new SqlParameter("@supId", supId);

            var supplier = await Task.Run(() => _dbContext.Supplier_Value
                            .FromSqlRaw(@"exec GetSupplierById @supId", param).ToListAsync());

            return supplier.FirstOrDefault();
        }
        public async Task<IList<Supplier_Value>> GetSupplier_ValueByCatValId(int catValId)
        {
            var param = new SqlParameter("@catValId", catValId);

            var categoryValue = await Task.Run(() => _dbContext.Supplier_Value
                            .FromSqlRaw(@"exec GetSupplierValuesByCatValID @catValId", param).ToListAsync());

            return categoryValue;
        }
        #endregion
    }
}
