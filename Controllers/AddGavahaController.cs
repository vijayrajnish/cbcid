using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography;

namespace CBCID_APPLICATION.Controllers
{
    public class AddGavahaController : Controller
    {
        private readonly IGAVAHA _GAV;
        private readonly ApplicationDBContext _dbContext;
        string strdistrict = "";
        string strrole = "";
        String vsecid = "";
        public AddGavahaController(IGAVAHA IG,ApplicationDBContext context)
        {
            _GAV = IG;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> VWGAVAHA(string DCFCID, string gavahaid)
        {
            string did = string.Empty;
            Int32 DCFID = 0, GVID = 0;
            Gavaha model = new Gavaha();
            try
            {
                var strdistrict = HttpContext.Session.GetString("SDistrict");
                var strrole = HttpContext.Session.GetString("SRole");
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    did = strdistrict;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        DCFID = Convert.ToInt32(DCFCID);
                        model.DCFCID = DCFID;
                    }
                    if (!string.IsNullOrEmpty(gavahaid))
                    {
                        GVID = Convert.ToInt32(gavahaid);
                    }
                    if (!string.IsNullOrEmpty(strrole))
                    {
                        model.createdby = strrole;
                    }
                    var VwLst = await _GAV.viewgavahaformat(DCFCID, gavahaid);

                    ViewBag.GavaLst = VwLst;
                    var entity = VwLst.FirstOrDefault();
                    if (entity != null)
                    {
                        model = new Gavaha
                        {
                            DCFCID = entity.DCFCID,
                        };
                    }
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
        public async Task<IActionResult> VWGAVAHACIS(string DCFCID, string gavahaid)
        {
            string did = string.Empty;
            Int32 DCFID = 0, GVID = 0;
            Gavaha model = new Gavaha();
            var strrole = HttpContext.Session.GetString("SRole");
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            if (!string.IsNullOrEmpty(strdistrict))
            {
                did = strdistrict;
                if (!string.IsNullOrEmpty(DCFCID))
                {
                    DCFID = Convert.ToInt32(DCFCID);
                }
                if (!string.IsNullOrEmpty(gavahaid))
                {
                    GVID = Convert.ToInt32(gavahaid);
                }
                var VwLst = await _GAV.viewgavahaformat(DCFCID, gavahaid);
                ViewBag.GavaLst = VwLst;
            }
            else
            {
                HttpContext.Session.Clear();
               await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> INSERTUPDATEGAVAHA(string DCFCID,string gavahaid,string ID)
        {
            Gavaha model = new Gavaha();
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");
            ViewBag.GavaLst = null;
            if (!string.IsNullOrEmpty(ID))
            {
                if (ID == "1")
                {
                    var stat = await _GAV.DeleteGavaha(DCFCID, gavahaid,"1");
                    if (stat != null)
                    {
                        ViewBag.msg = stat.VMEM;
                    }
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        model.DCFCID = Convert.ToInt32(DCFCID);
                        var VwLst = await _GAV.viewgavahaformat(DCFCID, gavahaid);
                        ViewBag.GavaLst = VwLst;

                    }
                    else
                    {
                        model.DCFCID = 0;
                    }
                    if (!string.IsNullOrEmpty(strrole))
                    {
                        model.createdby = strrole;
                    }
                    if ((!string.IsNullOrEmpty(DCFCID)) && (!string.IsNullOrEmpty(gavahaid)) && gavahaid != "0")
                    {
                        var VwLst = await _GAV.viewgavahaformat(DCFCID, gavahaid);
                        ViewBag.GavaLst = VwLst;
                        var entity = VwLst.FirstOrDefault();
                        if (entity != null)
                        {
                            model = new Gavaha
                            {
                                DCFCID = entity.DCFCID,
                                GAVAHA_KA_PRAKAR_ID = entity.GAVAHA_KA_PRAKAR_ID,
                                GavahaId = entity.GavahaId,
                                GavahaName = entity.GavahaName,
                                Address = entity.Address,
                                Phoneno = entity.Phoneno,
                                AdharNo = entity.AdharNo,
                                Examined = entity.Examined,
                                Examineddate = entity.Examineddate,
                                GhavahiNextdate = entity.GhavahiNextdate,
                                GavaTypeId = entity.GavaTypeId,
                                Remark = entity.Remark,
                                NonExaminedReasonId = entity.NonExaminedReasonId,
                                OtherRemark = entity.OtherRemark
                            };
                        }
                    }
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
        [HttpPost]
        public async Task<IActionResult> INSERTUPDATEGAVAHA(Gavaha model)
        {
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");
            string DCFCID = string.Empty;
            string gavahaid= string.Empty;
            ViewBag.GavaLst = null;
            try
            {
                if (!string.IsNullOrEmpty(strrole))
                {
                    model.createdby = strrole;
                }
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    string str = string.Empty;
                    if (!String.IsNullOrEmpty(Request.Form["Entrydate"]))
                    {
                        str = string.Empty;
                        str = Request.Form["Entrydate"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.Entrydate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.Entrydate = null;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["GhavahiNextdate"]))
                    {
                        str = string.Empty;
                        str = Request.Form["GhavahiNextdate"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.GhavahiNextdate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.GhavahiNextdate = null;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["Examineddate"]))
                    {
                        str = string.Empty;
                        str = Request.Form["Examineddate"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.Examineddate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.Examineddate = null;
                    }
                    var stat = await _GAV.AddGavahaAsync(model);
                    if (stat != null)
                    {
                        ViewBag.msg = stat.VMEM;
                        model.GavahaId = Convert.ToInt32(stat.DMEM);
                    }

                    //add new code
                    gavahaid = "0";
                    DCFCID = Convert.ToString(model.DCFCID);
                    var VwLst = await _GAV.viewgavahaformat(model.DCFCID.ToString(), gavahaid);
                    ViewBag.GavaLst = VwLst;
                    //
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
        public async Task<IActionResult> INSERTUPDATEGAVAHACIS(string DCFCID, string gavahaid)
        {
            Gavaha model = new Gavaha();
            var strdistrict = HttpContext.Session.GetString("SDistrict");

            if (!string.IsNullOrEmpty(strdistrict))
            {
                ViewBag.msg = 0;
                if (!string.IsNullOrEmpty(DCFCID))
                {
                    model.DCFCID = Convert.ToInt32(DCFCID);

                }
                else
                {
                    model.DCFCID = 0;
                }
                if ((!string.IsNullOrEmpty(DCFCID)) && (!string.IsNullOrEmpty(gavahaid)))
                {
                    var VwLst = await _GAV.viewgavahaformat(DCFCID, gavahaid);
                    var entity = VwLst.FirstOrDefault();
                    if (entity != null)
                    {
                        model = new Gavaha
                        {
                            DCFCID = entity.DCFCID,
                            GavahaId = entity.GavahaId,
                            GavahaName = entity.GavahaName,
                            Address = entity.Address,
                            Phoneno = entity.Phoneno,
                            AdharNo = entity.AdharNo,
                            Examined = entity.Examined,
                            Examineddate = entity.Examineddate,
                            GhavahiNextdate = entity.GhavahiNextdate,
                            GavaTypeId = entity.GavaTypeId,
                            Remark = entity.Remark
                        };
                    }
                }
            }
            else
            {
                HttpContext.Session.Clear();
               await  HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> INSERTUPDATEGAVAHACIS(Gavaha model)
        {
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");

            if (!string.IsNullOrEmpty(strdistrict))
            {
                ViewBag.msg = 0;
                string str = string.Empty;
                if (model.Entrydate != null)
                {
                    str = string.Empty;
                    DateTime dd = new DateTime();
                    str = Convert.ToDateTime(model.Entrydate).ToString("dd-MM-yyyy");
                    string[] ps = str.Split('-');
                    str = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.Entrydate = Convert.ToDateTime(str);
                }
                else
                {
                    model.Entrydate = null;
                }
                if (model.GhavahiNextdate != null)
                {
                    str = string.Empty;
                    DateTime dd = new DateTime();
                    str = Convert.ToDateTime(model.GhavahiNextdate).ToString("dd-MM-yyyy");
                    string[] ps = str.Split('-');
                    str = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.GhavahiNextdate = Convert.ToDateTime(str);
                }
                else
                {
                    model.GhavahiNextdate = null;
                }
                if (model.Examineddate != null)
                {
                    str = string.Empty;
                    DateTime dd = new DateTime();
                    str = Convert.ToDateTime(model.Examineddate).ToString("dd-MM-yyyy");
                    string[] ps = str.Split('-');
                    str = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.Examineddate = Convert.ToDateTime(str);
                }
                else
                {
                    model.Examineddate = null;
                }

                var stat = await _GAV.AddGavahaAsync(model);
                if (stat != null)
                {
                    ViewBag.msg = stat.VMEM;
                    model.GavahaId = Convert.ToInt32(stat.DMEM);
                }
            }
            else
            {
                HttpContext.Session.Clear();
               await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }

            //return RedirectToAction("VWGAVAHACIS", "AddGavaha", new { DCFCID = model.DCFCID });
            //Formatone / VWFmt
            //return RedirectToAction("VWFmt","Formatone");
            return View(model);

        }
    }
}
