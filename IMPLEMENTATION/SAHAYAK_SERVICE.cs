using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Dapper;
using System.Data;
using System.Net;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class SAHAYAK_SERVICE : ISAHAYA_ABIYUKT
    {
        private readonly DapperContext _dappercontext;
        public SAHAYAK_SERVICE(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;
        }
        public async Task<Status> AddsahayakAsync(SAHAYA model)
        {
            var parameter = new
            {
                @Sahayak_Abhiyukt_ID = model.Sahayak_Abhiyukt_ID,
                @DCFCID = model.DCFCID,
                @Name = model.Name,
                @Address = model.Address,
                @FATHER = model.Father,
                @Phoneno = model.Phoneno,
                @AdharNo = model.AdharNo,
                @Status = model.AbhiyuktStatus,
            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("ADD_SAHAYAK", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        public Task<Status> UpdatesahayakAsync(SAHAYA model)
        {
            throw new NotImplementedException();
        }
        public async Task<Status> DeletesahayakAsync(string DCFCID, string Sahayak_Abhiyukt_ID)
        {
            var parameter = new
            {
                @Sahayak_Abhiyukt_ID = Sahayak_Abhiyukt_ID,
                @DCFCID = DCFCID,
                
            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("DELETE_SAHAYAK", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        public async Task<IEnumerable<SAHAYA>> Viewsahayakformat(string DCFCID, string Sahayak_Abhiyukt_ID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,
                @Sahayak_Abhiyukt_ID = Sahayak_Abhiyukt_ID
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<SAHAYA>("VWsahayakformat", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

        }
    }
}

