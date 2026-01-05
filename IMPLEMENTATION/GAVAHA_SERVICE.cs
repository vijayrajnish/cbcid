using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Dapper;
using System.Data;
using System.Net;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class GAVAHA_SERVICE : IGAVAHA
    {
        private readonly DapperContext _dappercontext;
        public GAVAHA_SERVICE(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;
        }
        public async Task<Status> AddGavahaAsync(Gavaha model)
        {
            var parameter = new
            {
                @GavahaId=model.GavahaId,
                @DCFCID = model.DCFCID,
                @GAVAHA_KA_PRAKAR_ID = model.GAVAHA_KA_PRAKAR_ID,
                @GavahaName=model.GavahaName,
                @ADDRESS=model.Address,
                @PHONENO=model.Phoneno,
                @ADHARNO=model.AdharNo,
                @EXAMINED=model.Examined,
                @Examineddate=model.Examineddate,
                @GhavahiNextdate=model.GhavahiNextdate,
                @GavaTypeId=model.GavaTypeId,
                @Remark =model.Remark,
                @OtherRemark= model.OtherRemark,
                @NonExaminedReasonId = model.NonExaminedReasonId,
                @CreatedBy = model.createdby,
            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("ADD_GAVAHA", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally { connection.Close(); }
            }
        }

        public Task<Status> UpdateGavahaAsync(Gavaha model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gavaha>> viewgavahaformat(string DCFCID, string gavahaid)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,
                @GAVAHAID = gavahaid
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<Gavaha>("VW_GAVAHA_DET", parameter, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    return Enumerable.Empty<Gavaha>();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<Status> DeleteGavaha(string DCFCID, string gavahaid,string ID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,
                @gavahaid = gavahaid,
                @ID= ID,
            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("DELETE_GAVAHA", parameter, commandType: CommandType.StoredProcedure);
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
