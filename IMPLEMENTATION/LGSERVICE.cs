using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.DomainLayer;
using CBCID_APPLICATION.ICBCID;
using Dapper;
using System.Data;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class LGSERVICE : ILGVIEW
    {
        private readonly DapperContext _dappercontext;
        public LGSERVICE(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;
        }
        public async Task<LGVW> GetLgDetails(string uname, string password)
        {
            try
            {
                var parameter = new
                {
                    @USERNAME = uname,
                    @PASSWORD = password
                };
                using (
                var connection = _dappercontext.CreateConnection())
                {
                    return await connection.QueryFirstAsync<LGVW>("USP_LOGIN", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch (InvalidOperationException ex)
            {
                //if it's an InvalidOperationException return 0
                return null;
            }

        }
    }
}
