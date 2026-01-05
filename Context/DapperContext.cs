using Microsoft.Data.SqlClient;
using System.Data;

namespace CBCID_APPLICATION.Context
{
    public class DapperContext
    {
        private string _connectionstring;
        public DapperContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionstring);
        }
    }
}
