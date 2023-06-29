using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class PointerService : IPointerService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public PointerService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        #region Pointer Master
        public async Task<int> InsertPointer(Pointer_Mas pointer_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Pointer_Id", pointer_Mas.Pointer_Id));
            parameter.Add(new SqlParameter("@Pointer_Name", pointer_Mas.Pointer_Name));
            parameter.Add(new SqlParameter("@From_Cts", pointer_Mas.From_Cts));
            parameter.Add(new SqlParameter("@To_Cts", pointer_Mas.To_Cts));
            parameter.Add(new SqlParameter("@Pointer_Type", pointer_Mas.Pointer_Type));
            parameter.Add(new SqlParameter("@Order_No", pointer_Mas.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", pointer_Mas.Sort_No));
            parameter.Add(new SqlParameter("@Status", pointer_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
                        .ExecuteSqlRawAsync(@"exec Pointer_Mas_Insert_Update @Pointer_Id, @Pointer_Name, @From_Cts, @To_Cts, @Pointer_Type, @Order_No, @Sort_No, @Status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdatePointer(Pointer_Mas pointer_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Pointer_Id", pointer_Mas.Pointer_Id));
            parameter.Add(new SqlParameter("@Pointer_Name", pointer_Mas.Pointer_Name));
            parameter.Add(new SqlParameter("@From_Cts", pointer_Mas.From_Cts));
            parameter.Add(new SqlParameter("@To_Cts", pointer_Mas.To_Cts));
            parameter.Add(new SqlParameter("@Pointer_Type", pointer_Mas.Pointer_Type));
            parameter.Add(new SqlParameter("@Order_No", pointer_Mas.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", pointer_Mas.Sort_No));
            parameter.Add(new SqlParameter("@Status", pointer_Mas.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
                        .ExecuteSqlRawAsync(@"exec Pointer_Mas_Insert_Update @Pointer_Id, @Pointer_Name, @From_Cts, @To_Cts, @Pointer_Type, @Order_No, @Sort_No, @Status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeletePointer(int pointerId)
        {
            var isReferencedParameter = new SqlParameter("@IsReferenced", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _dbContext.Database
                                .ExecuteSqlRawAsync("EXEC Delete_Pointer_Mas @PointerId, @IsReferenced OUT", new SqlParameter("@PointerId", pointerId),
                                isReferencedParameter);

            var isReferenced = (bool)isReferencedParameter.Value;
            if (isReferenced)
                return 2;

            return result;
        }
        public async Task<Pointer_Mas> GetPointerById(int pointerId)
        {
            var param = new SqlParameter("@Pointer_Id", pointerId);

            var result = await Task.Run(() => _dbContext.Pointer_Mas
                            .FromSqlRaw(@"exec Pointer_Mas_Select @Pointer_Id", param)
                            .AsEnumerable()
                            .FirstOrDefault());
            return result;
        }
        public async Task<IList<Pointer_Mas>> GetAllPointers()
        {
            var result = await Task.Run(() => _dbContext.Pointer_Mas
                            .FromSqlRaw(@"exec Pointer_Mas_Select")
                            .ToListAsync());
            return result;
        }
        #endregion

        #region Pointer Detail
        public async Task<int> InsertPointerDetail(Pointer_Det pointer_Det)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Pointer_id", pointer_Det.Pointer_id));
            parameter.Add(new SqlParameter("@Sub_Pointer_Name", pointer_Det.Sub_Pointer_Name));
            parameter.Add(new SqlParameter("@From_Cts", pointer_Det.From_Cts));
            parameter.Add(new SqlParameter("@To_Cts", pointer_Det.To_Cts));
            parameter.Add(new SqlParameter("@Order_No", pointer_Det.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", pointer_Det.Sort_No));
            parameter.Add(new SqlParameter("@Status", pointer_Det.Status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
                        .ExecuteSqlRawAsync(@"exec Pointer_Det_Insert_Update @Pointer_id, @Sub_Pointer_Name, @From_Cts, @To_Cts, @Order_No, @Sort_No, @Status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdatePointerDetail(Pointer_Det pointer_Det)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Pointer_id", pointer_Det.Pointer_id));
            parameter.Add(new SqlParameter("@Sub_Pointer_Name", pointer_Det.Sub_Pointer_Name));
            parameter.Add(new SqlParameter("@From_Cts", pointer_Det.From_Cts));
            parameter.Add(new SqlParameter("@To_Cts", pointer_Det.To_Cts));
            parameter.Add(new SqlParameter("@Order_No", pointer_Det.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", pointer_Det.Sort_No));
            parameter.Add(new SqlParameter("@Status", pointer_Det.Status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
                        .ExecuteSqlRawAsync(@"exec Pointer_Det_Insert_Update @Pointer_id, @Sub_Pointer_Name, @From_Cts, @To_Cts, @Order_No, @Sort_No, @Status, @recordType", parameter.ToArray()));

            return result;
        }
        public async Task<int> DeletePointerDetail(int pointerId, string subPointerName)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Pointer_id", pointerId));
            parameter.Add(new SqlParameter("@Sub_Pointer_Name", subPointerName));

            var result = await Task.Run(() => _dbContext.Database
                         .ExecuteSqlRawAsync(@"exec Delete_Pointer_Det @Pointer_id, @Sub_Pointer_Name", parameter.ToArray()));

            return result;
        }
        public async Task<Pointer_Det> GetPointerDetailByPointerIdAndSubPointerName(int pointerId, string subPointerName)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Pointer_id", pointerId));
            parameter.Add(new SqlParameter("@Sub_Pointer_Name", subPointerName));

            var result = await Task.Run(() => _dbContext.Pointer_Det
                            .FromSqlRaw(@"exec Pointer_Det_Select @Pointer_id, @Sub_Pointer_Name", parameter.ToArray())
                            .AsEnumerable()
                            .FirstOrDefault());
            return result;
        }
        public async Task<IList<Pointer_Det>> GetAllPointersDetail()
        {
            var result = await Task.Run(() => _dbContext.Pointer_Det
                            .FromSqlRaw(@"exec Pointer_Det_Select")
                            .ToListAsync());
            return result;
        }
        #endregion
        #endregion
    }
}
