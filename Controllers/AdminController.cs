using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace CBCID_APPLICATION.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDET_CRIME_FEMALE_CHILDREN _Fmt;
        private readonly IAddSunvahi _sun;
        private readonly ApplicationDBContext _dbContext;
        private string strdistrict = string.Empty;
        private string strsectorid = string.Empty;
        private string strroleid = string.Empty;
        public AdminController(IDET_CRIME_FEMALE_CHILDREN Fmt, ApplicationDBContext dBContext, IAddSunvahi sun)
        {
            _Fmt = Fmt;
            _sun = sun;
            _dbContext = dBContext;
        }

        private async Task<IActionResult> SessionExpired()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> VWAdmin(string SECTORID, string DISTRICTID, string SCBCID, string FMTID, string CaseStatusId, string PSAVE, string CREATEDBY,string YEAR,string APRADHYEAR)
        {

            DET_CRIME_FEMALE_CHILDREN model = new DET_CRIME_FEMALE_CHILDREN();
            try
            {
                var strdistrict = HttpContext.Session.GetString("SDistrict");
                var strsectorid = HttpContext.Session.GetString("SID");
                String FID = string.Empty;
                String Appstatus = string.Empty;
                if (!string.IsNullOrEmpty(PSAVE))
                {
                    if (PSAVE == "TOT")
                    {
                        Appstatus = "0"; // Total
                        model.PSAVE = 0;
                        model.CREATEDBY = "0";
                    }
                    if (PSAVE == "2")
                    {
                        Appstatus = "2"; // Total
                        model.PSAVE = 2;
                    }
                    if (PSAVE == "1")
                    {
                        Appstatus = "1"; // Total
                        model.PSAVE = 1;
                    }

                }
                else
                {
                    Appstatus = "0"; // Total
                    model.PSAVE = 0;
                    model.CREATEDBY = "0";
                }

                if (!string.IsNullOrEmpty(CREATEDBY))
                {
                    model.CREATEDBY = CREATEDBY;
                }
                else
                {
                    model.CREATEDBY = "0";
                }

                if (!string.IsNullOrEmpty(FMTID))
                {
                    FID = FMTID;
                }
                else
                {
                    FID = "0";
                }

                if (!string.IsNullOrEmpty(strsectorid) && !string.IsNullOrEmpty(strdistrict))
                {
                    var dist = await _Fmt.GetDistrictLst(strdistrict);
                    ViewBag.DistrictId = new SelectList(dist, "DistrictId", "DistrictName", strdistrict);
                    var SEC = await _Fmt.GetSectorLst(strsectorid);
                    ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME", strsectorid);
                    if (strsectorid == "9")
                    {
                        model.CREATEDBY = "3";
                    }
                    else
                    {
                        model.CREATEDBY = "2";
                    }
                }
                else
                {
                    await SessionExpired();
                }


                if (!string.IsNullOrEmpty(FID))
                {
                    var FMT = await _Fmt.GetFMTLst(FID);
                    ViewBag.FMTID = new SelectList(FMT, "FMTID", "FNAME");
                }

                if (!string.IsNullOrEmpty(DISTRICTID))
                {
                    strdistrict = DISTRICTID;
                    var dist = await _Fmt.GetDistrictLst(strdistrict);
                    ViewBag.DistrictId = new SelectList(dist, "DistrictId", "DistrictName", strdistrict);
                }
                if (!string.IsNullOrEmpty(SECTORID))
                {
                    strsectorid = SECTORID;
                }

                var VwLst = await _Fmt.viewformatAdminSrh(strsectorid, strdistrict, SCBCID, FMTID, CaseStatusId, Appstatus, CREATEDBY,YEAR,APRADHYEAR);
                ViewBag.FmtLst = VwLst;
                var CSTAT = await _sun.GetStatusLst();
                ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
                var YR = await _Fmt.GetYRLst();
                ViewBag.YEAR = new SelectList(YR, "YrId", "Year", model.YEAR);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewAdminDashBoard()
        {
            ApplicationRecordCnt model = new ApplicationRecordCnt();
            try
            {
                var VwLst = await _Fmt.GetApplicationStatus();
                ViewBag.AdminDashBoardLst = VwLst;
                var st = await _Fmt.GetApplicationRecordCnt();
                var entity = st.FirstOrDefault();
                if (entity != null)
                {
                    model = new ApplicationRecordCnt
                    {
                        totapplicationcnt = entity.totapplicationcnt,
                        totfinalsave = entity.totfinalsave,
                        totpartialsave = entity.totpartialsave,
                    };

                }
                var CSTATLST = await _Fmt.GetAPPLICATION_CASE_STATUS();
                ViewBag.casesstatuslst = CSTATLST;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ViewAdminDashBoard(string PSAVE)
        {
            ApplicationRecordCnt model = new ApplicationRecordCnt();
            try
            {
                if (!string.IsNullOrEmpty(PSAVE))
                {
                    if (PSAVE == "Total")
                    {
                        return RedirectToAction("VWAdmin", "Admin", new { PSAVE = "0" });
                    }
                    if (PSAVE == "Partial")
                    {
                        return RedirectToAction("VWAdmin", "Admin", new { PSAVE = "2" });
                    }
                    if (PSAVE == "Final")
                    {
                        return RedirectToAction("VWAdmin", "Admin", new { PSAVE = "1" });
                    }

                }
                var VwLst = await _Fmt.GetApplicationStatus();
                ViewBag.AdminDashBoardLst = VwLst;
                var st = await _Fmt.GetApplicationRecordCnt();
                var entity = st.FirstOrDefault();
                if (entity != null)
                {
                    model = new ApplicationRecordCnt
                    {
                        totapplicationcnt = entity.totapplicationcnt,
                        totfinalsave = entity.totfinalsave,
                        totpartialsave = entity.totpartialsave,
                    };
                }
                var CSTATLST = await _Fmt.GetAPPLICATION_CASE_STATUS();
                ViewBag.casesstatuslst = CSTATLST;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> VWAdminCIS(string SECTORID, string DISTRICTID, string SCBCID, string FMTID)
        {
            DET_CRIME_FEMALE_CHILDREN model = new DET_CRIME_FEMALE_CHILDREN();
            //strdistrict = HttpContext.Session.GetString("SDistrict");
            //strsectorid = HttpContext.Session.GetString("SID");
            strdistrict = "0";
            strsectorid = "0";
            if (!string.IsNullOrEmpty(strsectorid))
            {
                var dist = await _Fmt.GetDistrictLst(strdistrict);
                ViewBag.DistrictId = new SelectList(dist, "DistrictId", "DistrictName");
                var SEC = await _Fmt.GetSectorLst(strsectorid);
                ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME");
            }
            if (!string.IsNullOrEmpty(FMTID))
            {
                var FMT = await _Fmt.GetFMTLst(FMTID);
                ViewBag.FMTID = new SelectList(FMT, "FMTID", "FNAME");
            }
            var VwLst = await _Fmt.viewformatAdminSrhAdmin(strsectorid, strdistrict, SCBCID, FMTID);
            ViewBag.FmtLst = VwLst;
            return View(model);
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
