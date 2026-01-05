using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Dapper;
using System.Data;
using System.Net;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class SunvahiService : IAddSunvahi
    {
        private readonly DapperContext _dappercontext;
        public SunvahiService(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;
        }
        public async Task<Status> AddSunvahiAsync(CaseHearing model)
        {
            var parameter = new
            {
                @HearingID = model.HearingID,
                @DCFCID = model.DCFCID,
                @HearingDate = model.HearingDate,
                @NextHearingDate = model.NextHearingDate,
                @CaseStatusId = model.CaseStatusId,
                @Remark = model.Remark,
                @ADATHAN_STATUS = model.ADATHAN_STATUS,
                @PROSECUTOR = model.Prosecutor,
                @MOBILE = model.Mobile,
                @ABHIYUKTHKAIVIRUDHMSG_ID=model.ABHIYUKTHKAIVIRUDHMSG_ID,
                @Sahayak_Abhiyukt_ID=model.Sahayak_Abhiyukt_ID,
                @CASERMK=model.CASERMK,
                @FDATE=model.FDATE

            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<Status>("ADD_SUNVAHI", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<ABHIYUKTH_SAVE_STATUS> AddAbhiyukthAsync(CSTATUSDETAILS model)
        {
            var parameter = new
            {
                @CSDID = model.CSDID,
                @DCFCID=model.DCFCID,
                @ABHIYUKTHKAIVIRUDHMSG_ID=model.ABHIYUKTHKAIVIRUDHMSG_ID,
                @Sahayak_Abhiyukt_ID=model.Sahayak_Abhiyukt_ID,
                @CASESTATUSID=model.CASESTATUSID,
                @REMARK=model.REMARK,
                @AdeshDate=model.AdeshDate
            };
            using (
            var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryFirstAsync<ABHIYUKTH_SAVE_STATUS>("CASE_DETAILS_SAVE", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<IEnumerable<CSTATUSDETAILS>> LstAbhiyukthAsync(string DCFCID)
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
                    return await connection.QueryAsync<CSTATUSDETAILS>("LSTABHIYUKTH", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
        public async Task<IEnumerable<CaseStatusMaster>> GetStatusLst()
        {
            
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<CaseStatusMaster>("GETCASE", commandType: CommandType.StoredProcedure);
                }
                catch { throw; }
                finally { connection.Close(); }
            }
        }
        public async Task<IEnumerable<Sunvahidetails>> Getdet(string id)
        {
            
            var parameter = new
            {
                @DCFCID = id,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<Sunvahidetails>("GETDET", parameter, commandType: CommandType.StoredProcedure);
                } catch { throw; }
                finally { connection.Close(); }
            }
        }

        public async Task<IEnumerable<AbhiyukthMsg>> Getabhiyukthmsg()
        {

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<AbhiyukthMsg>("GETABHIYUKTHMSG", commandType: CommandType.StoredProcedure);
                    }
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    ex.Message.ToString();
                    throw;
                }
                finally { connection.Close(); }

        }

        public async Task<IEnumerable<ABHIYUKTHDRP>> GetABHIYUKTHLst(String DCFCID)
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
                    return await connection.QueryAsync<ABHIYUKTHDRP>("GET_ABHIYUKTH_DRP", parameter, commandType: CommandType.StoredProcedure);
                }
                catch { throw; }
                finally { connection.Close(); }
            }

        }

        

        public Task<Status> UpdateSunvahiAsync(CaseHearing model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CaseHearing>> viewSunvahiformat(string DCFCID, string Hearingid)
        {
            var parameter = new
            {
                @Hearingid = Hearingid,
                @DCFCID = DCFCID,

                
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<CaseHearing>("VW_SUNVAHI_DET", parameter, commandType: CommandType.StoredProcedure);
                }
                catch { throw; }
                finally { connection.Close(); }
            }
        }

        public async Task<IEnumerable<SunvahiMandatoryDetails>> Getsunvahidet(string id)
        {

            var parameter = new
            {
                @DCFCID = id,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<SunvahiMandatoryDetails>("GETSUNVAHIMANDATORYDET", parameter, commandType: CommandType.StoredProcedure);
                }
                catch { throw; }
                finally { connection.Close(); }
            }
        }
    }
}
