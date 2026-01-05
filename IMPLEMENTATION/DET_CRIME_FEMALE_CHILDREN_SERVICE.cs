using Azure.Core;
using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.DomainLayer;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System.Data;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient.Diagnostics;

namespace CBCID_APPLICATION.IMPLEMENTATION
{
    public class DET_CRIME_FEMALE_CHILDREN_SERVICE : IDET_CRIME_FEMALE_CHILDREN
    {
        private readonly DapperContext _dappercontext;
        public DET_CRIME_FEMALE_CHILDREN_SERVICE(DapperContext dapperContext)
        {
            _dappercontext = dapperContext;

        }

        public async Task<IEnumerable<Fmtvw>> Getformat1ByID(string DCFCID)
        {
            
            var parameter = new
            {
                @DCFCID = DCFCID
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<Fmtvw>("VW_FORMAT_DISPLAY_BY_ID", parameter, commandType: CommandType.StoredProcedure);

                    }
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
           
        }

        public async Task<IEnumerable<MDistrict>> GetDistrictLst(string dsid)
        {
           
            var parameter = new
            {
                @dsid = dsid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<MDistrict>("GETDISTRICT", parameter, commandType: CommandType.StoredProcedure);
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
        public async Task<IEnumerable<MDistrict>> GetDistrictSectorwiseLst(string secid)
        {
            
            var parameter = new
            {
                @secid = secid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<MDistrict>("GETDISTRICT_WISE_SECTORID", parameter, commandType: CommandType.StoredProcedure);
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


        public async Task<IEnumerable<MDistrict>> GetDistrictALLLst(string secid)
        {
            var parameter = new { @secid = secid };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    {
                        return await connection.QueryAsync<MDistrict>("GETALLDISTRICT", parameter, commandType: CommandType.StoredProcedure);
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    ex.Message.ToString();
                    throw;
                }
                finally { connection.Close(); }
            }
        }

        public async Task<IEnumerable<MSector>> GetSectorALLLst(string secid)
        {
            var parameter = new { @secid = secid };

            try
            {
                using (var connection = _dappercontext.CreateConnection())
                {
                    return await connection.QueryAsync<MSector>(
                        "GETALLSECTOR",
                        parameter,
                        commandType: CommandType.StoredProcedure
                        
                    );
                    
                }
            }
            catch (Exception ex)
            {
                
                ex.Message.ToString();
                // Proper exception handling - log or rethrow
                //ILogger.LogError(ex, "Error while fetching sectors.");
                throw; // Or handle appropriately
            }
        }


        public async Task<IEnumerable<MSector>> GetSectorLst(string secid)
        {
            
            var parameter = new
            {
                @secid = secid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<MSector>("GETSECTOR_DASH_URL", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<MSector>> GetSectorKhandLst(string secid)
        {

            var parameter = new
            {
                @secid = secid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<MSector>("GET_SECTOR_KHAND", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<Gavaha_Cnt>> GetGavahaCnt(string DCFCID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,
                
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<Gavaha_Cnt>("GETGCOUNT", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<AbhiyukCnt>> GetAbhiyuktCnt(string DCFCID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,

            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<AbhiyukCnt>("GET_ABHIYUKTH_COUNT", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<INVESTIGATION_OFFICER_CNT>> GetINVESTCnt(string DCFCID)
        {
            var parameter = new
            {
                @DCFCID = DCFCID,

            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<INVESTIGATION_OFFICER_CNT>("GET_INVESTIGATION_OFFICER_COUNT", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<CaseStatusMaster>> GetStatusLst()
        {
            
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<CaseStatusMaster>("GETCASE", commandType: CommandType.StoredProcedure);
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
        public async Task<IEnumerable<MYR>> GetYRLst()
        {
           
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<MYR>("GETYRLST", commandType: CommandType.StoredProcedure);

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
        public async Task<IEnumerable<AdminDashBoardView>> GetFormatLst(string distid, string scid)
        {
            var parameter = new
            {
                @dsid = distid,
                @scid = scid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<AdminDashBoardView>("VW_ADMIN_DASHBOARD_VIEW", parameter, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<AdminDashBoardView>> GetFormatLstCIS(string distid, string scid)
        {
            var parameter = new
            {
                @dsid = distid,
                @scid = scid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<AdminDashBoardView>("VW_ADMIN_DASHBOARD_VIEW_CIS", parameter, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<MThana>> GetThanaLst(string dsid)
        {
            var parameter = new
            {
                @dsid = dsid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<MThana>("GETTHANA", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<FMTMASTER>> GetFMTLst(string fmtid)
        {
            
            var parameter = new
            {
                @fmtid = fmtid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<FMTMASTER>("GETFMT", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<FMTMASTER>> GetALLFMTLst()
        {
            

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<FMTMASTER>("GETALLFMT", commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<Court>> GetCourtLst()
        {
            
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<Court>("GETCOURT", commandType: CommandType.StoredProcedure);
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


        

        public async Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatAdminSrh(string scid, string dsid, string cbcid, string fmttypeid, string CaseStatusId,string Appstatus, string CREATEDBY, string YEAR, string APRADHYEAR)
        {
            
            var parameter = new
            {
                @scid = scid,
                @distid = dsid,
                @cbcid = cbcid,
                @fmttypeid = fmttypeid,
                @casestatusId = CaseStatusId,
                @Appstatus = Appstatus,
                @CREATEDBY = CREATEDBY,
                @YEAR = YEAR,
                @APRADHYEAR = APRADHYEAR
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<VW_DET_CRIME_FEM_CHILD>("VW_FORMAT_DISPLAY_ADMIN_SRH", parameter, commandType: CommandType.StoredProcedure,commandTimeout:180);
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


        public async Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatAdminSrhAdmin(string scid, string dsid, string cbcid, string fmttypeid)
        {
            var parameter = new
            {
                @scid = scid,
                @distid = dsid,
                @cbcid = cbcid,
                @fmttypeid = fmttypeid
            };  
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<VW_DET_CRIME_FEM_CHILD>("VW_FORMAT_DISPLAY_SRH_CIS", parameter, commandType: CommandType.StoredProcedure);
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


        public async Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformat1(string dsid)
        {
            var parameter = new
            {
                @distid = dsid,
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<VW_DET_CRIME_FEM_CHILD>("VW_FORMAT_DISPLAY", parameter, commandType: CommandType.StoredProcedure);
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

        //17june2025 code
        public async Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatsrh(string APDHNO, string cbcid, string fmttypeid, string sid, string Fdate, string Tdate, string CaseStatusId, string DISTRICTID,string dfif,string SIBNO,string strrole,string CRNO)
        {
            var parameter = new
            {
                @APRADHNO = APDHNO,
                @cbcid = cbcid,
                @fmttypeid = fmttypeid,
                @sid = sid,
                @FRMDATE= Fdate,
                @TODATE=Tdate,
                @CaseStatusId = CaseStatusId,
                @DISTRICTID = DISTRICTID,
                @dfif = dfif,
                @SBNO = SIBNO,
                @strrole= strrole,
                @CRNO = CRNO,
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<VW_DET_CRIME_FEM_CHILD>("VW_FORMAT_DISTRICT_DISPLAY_SRH", parameter, commandType: CommandType.StoredProcedure, commandTimeout: 180);
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

        public async Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatsrhCIS(string APDHNO, string cbcid, string fmttypeid, string sid)
        {
            
            var parameter = new
            {
                @APRADHNO = APDHNO,
                @cbcid = cbcid,
                @fmttypeid = fmttypeid,
                @sid = sid
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<VW_DET_CRIME_FEM_CHILD>("VW_FORMAT_DISTRICT_DISPLAY_SRH_CIS", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewAllformatsrh(string APDHNO, string cbcid, string fmttypeid)
        {
            
            var parameter = new
            {
                @APRADHNO = APDHNO,
                @cbcid = cbcid,
                @fmttypeid = fmttypeid
                
            };
            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<VW_DET_CRIME_FEM_CHILD>("VW_FORMAT_DISTRICT_DISPLAY_SRH", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<Status> AddFormatAsync(DET_CRIME_FEMALE_CHILDREN model)
        {
            

            var parameter = new
            {
                @DCFCID = model.DCFCID,
                @FMTID = model.FMTID,
                @yr = model.YEAR,
                @SECTORID = model.SECTORID,
                @DISTRICTID = model.DISTRICTID,
                @CBCID_NO = model.CBCID_NO,
                @APRADHNO = model.APRADHNO,
                @ACT = model.ACT,
                @THANAID = model.THANAID,
                @BNAME = model.BNAME,
                @COURTID = model.COURTID,
                @DAKILDATE = model.DAKILDATE,
                @ARROPVICHARANTHITHI = model.ARROPVICHARANTHITHI,
                @LETTERDATE = model.LETTERDATE,
                @LETTERNO = model.LETTERNO,
                @FATHERNAME = model.FATHERNAME,
                @ADDRESS = model.ADDRESS,
                @MOBLENO = model.MOBILENO,
                @PSHAKSHI_NAME = model.PSHAKSHI_NAME,
                @RSHAKSHI_NAME = model.RSHAKSHI_NAME,
                @ADATHAN_STATUS = model.ADATHAN_STATUS,
                @NEXT_HEARING_DATE = model.NEXT_HEARING_DATE,
                @ABHIYUKT = model.ABHIYUKT,
                @APRADHYEAR = model.APRADHYEAR,
                @CaseStatusId = model.CaseStatusId,
                @TransferFrm = model.TransferFrm,
                @CRNO = model.CNRNO,
                @GOVTOOFICER=model.GOVTOFFICER,
                @PLNO=model.PLNO,
                @SIBNO = model.SIBNO,
                @SIBNOYR = model.SIBNOYR,
                @CREATEDBY = model.CREATEDBY,
                @ADESHDATE=model.FRMDATE
            };

            using (var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryFirstAsync<Status>(
                            "USP_DET_CRIME_FEMALE_CHILDREN_INSERT",
                            parameter,
                            commandType: CommandType.StoredProcedure);
                    }

                }
                catch (SqlException sqlEx)
                {
                    connection.Dispose();
                    sqlEx.Message.ToString();
                    throw;
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

        public async Task<Status> AddFormatAsyncCIS(DET_CRIME_FEMALE_CHILDREN model)
        {
            

            var parameter = new
            {
                @DCFCID = model.DCFCID,
                @FMTID = model.FMTID,
                @yr = model.YEAR,
                @SECTORID = model.SECTORID,
                @DISTRICTID = model.DISTRICTID,
                @CBCID_NO = model.CBCID_NO,
                @APRADHNO = model.APRADHNO,
                @ACT = model.ACT,
                @THANAID = model.THANAID,
                @BNAME = model.BNAME,
                @COURTID = model.COURTID,
                @DAKILDATE = model.DAKILDATE,
                @ARROPVICHARANTHITHI = model.ARROPVICHARANTHITHI,
                @PSHAKSHI_NAME = model.PSHAKSHI_NAME,
                @RSHAKSHI_NAME = model.RSHAKSHI_NAME,
                @ADATHAN_STATUS = model.ADATHAN_STATUS,
                @NEXT_HEARING_DATE = model.NEXT_HEARING_DATE,
                @ABHIYUKT = model.ABHIYUKT,
                @APRADHYEAR = model.APRADHYEAR,
                @CaseStatusId = model.CaseStatusId,
                @TransferFrm = model.TransferFrm,
                @CRNO = model.CNRNO,
                @GOVTOOFICER = model.GOVTOFFICER,
                @PLNO = model.PLNO,
                @UPDATEDBY=model.UPDATEDBY
            };

            using (var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryFirstAsync<Status>(
                            "USP_DET_CRIME_FEMALE_CHILDREN_INSERT_CIS",
                            parameter,
                            commandType: CommandType.StoredProcedure);
                    }

                }
                catch (SqlException sqlEx)
                {
                    connection.Dispose();
                    sqlEx.Message.ToString();
                    throw;
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

        public Task<Status> UpdateFormatAsync(DET_CRIME_FEMALE_CHILDREN model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ADMIN_DASHBOARD_VIEW>> GetApplicationStatus()
        {

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<ADMIN_DASHBOARD_VIEW>("ADMIN_SECTOR_WISE_CNT", commandType: CommandType.StoredProcedure);
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


        public async Task<IEnumerable<ApplicationRecordCnt>> GetApplicationRecordCnt()
        {

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<ApplicationRecordCnt>("GETAPPLICATIONRECORD_CNT", commandType: CommandType.StoredProcedure);
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


        public async Task<IEnumerable<CASESTATUS_CNT_SECTOR_WISE>> GetAPPLICATION_CASE_STATUS()
        {

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<CASESTATUS_CNT_SECTOR_WISE>("USP_SECTOR_CASESTATUS_CNT", commandType: CommandType.StoredProcedure);
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


        public async Task<IEnumerable<Notification>> GetSectorWiseApplicationStatus(string strsecid)
        {
            var parameter = new
            {
                @SECTORID = strsecid,
                
            };

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<Notification>("SECTOR_WISE_CNT", parameter, commandType: CommandType.StoredProcedure);
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


        public async Task<IEnumerable<SECTOR_WISE_APPLICATION_RECORD_CNT>> GetSectorApplicationRecordCnt(string strsecid)
        {
            var parameter = new
            {
                @SECTORID = strsecid,

            };

            using (
                var connection = _dappercontext.CreateConnection())
                try
                {
                    {
                        return await connection.QueryAsync<SECTOR_WISE_APPLICATION_RECORD_CNT>("SECTORWISE_APPLICATIONRECORD_CNT", parameter, commandType: CommandType.StoredProcedure);
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


    }
}
