using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;

namespace CBCID_APPLICATION.Controllers
{
    public class INVESTOFFController : Controller
    {
        private readonly IInvestigation_officer _INGOFF;
        private readonly ApplicationDBContext _dbContext;
       // private readonly IDET_CRIME_FEMALE_CHILDREN _Fmt;
        string strdistrict = "";
        string strrole = "";
        String vsecid = "";
        public IActionResult Index()
        {
            return View();
        }
        public INVESTOFFController(IInvestigation_officer INGOFF, ApplicationDBContext context)
        {
            _INGOFF = INGOFF;
            _dbContext = context;
            //_Fmt = fmt;
        }
        [HttpGet]
        public async Task<IActionResult> VWINVEST(string DCFCID, string INVESTIGATION_OFFICER_ID)
        {
            string did = string.Empty;
            Int32 DCFID = 0, INFIF = 0;
            INVEST_OFFICER model = new INVEST_OFFICER();
            try
            {
                var strdistrict = HttpContext.Session.GetString("SDistrict");
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    did = strdistrict;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        DCFID = Convert.ToInt32(DCFCID);
                    }
                    if (!string.IsNullOrEmpty(INVESTIGATION_OFFICER_ID))
                    {
                        INFIF = Convert.ToInt32(INVESTIGATION_OFFICER_ID);
                    }
                    var VwLst = await _INGOFF.ViewInvestformat(DCFCID, INVESTIGATION_OFFICER_ID);
                    ViewBag.INVESTLst = VwLst;
                    model.DCFCID = DCFCID != null ? Convert.ToInt32(DCFCID) : 0;
                }
                else
                {
                    HttpContext.Session.Clear();
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Error: " + ex.Message;
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> INSERTUPDATEINVEST(string DCFCID, string sid)
        {
            INVEST_OFFICER model = new INVEST_OFFICER();
            ViewBag.INVESTLst = null;
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            ViewBag.msg = 0;
            try
            {
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        var id = DCFCID;
                        var VwLst = await _INGOFF.ViewInvestformat(DCFCID, sid);
                        ViewBag.INVESTLst = VwLst;
                        if ((!string.IsNullOrEmpty(DCFCID)) && (!string.IsNullOrEmpty(sid)) && sid != "0")
                        {
                            var VwLst1 = await _INGOFF.ViewInvestformat(DCFCID, sid);
                            ViewBag.INVESTLst = VwLst1;
                            var entity = VwLst1.FirstOrDefault();
                            if (entity != null)
                            {
                                model.INVESTIGATION_OFFICER_ID = entity.INVESTIGATION_OFFICER_ID;
                                model.DCFCID = entity.DCFCID;
                                model.DESIGNATIONID = entity.DESIGNATIONID;
                                model.OFFICER_NAME = entity.OFFICER_NAME;
                                model.PHONENO = entity.PHONENO;
                                model.FROMDATE = entity.FROMDATE;
                                model.TODATE = entity.TODATE;
                            }
                        }
                        else
                        {
                            int dfid = 0;
                            if (!string.IsNullOrEmpty(DCFCID))
                            {
                                dfid = Convert.ToInt32(DCFCID);
                            }
                            else
                            {
                                dfid = 0;
                            }
                            model.DCFCID = dfid;
                            model.INVESTIGATION_OFFICER_ID = 0;
                        }
                    }
                    else
                    {
                        HttpContext.Session.Clear();
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Error: " + ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> INSERTUPDATEINVEST(INVEST_OFFICER model)
        {
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");
            ViewBag.INVESTLst = null;
            ViewBag.msg = null;
            string DCFCID = string.Empty;
            string sid=string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    string str = string.Empty;
                    if (!String.IsNullOrEmpty(Request.Form["FROMDATE"]))
                    {
                        str = string.Empty;
                        str = Request.Form["FROMDATE"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.FROMDATE = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.FROMDATE = null;
                    }

                    if (!String.IsNullOrEmpty(Request.Form["TODATE"]))
                    {
                        str = string.Empty;
                        str = Request.Form["TODATE"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.TODATE = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.TODATE = null;
                    }

                    var stat = await _INGOFF.AddInvestOfficerAsync(model);
                    if (stat != null)
                    {
                        ViewBag.msg = stat.VMEM;
                    }
                    sid = "0";
                    DCFCID = Convert.ToString(model.DCFCID);
                    var VwLst = await _INGOFF.ViewInvestformat(DCFCID, sid);
                    ViewBag.INVESTLst = VwLst;
                }
                else
                {
                    HttpContext.Session.Clear();
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Error: " + ex.Message;
            }
            return View(model);
        }

        
    }
}
