using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;

using Dapper;
using System.Data;
using System.Net;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class ChgPassword : IChangePassword
    {
        private readonly DapperContext _dappercontext;
        public ChgPassword(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;
        }
        public async Task<Status> UpdatePasswordAsync(Utility_Users model)
        {
            var parameter = new
            {
                @Empid = model.EmployeeId,
                @password = model.Password,
                
            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("UPTPASSWORD", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally { connection.Close(); }
            }
        }
    }
}
