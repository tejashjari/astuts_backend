using astute.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial class ProcessService : IProcessService
    {
        #region Fields
        private readonly AstuteDbContext _dbContext;
        #endregion

        #region Ctor
        public ProcessService(AstuteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<int> InsertProcessMas(Process_Mas process_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Process_Id", process_Mas.Process_Id));
            parameter.Add(new SqlParameter("@Process_Name", process_Mas.Process_Name));
            parameter.Add(new SqlParameter("@Process_Type", process_Mas.Process_Type));
            parameter.Add(new SqlParameter("@Order_No", process_Mas.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", process_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", process_Mas.status));
            parameter.Add(new SqlParameter("@recordType", "Insert"));

            var result = await Task.Run(() => _dbContext.Database
                                .ExecuteSqlRawAsync(@"exec Process_Mas_Insert_Update @Process_Id, @Process_Name, @Process_Type, @Order_No, @Sort_No, @status, @recordType", parameter.ToArray()));
            return result;
        }

        public async Task<int> UpdateProcessMas(Process_Mas process_Mas)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Process_Id", process_Mas.Process_Id));
            parameter.Add(new SqlParameter("@Process_Name", process_Mas.Process_Name));
            parameter.Add(new SqlParameter("@Process_Type", process_Mas.Process_Type));
            parameter.Add(new SqlParameter("@Order_No", process_Mas.Order_No));
            parameter.Add(new SqlParameter("@Sort_No", process_Mas.Sort_No));
            parameter.Add(new SqlParameter("@status", process_Mas.status));
            parameter.Add(new SqlParameter("@recordType", "Update"));

            var result = await Task.Run(() => _dbContext.Database
                                .ExecuteSqlRawAsync(@"exec Process_Mas_Insert_Update @Process_Id, @Process_Name, @Process_Type, @Order_No, @Sort_No, @status, @recordType", parameter.ToArray()));
            return result;
        }

        public async Task<int> DeleteProcessMas(int proccessId)
        {
            var isReferencedParameter = new SqlParameter("@IsReferenced", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _dbContext.Database
                                .ExecuteSqlRawAsync("EXEC Delete_Process_Mas @Process_Id, @IsReferenced OUT", new SqlParameter("@Process_Id", proccessId),
                                isReferencedParameter);

            var isReferenced = (bool)isReferencedParameter.Value;
            if (isReferenced)
                return 2;

            return result;
        }

        public async Task<Process_Mas> GetProcessById(int processId)
        {
            var param = new SqlParameter("@Process_Id", processId);

            var result = await Task.Run(() => _dbContext.Process_Mas
                            .FromSqlRaw(@"exec Process_Mas_Select @Process_Id", param)
                            .AsEnumerable()
                            .FirstOrDefault());
            return result;
        }

        public async Task<IList<Process_Mas>> GetAllProcessMas()
        {
            var result = await Task.Run(() => _dbContext.Process_Mas
                            .FromSqlRaw(@"exec Process_Mas_Select")
                            .ToListAsync());
            return result;
        }
        #endregion
    }
}
