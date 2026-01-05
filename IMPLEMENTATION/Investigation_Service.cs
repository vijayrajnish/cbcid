using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Dapper;
using System.Data;
using System.Net;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class Investigation_Service: IInvestigation_officer
    {
        private readonly DapperContext _dappercontext;
        public Investigation_Service(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;
        }
        public async Task<Status> AddInvestOfficerAsync(INVEST_OFFICER model)
        {
            var parameter = new
            {
                @INVESTIGATION_OFFICER_ID = model.INVESTIGATION_OFFICER_ID,
                @DCFCID = model.DCFCID,
                @OFFICER_NAME = model.OFFICER_NAME,
                @FROMDATE = model.FROMDATE,
                @TODATE = model.TODATE,
                @DESIGNATIONID=model.DESIGNATIONID,
                @PHONENO= model.PHONENO,

            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("ADD_INVEST_OFFICER", parameter, commandType: CommandType.StoredProcedure);
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
        public Task<Status> UpdateInvestOfficerAsync(INVEST_OFFICER model)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<INVEST_OFFICER>> ViewInvestformat(string DCFCID, string INVESTIGATION_OFFICER_ID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,
                @INVESTIGATION_OFFICER_ID = INVESTIGATION_OFFICER_ID
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<INVEST_OFFICER>("VW_INVEST_DET", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<INVEST_OFFICER>> Getdet(string DCFCID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<INVEST_OFFICER>("GETINVESTDET", parameter, commandType: CommandType.StoredProcedure);
                }
                catch { throw; }
                finally { connection.Close(); }
            }
        }

        public async Task<IEnumerable<DrpInvest>> GetInvestigationLst()
        {

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<DrpInvest>("GETINVESTIGATION_OFFICER_LST", commandType: CommandType.StoredProcedure);
                    }
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    ex.Message.ToString();
                    throw;
                }
                finally
                {
                    connection.Close();
                }

        }

        public async Task<Status> AddOfficerAsync(OFFICERMASTER model)
        {
            var parameter = new
            {
                
                @OFFICER_NAME = model.OFF_NAME,
                

            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("ADD_OFFICER", parameter, commandType: CommandType.StoredProcedure);
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
