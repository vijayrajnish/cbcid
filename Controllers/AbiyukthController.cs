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
using CBCID_APPLICATION.Context;
using System.Data;

namespace CBCID_APPLICATION.Controllers
{
    public class AbiyukthController : Controller
    {
        private readonly ISAHAYA_ABIYUKT _SAV;
        private readonly ApplicationDBContext _dbContext;
        string strdistrict = "";
        string strrole = "";
        String vsecid = "";
        public AbiyukthController(ISAHAYA_ABIYUKT IG, ApplicationDBContext context)
        {
            _SAV = IG;
            _dbContext = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> VWsahaabiyuk(string DCFCID, string sid)
        {
            string did = string.Empty;
            Int32 DCFID = 0, GVID = 0;
            SAHAYA model = new SAHAYA();

            var strdistrict = HttpContext.Session.GetString("SDistrict");
            if (!string.IsNullOrEmpty(strdistrict))
            {
                did = strdistrict;
                if (!string.IsNullOrEmpty(DCFCID))
                {
                    DCFID = Convert.ToInt32(DCFCID);
                }
                if (!string.IsNullOrEmpty(sid))
                {
                    GVID = Convert.ToInt32(sid);
                }
                var VwLst = await _SAV.Viewsahayakformat(DCFCID, sid);
                ViewBag.sahaLst = VwLst;
                model.DCFCID = DCFID;
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
        public async Task<IActionResult> INSERTUPDATESAHAABIYUKTH(string DCFCID, string sid,string ID)
        {
            SAHAYA model = new SAHAYA();
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            ViewBag.sahaLst = null;
            if (!string.IsNullOrEmpty(ID))
            {
                if(ID=="1")
                {
                    var stat = await _SAV.DeletesahayakAsync(DCFCID, sid);
                    if (stat != null)
                    {
                        ViewBag.msg = stat.VMEM;
                    }
                }
            }
            if (!string.IsNullOrEmpty(strdistrict))
            {
                
                    ViewBag.msg = 0;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        model.DCFCID = Convert.ToInt32(DCFCID);
                        var VwLst = await _SAV.Viewsahayakformat(DCFCID, sid);
                        ViewBag.sahaLst = VwLst;
                    }
                    else
                    {
                        model.DCFCID = 0;
                    }
                    if ((!string.IsNullOrEmpty(DCFCID)) && (!string.IsNullOrEmpty(sid)) && sid != "0")
                    {
                        var VwLst = await _SAV.Viewsahayakformat(DCFCID, sid);
                        ViewBag.sahaLst = VwLst;
                        var entity = VwLst.FirstOrDefault();
                        if ((entity != null))
                        {
                            model.DCFCID = entity.DCFCID;
                            model.Sahayak_Abhiyukt_ID = entity.Sahayak_Abhiyukt_ID;
                            model.Name = entity.Name;
                            model.Address = entity.Address;
                            model.Phoneno = entity.Phoneno;
                            model.AdharNo = entity.AdharNo;
                            model.Remark = entity.Remark;
                            model.Father = entity.Father;
                            model.AbhiyuktStatus = entity.AbhiyuktStatus;
                        }
                    }
                
                
            }
            else
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> INSERTUPDATESAHAABIYUKTH(SAHAYA model)
        {
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");
            ViewBag.sahaLst = null;
            string DCFCID = string.Empty; 
            string sid=string.Empty;
            if (!string.IsNullOrEmpty(strdistrict))
            {
                ViewBag.msg = 0;
                string str = string.Empty;
                if (model.Createdon != null)
                {
                    str = string.Empty;
                    DateTime dd = new DateTime();
                    str = Convert.ToDateTime(model.Createdon).ToString("dd-MM-yyyy");
                    string[] ps = str.Split('-');
                    str = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.Createdon = Convert.ToDateTime(str);
                }
                else
                {
                    model.Createdon = null;
                }
                
                    var stat = await _SAV.AddsahayakAsync(model);
                    if (stat != null)
                    {
                        ViewBag.msg = stat.VMEM;
                        //model.Sahayak_Abhiyukt_ID = Convert.ToInt32(stat.DMEM);
                    }
                
                
                sid = "0";
                DCFCID = Convert.ToString(model.DCFCID);
                var VwLst = await _SAV.Viewsahayakformat(DCFCID, sid);
                ViewBag.sahaLst = VwLst;
            }
            else
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            //return RedirectToAction("FmtInsertUpdate", "Formatone", new { DCFCID = model.DCFCID });
            return View(model);


        }


        [HttpPost]
        public async Task<IActionResult> DeleteAbhiyukth(string DCFCID, string ABHIID,string ID)
        {
            SAHAYA model = new SAHAYA();
            var stat = await _SAV.DeletesahayakAsync(DCFCID, ABHIID);
            if (stat != null)
            {
                ViewBag.msg = stat.VMEM;
                //model.Sahayak_Abhiyukt_ID = Convert.ToInt32(stat.DMEM);
            }
            return View(model);
        }



    }
}
