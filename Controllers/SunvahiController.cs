using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using static Dapper.SqlMapper;

namespace CBCID_APPLICATION.Controllers
{
    public class SunvahiController : Controller
    {
        private readonly IAddSunvahi _sun;
        private readonly ApplicationDBContext _dbContext;
        string strdistrict = "";
        string strrole = "";
        String vsecid = "";
        public SunvahiController(IAddSunvahi sun, ApplicationDBContext context)
        {
            _sun = sun;
            _dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> VWSUNVAHI(string DCFCID, string Hearingid)
        {
            string did = string.Empty;
            Int32 DCFID = 0, SUNID = 0;
            CaseHearing model = new CaseHearing();
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
                    if (!string.IsNullOrEmpty(Hearingid))
                    {
                        SUNID = Convert.ToInt32(Hearingid);
                    }
                    var VwLst = await _sun.viewSunvahiformat(DCFCID, Hearingid);
                    ViewBag.SunvahiLst = VwLst;
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
                // Log the exception (ex) here if needed
                ViewBag.ErrorMessage = "An error occurred while fetching data. Please try again later.";
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VWSUNVAHICIS(string DCFCID, string Hearingid)
        {
            string did = string.Empty;
            Int32 DCFID = 0, SUNID = 0;
            CaseHearing model = new CaseHearing();

            var strdistrict = HttpContext.Session.GetString("SDistrict");
            if (!string.IsNullOrEmpty(strdistrict))
            {
                did = strdistrict;
                if (!string.IsNullOrEmpty(DCFCID))
                {
                    DCFID = Convert.ToInt32(DCFCID);
                }
                if (!string.IsNullOrEmpty(Hearingid))
                {
                    SUNID = Convert.ToInt32(Hearingid);
                }
                var VwLst = await _sun.viewSunvahiformat(DCFCID, Hearingid);
                ViewBag.SunvahiLst = VwLst;
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
        public async Task<IActionResult> INSERTUPDATESUNVAHI(string DCFCID, string Hearingid)
        {
            CaseHearing model = new CaseHearing();
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            ViewBag.msg = 0;
            try
            {
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        model.DCFCID = Convert.ToInt32(DCFCID);
                        model.HearingID = 0;
                        var st = await _sun.Getdet(DCFCID);
                        if (st == null || !st.Any())
                        {
                            var SUNDET = await _sun.Getsunvahidet(DCFCID);
                            var Sunentity = SUNDET.FirstOrDefault();
                            if (Sunentity != null)
                            {
                                SunvahiMandatoryDetails model1 = new SunvahiMandatoryDetails
                                {
                                    CBCID_NO = Sunentity.CBCID_NO,
                                    Bname = Sunentity.Bname,
                                    appradhno = Sunentity.appradhno,
                                    CaseStatusId = Sunentity.CaseStatusId
                                };
                                model.CBCID = model1.CBCID_NO;
                                model.Bname = model1.Bname;
                                model.APRADHNO = model1.appradhno;
                                model.CaseStatusId = model1.CaseStatusId;
                            }

                        }
                        var VwLst = await _sun.viewSunvahiformat(DCFCID, Hearingid);
                        ViewBag.SunvahiLst = VwLst;
                        var entity = st.FirstOrDefault();
                        if (entity != null)
                        {
                            model = new CaseHearing
                            {


                                APRADHNO = entity.Apradhno,
                                CBCID = entity.cbcidno,
                                Bname = entity.Bname,
                                HearingDate = entity.HearingDate,
                                NextHearingDate = entity.NextHearingDate,
                                CaseStatusId = entity.CaseStatusId,
                                Prosecutor = entity.Prosecutor,
                                Mobile = entity.Mobile,
                                DCFCID = entity.DCFCID,
                                ABHIYUKTHKAIVIRUDHMSG_ID = entity.ABHIYUKTHKAIVIRUDHMSG_ID,
                                Sahayak_Abhiyukt_ID = entity.Sahayak_Abhiyukt_ID,
                                CASERMK = entity.CASERMK,
                                FDATE = entity.FDATE,

                            };

                        }
                    }
                    else
                    {
                        model.DCFCID = 0;
                        model.HearingID = 0;
                    }
                    if ((!string.IsNullOrEmpty(DCFCID)) && (!string.IsNullOrEmpty(Hearingid)) && Hearingid != "0")
                    {
                        var VwLst = await _sun.viewSunvahiformat(DCFCID, Hearingid);
                        ViewBag.SunvahiLst = VwLst;
                        var entity = VwLst.FirstOrDefault();
                        if (entity != null)
                        {
                            model = new CaseHearing
                            {

                                APRADHNO = entity.APRADHNO,
                                CBCID = entity.CBCID,
                                Bname = entity.Bname,
                                HearingDate = entity.HearingDate,
                                NextHearingDate = entity.NextHearingDate,
                                CaseStatusId = entity.CaseStatusId,
                                Prosecutor = entity.Prosecutor,
                                Mobile = entity.Mobile,
                                HearingID = entity.HearingID,
                                Remark = entity.Remark,
                                ADATHAN_STATUS = entity.ADATHAN_STATUS,
                                DCFCID = entity.DCFCID,
                                ABHIYUKTHKAIVIRUDHMSG_ID = entity.ABHIYUKTHKAIVIRUDHMSG_ID,
                                Sahayak_Abhiyukt_ID = entity.Sahayak_Abhiyukt_ID,
                                CASERMK = entity.CASERMK,
                                FDATE = entity.FDATE
                            };
                        }
                    }
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        var abhiLst = await _sun.LstAbhiyukthAsync(DCFCID);
                        if (abhiLst != null)
                        {
                            ViewBag.SunvahiVIEW = abhiLst;
                        }
                        else
                        {
                            ViewBag.SunvahiVIEW = null;
                        }
                    }

                }
                else
                {
                    HttpContext.Session.Clear();
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Home");
                }
                var CSTAT = await _sun.GetStatusLst();
                ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
                var ABHIYUKTH = await _sun.GetABHIYUKTHLst(DCFCID);
                ViewBag.ABHIYUKTHDRP = new SelectList(ABHIYUKTH, "Sahayak_Abhiyukt_ID", "ABHIYUKTHNAME", model.Sahayak_Abhiyukt_ID);
                var ABMSG = await _sun.Getabhiyukthmsg();
                ViewBag.ABHIYUKTMSG = new SelectList(ABMSG, "ABHIYUKTHKAIVIRUDHMSG_ID", "MESSAGE", model.ABHIYUKTHKAIVIRUDHMSG_ID);

            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                ViewBag.ErrorMessage = "An error occurred while fetching data. Please try again later.";
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> INSERTUPDATESUNVAHI(CaseHearing model)
        {
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");
            string dfid = string.Empty;
            string fdate = string.Empty;
            int did = 0;
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["FRMDATE"]))
                {
                    fdate = string.Empty;
                    fdate = Request.Form["FRMDATE"].ToString();
                    string[] ps = fdate.Split('-');
                    fdate = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.FDATE = string.IsNullOrEmpty(fdate) ? null : Convert.ToDateTime(fdate); ;
                }
                else
                {
                    model.FDATE = null;
                }
                did = Convert.ToInt32(model.DCFCID);
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    string str = string.Empty;
                    if (!String.IsNullOrEmpty(Request.Form["HearingDate"]))
                    {
                        str = string.Empty;
                        str = Request.Form["HearingDate"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.HearingDate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.HearingDate = null;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["NextHearingDate"]))
                    {
                        str = string.Empty;
                        str = Request.Form["NextHearingDate"].ToString();
                        string[] ps = str.Split('-');
                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                        model.NextHearingDate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                    }
                    else
                    {
                        model.NextHearingDate = null;
                    }

                    var stat = await _sun.AddSunvahiAsync(model);
                    if (stat != null)
                    {
                        ViewBag.msg = stat.VMEM;
                        model.HearingID = Convert.ToInt32(stat.DMEM);

                        foreach (string key in Request.Form.Keys)
                        {
                            int k = 0;
                            if (key.Contains("SunvahiSahayakAbhiyukt_"))
                            {

                                CSTATUSDETAILS re = new CSTATUSDETAILS();
                                string[] temp = key.Split('_');
                                string row = temp[1];
                                re.DCFCID = did;
                                re.CASESTATUSID = model.CaseStatusId;


                                if (!string.IsNullOrEmpty(Request.Form["SunvahiSahayakAbhiyukt_" + row]) && !string.IsNullOrEmpty(Request.Form["SunvahiABHIYUKTHKAIVIRUDHMSG_" + row])
                                    && !string.IsNullOrEmpty(Request.Form["SunvahiCASERMK_" + row]))
                                {
                                    re.Sahayak_Abhiyukt_ID = Convert.ToInt32(Request.Form["SunvahiSahayakAbhiyukt_" + row]);
                                    re.ABHIYUKTHKAIVIRUDHMSG_ID = Convert.ToInt32(Request.Form["SunvahiABHIYUKTHKAIVIRUDHMSG_" + row]);
                                    //re.AdeshDate = Convert.ToDateTime(Request.Form["SunvahiAdeshDate_" + row]);
                                    if (!String.IsNullOrEmpty(Request.Form["SunvahiAdeshDate_" + row]))
                                    {
                                        str = string.Empty;
                                        str = Request.Form["SunvahiAdeshDate_" + row].ToString();
                                        string[] ps = str.Split('-');
                                        str = ps[2] + "/" + ps[1] + "/" + ps[0];
                                        re.AdeshDate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                                    }
                                    else
                                    {
                                        re.AdeshDate = null;
                                    }
                                    re.REMARK = Request.Form["SunvahiCASERMK_" + row];
                                    var abhiyukthstatus = await _sun.AddAbhiyukthAsync(re);
                                }
                                k = k + 1;
                            }
                        }
                    }


                    //Commented for test
                    //var stat = await _sun.AddSunvahiAsync(model);
                    //if (stat != null)
                    //{
                    //    ViewBag.msg = stat.VMEM;
                    //    model.HearingID = Convert.ToInt32(stat.DMEM);
                    //}
                    model.HearingID = 0;
                    dfid = did.ToString();
                    var VwLst = await _sun.viewSunvahiformat(dfid, "0");
                    ViewBag.SunvahiLst = VwLst;
                    model.DCFCID = did;
                    string cdid = string.Empty;
                    cdid = did.ToString();
                    if (!string.IsNullOrEmpty(cdid))
                    {
                        var abhiLst = await _sun.LstAbhiyukthAsync(cdid);
                        if (abhiLst != null)
                        {
                            ViewBag.SunvahiVIEW = abhiLst;
                        }
                        else
                        {
                            ViewBag.SunvahiVIEW = null;
                        }
                    }
                }
                else
                {
                    HttpContext.Session.Clear();
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Home");
                }
                var CSTAT = await _sun.GetStatusLst();
                ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
                dfid = did.ToString();
                var ABHIYUKTH = await _sun.GetABHIYUKTHLst(dfid);
                ViewBag.ABHIYUKTHDRP = new SelectList(ABHIYUKTH, "Sahayak_Abhiyukt_ID", "ABHIYUKTHNAME", model.Sahayak_Abhiyukt_ID);
                var ABMSG = await _sun.Getabhiyukthmsg();
                ViewBag.ABHIYUKTMSG = new SelectList(ABMSG, "ABHIYUKTHKAIVIRUDHMSG_ID", "MESSAGE", model.ABHIYUKTHKAIVIRUDHMSG_ID);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> INSERTUPDATESUNVAHICIS(string DCFCID, string Hearingid)
        {
            CaseHearing model = new CaseHearing();
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            ViewBag.msg = 0;

            ViewBag.msg = 0;
            if (!string.IsNullOrEmpty(DCFCID))
            {
                model.DCFCID = Convert.ToInt32(DCFCID);
                model.HearingID = 0;
                var st = await _sun.Getdet(DCFCID);
                var entity = st.FirstOrDefault();
                if (entity != null)
                {
                    model = new CaseHearing
                    {
                        APRADHNO = entity.Apradhno,
                        CBCID = entity.cbcidno,
                        Bname = entity.Bname,
                        HearingDate = entity.HearingDate,
                        NextHearingDate = entity.NextHearingDate,
                        CaseStatusId = entity.CaseStatusId,
                        Prosecutor = entity.Prosecutor,
                        Mobile = entity.Mobile

                    };
                }
                else
                {
                    model.DCFCID = 0;
                    model.HearingID = 0;
                }
            }
            if ((!string.IsNullOrEmpty(DCFCID)) && (!string.IsNullOrEmpty(Hearingid)))
            {
                var VwLst = await _sun.viewSunvahiformat(DCFCID, Hearingid);
                var entity1 = VwLst.FirstOrDefault();
                if (entity1 != null)
                {
                    model = new CaseHearing
                    {
                        DCFCID = entity1.DCFCID,
                        HearingID = entity1.HearingID,
                        HearingDate = entity1.HearingDate,
                        NextHearingDate = entity1.NextHearingDate,
                        Remark = entity1.Remark,
                        ADATHAN_STATUS = entity1.ADATHAN_STATUS,
                        CaseStatusId = entity1.CaseStatusId
                    };
                }
                else
                {
                    HttpContext.Session.Clear();
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Home");
                }
            }
            var CSTAT = await _sun.GetStatusLst();
            ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> INSERTUPDATESUNVAHICIS(CaseHearing model)
        {
            var strdistrict = HttpContext.Session.GetString("SDistrict");
            var strrole = HttpContext.Session.GetString("SRole");

            if (!string.IsNullOrEmpty(strdistrict))
            {
                ViewBag.msg = 0;
                string str = string.Empty;
                if (!String.IsNullOrEmpty(Request.Form["HearingDate"]))
                {
                    str = string.Empty;
                    str = Request.Form["HearingDate"].ToString();
                    string[] ps = str.Split('-');
                    str = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.HearingDate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                }
                else
                {
                    model.HearingDate = null;
                }

                if (!String.IsNullOrEmpty(Request.Form["NextHearingDate"]))
                {
                    str = string.Empty;
                    str = Request.Form["NextHearingDate"].ToString();
                    string[] ps = str.Split('-');
                    str = ps[2] + "/" + ps[1] + "/" + ps[0];
                    model.NextHearingDate = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                }
                else
                {
                    model.NextHearingDate = null;
                }
                var stat = await _sun.AddSunvahiAsync(model);
                if (stat != null)
                {
                    ViewBag.msg = stat.VMEM;
                    model.HearingID = Convert.ToInt32(stat.DMEM);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            var CSTAT = await _sun.GetStatusLst();
            ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
            return RedirectToAction("VWSUNVAHICIS", "Sunvahi", new { DCFCID = model.DCFCID });
        }

    }
}
