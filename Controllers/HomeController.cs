using System.Diagnostics;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.IMPLEMENTATION;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBCID_APPLICATION.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILGVIEW _LG;
        private const string strdistrict = "_District";
        private const string strrole = "_Role";
        
        public HomeController(ILogger<HomeController> logger, ILGVIEW _LGSERVICE)
        {
            _logger = logger;
            _LG = _LGSERVICE;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Msg = 0;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Utility_Users model)
        {
            string Uname = string.Empty;
            string Pass = string.Empty;
            string Uid = string.Empty;
            string Did = string.Empty;
            string eid = string.Empty;
            string roleid = string.Empty;
            string Secid = string.Empty;
            Uname = model.UserName;
            Pass = model.Password;
            try
            {
                var stat = await _LG.GetLgDetails(model.UserName, model.Password);
                if (stat != null)
                {
                    Uid = stat.UserId;
                    Did = stat.districtid;
                    eid = stat.EmployeeId;
                    roleid = stat.Roleid;
                    Secid = stat.SectorId;
                }
                if (Uid != string.Empty && roleid != string.Empty)
                {
                    HttpContext.Session.SetString("SDistrict", Did);
                    HttpContext.Session.SetString("SRole", roleid);
                    HttpContext.Session.SetString("Userid", eid);
                    HttpContext.Session.SetString("SID", Secid);
                    HttpContext.Session.SetString("UNAME", model.UserName);
                    HttpContext.Session.SetString("NotificationShown", "false");

                    if (roleid == "1")// 1 stand for admin user
                    {
                        //Commented on 16feb 2025
                        //return RedirectToAction("Index", "DashBoard", null);
                        // return RedirectToAction("VWadmin", "DashBoard", null);
                        //return RedirectToAction("VWAdmin", "Admin", null);
                        return RedirectToAction("ViewAdminDashBoard", "Admin", null);
                    }
                    if (roleid == "2")//2 stand for district user
                    {
                        //var secdetails = _Fmt.GetSectorLst(strsecid);
                        //Commented on 16feb 2025
                        //return RedirectToAction("VWadmin", "DashBoard", null);
                        // ViewBag.LgUser = model.UserName;
                        return RedirectToAction("Index", "DashBoard", null);
                    }
                    if (roleid == "3")//2 stand for district user
                    {
                        //return RedirectToAction("VCUSER", "DashBoard", null);
                        return RedirectToAction("Index", "DashBoard", null);
                    }
                }
                else
                {
                    ViewBag.Msg = 1;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                ViewBag.Msg = 1;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
