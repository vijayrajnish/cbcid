using CBCID_APPLICATION.Context;
using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Data;


namespace CBCID_APPLICATION.Controllers
{
    public class CommonController : Controller
    {
      
        private readonly IDET_CRIME_FEMALE_CHILDREN _Fmt;
        private readonly DapperContext _dappercontext;
        public CommonController(DapperContext dapperContext, IDET_CRIME_FEMALE_CHILDREN fmt)
        {
           
            _dappercontext = dapperContext;
            _Fmt = fmt;

        }
        public IActionResult Index()
        {
            return View();
        }

        public List<MDistrict> FetchDistrict(string DistrictId)
        {

            List<MDistrict> _MDist;
            var parameter = new
            {
                @distid = DistrictId,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _MDist = connection.Query<MDistrict>(
                    "GETDISTRICTBYID", parameter,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                connection.Close();

            }

            return _MDist;
        }

        public List<MDistrict> FetchAdminDistrict(string DistrictId)
        {

            List<MDistrict> _MDist;
            var parameter = new
            {
                @distid = DistrictId,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _MDist = connection.Query<MDistrict>(
                    "GETDISTRICTBYIDADMIN", parameter,
                    commandType: CommandType.StoredProcedure,commandTimeout:180
                ).ToList();
                connection.Close();

            }

            return _MDist;
        }

        public List<MThana> FetchThana(string DistrictId)
        {
            List<MThana> _MThana = new List<MThana>();
            
            var parameter = new
            {
                @distid = DistrictId,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                try
                {
                    connection.Open();
                    _MThana = connection.Query<MThana>(
                        "FetchThanaById", parameter,
                        commandType: CommandType.StoredProcedure
                    ).ToList();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return _MThana;
        }

        public IActionResult FetchFormatone(string encid)
        {
            VW_DET_CRIME_FEM_CHILD model = new VW_DET_CRIME_FEM_CHILD();
            List<VW_DET_CRIME_FEM_CHILD> _MDist;
            List<Gavaha> _GVLST;
            List<CaseHearing> _SUNVAHI;


            var parameter = new
            {
                @DCFCID = encid,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _MDist = connection.Query<VW_DET_CRIME_FEM_CHILD>(
                    "VIEW_FORMAT_DISPLAY", parameter,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                var entity = _MDist.FirstOrDefault();
                if (entity != null)
                {
                    model.DCFCID = entity.DCFCID;
                    model.THANA = entity.THANA;
                    model.Sname = entity.Sname;
                    model.dname = entity.dname;
                    model.COURT = entity.COURT;
                    model.cbcidno = entity.cbcidno;
                    model.APRADHNO = entity.APRADHNO;
                    model.ACT = entity.ACT;
                    model.BNAME = entity.BNAME;
                    model.DAKILDATE = entity.DAKILDATE;
                    model.PSHAKSHI_NAME = entity.PSHAKSHI_NAME;
                    model.RSHAKSHI_NAME = entity.RSHAKSHI_NAME;
                    model.ADATHAN_STATUS = entity.ADATHAN_STATUS;
                    model.ARROPVICHARANTHITHI = entity.ARROPVICHARANTHITHI;
                }
            }

            // Fetch Gavaha List
            var parameter1 = new
            {
                @DCFCID = encid,
                @GAVAHAID = 0,
            };
            using (
               var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _GVLST = connection.Query<Gavaha>(
                    "VW_GAVAHA_LST", parameter1,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                connection.Close();

            }
            if (_GVLST != null)
            {
                ViewBag.GLst = _GVLST;
            }
            else
            {
                ViewBag.GLst = null;
            }

            ////  Fetch Gavaha List

            var parameter2 = new
            {
                @Hearingid = 0,
                @DCFCID = encid,
            };

            using (
               var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _SUNVAHI = connection.Query<CaseHearing>(
                    "VW_SUNVAHI_DET", parameter2,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                connection.Close();

            }

            if (_SUNVAHI != null)
            {
                ViewBag.SunvahiLst = _SUNVAHI;
            }
            else
            {
                ViewBag.SunvahiLst = null;
            }

            return PartialView("_FormatVW", model);
        }

        [HttpPost]
        public List<Status> DeleteAbhiyukth(string DCFCID, string ABHIID, string ID)
        {

            List<Status> _MDist;
            var parameter = new
            {
                @DCFCID = DCFCID,
                ABHIID = ABHIID,
                ID = ID,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _MDist = connection.Query<Status>(
                    "DELETE_SAHAYAK", parameter,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                connection.Close();

            }

            return _MDist;
        }

        [HttpPost]
        public List<Status> DeleteGavaha(string DCFCID, string ABHIID, string ID)
        {

            List<Status> _MDist;
            var parameter = new
            {
                @DCFCID = DCFCID,
                ABHIID = ABHIID,
                ID = ID,
            };
            using (
                var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _MDist = connection.Query<Status>(
                    "DELETE_SAHAYAK", parameter,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                connection.Close();

            }

            return _MDist;
        }

        public IActionResult FetchNotification(string Secid, string Roleid)
        {
            SectorWiseNotification model = new SectorWiseNotification();
            List<SectorWiseNotification> _SLST;
            var parameter = new
            {
                @Secid = Secid,
                @Roleid = Roleid,
            };
            using (
              var connection = _dappercontext.CreateConnection())
            {
                connection.Open();
                _SLST = connection.Query<SectorWiseNotification>(
                    "VIEW_FORMAT_SECTOR_DISPLAY", parameter,
                    commandType: CommandType.StoredProcedure
                ).ToList();
                connection.Close();

            }
            if (_SLST != null)
            {
                ViewBag.SLst = _SLST;
            }
            else
            {
                ViewBag.SLst = null;
            }
            return PartialView("_SectorVWSector", model);
        }

        public IActionResult InvestigationOfficer(string investid)
        {
            INVEST_OFFICER model = new INVEST_OFFICER();
            model.DCFCID = Convert.ToInt32(investid);
            return PartialView("_INVEST", model);
        }
    }
}
