using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace CBCID_APPLICATION.Controllers
{
    public class DashBoardController : Controller
    {
        //string strdistrict = "";
        string strrole = "";
        private string strdistrict = string.Empty;
        private string strsectorid = string.Empty;
        private readonly IDET_CRIME_FEMALE_CHILDREN _Fmt;
        
        public DashBoardController(IDET_CRIME_FEMALE_CHILDREN FMT)
        {
            _Fmt = FMT;
        }
        public async Task<IActionResult> Index()
        {
            var strrole = HttpContext.Session.GetString("SRole");
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strsecid = HttpContext.Session.GetString("SID");
            var USName = HttpContext.Session.GetString("UNAME");
            ApplicationRecordCnt model = new ApplicationRecordCnt();
            try
            {
                if (!string.IsNullOrEmpty(strsecid))
                {
                    var isNotificationShown = HttpContext.Session.GetString("NotificationShown");

                    if (string.IsNullOrEmpty(isNotificationShown) || isNotificationShown == "false")
                    {
                        ViewBag.ShowNotification = true;
                        HttpContext.Session.SetString("NotificationShown", "true"); // Set as shown
                    }
                    else
                    {
                        ViewBag.ShowNotification = false;
                    }
                    var VwLst = await _Fmt.GetSectorWiseApplicationStatus(strsecid);
                    ViewBag.AdminDashBoardLst = VwLst;
                    var st = await _Fmt.GetSectorApplicationRecordCnt(strsecid);
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
                }
                if (!(string.IsNullOrEmpty(strrole)) && !(string.IsNullOrEmpty(strsecid)))
                {
                    model.SecId = Convert.ToInt32(strsecid);
                    model.Roleid = Convert.ToInt32(strrole);
                }
                if (string.IsNullOrEmpty(USName))
                {
                    ViewBag.UsLstName = USName;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public IActionResult VCUSER()
        {
            var strrole = HttpContext.Session.GetString("SRole");
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            return View();
        }
        public async Task<IActionResult> VWadmin()
        {
            MSector model = new MSector();
            try
            {
                var strrole = HttpContext.Session.GetString("SRole");
                if (strrole == "1")
                {
                    var SEC = await _Fmt.GetSectorLst(null);
                    ViewBag.SecLst = SEC;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public async Task<IActionResult> VWAdminSectorDetails(string encid)
        {
            MDistrict model = new MDistrict();
            string secid = string.Empty;
            var role = HttpContext.Session.GetString("SRole");

            if (role == "1")
            {
                if (!string.IsNullOrEmpty(encid))
                {

                    HttpContext.Session.SetString("SID", encid);
                    var dist = await _Fmt.GetDistrictSectorwiseLst(encid);
                    ViewBag.DistLst = dist;
                }
            }
            return View(model);
        }
        public async Task<IActionResult> VWAdminSectorDetailsCIS(string encid)
        {
            MDistrict model = new MDistrict();
            string secid = string.Empty;
            var role = HttpContext.Session.GetString("SRole");

            if (role == "1")
            {
                //if (!string.IsNullOrEmpty(encid))
                //{

                   // HttpContext.Session.SetString("SID", encid);
                    var dist = await _Fmt.GetDistrictSectorwiseLst(encid);
                    ViewBag.DistLst = dist;
                //}
            }
            return View(model);
        }
        public async Task<IActionResult> Getformatdetails(string encid)
        {
            string strdid = string.Empty;
            AdminDashBoardView model = new AdminDashBoardView();
            var strscid = HttpContext.Session.GetString("SID");
            HttpContext.Session.SetString("SDistrict", encid);
            var FMTLST = await _Fmt.GetFormatLst(encid, strscid);
            if (!string.IsNullOrEmpty(strscid))
            {
                model.Distid = Convert.ToInt32(strscid);
            }
            else
            {
                model.Distid = 0;
            }
            ViewBag.FORMATLST = FMTLST;
            return View(model);
        }

        public async Task<IActionResult> GetCISformatdetails(string encid)
        {
            string strscid = string.Empty;
            string strdid = string.Empty;
            AdminDashBoardView model = new AdminDashBoardView();
            strscid = "4";
            var FMTLST = await _Fmt.GetFormatLstCIS(encid, strscid);
            if (!string.IsNullOrEmpty(strscid))
            {
                model.Distid = Convert.ToInt32(strscid);
            }
            else
            {
                model.Distid = 0;
            }
            ViewBag.FORMATLST = FMTLST;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VWAdminReport(string SECTORID, string DISTRICTID, string SCBCID, string FMTID)
        {
            DET_CRIME_FEMALE_CHILDREN model = new DET_CRIME_FEMALE_CHILDREN();
           //var strdistrict = HttpContext.Session.GetString("SDistrict");
           //var  strsectorid = HttpContext.Session.GetString("SID");
           // string CaseStatusId = string.Empty;
           //     var dist = await _Fmt.GetDistrictLst(null);
           //     ViewBag.DistrictId = new SelectList(dist, "DistrictId", "DistrictName", strdistrict);
           //     var SEC = await _Fmt.GetSectorLst(null);
           //     ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME", strsectorid);
           //     var FMT = await _Fmt.GetALLFMTLst();
           //     ViewBag.FMTID = new SelectList(FMT, "FMTID", "FNAME");
           // var VwLst = await _Fmt.viewformatAdminSrh(SECTORID, DISTRICTID, SCBCID, FMTID, CaseStatusId);
           // ViewBag.FmtLst = VwLst;
            return View(model);
        }
    }
}
