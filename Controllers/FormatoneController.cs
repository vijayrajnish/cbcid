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
using System.Globalization;
using Microsoft.Extensions.Primitives;

namespace CBCID_APPLICATION.Controllers
{
    public class FormatoneController : Controller
    {
        private readonly IDET_CRIME_FEMALE_CHILDREN _Fmt;
        private readonly ApplicationDBContext _dbContext;
        private readonly IAddSunvahi _sun;
        string strsecid = "";
        string strrole = "";
        String vsecid = "";
        String strdistrict = string.Empty;
        public FormatoneController(IDET_CRIME_FEMALE_CHILDREN Fmt, ApplicationDBContext dBContext, IAddSunvahi sun)
        {
            _Fmt = Fmt;
            _sun = sun;
            _dbContext = dBContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> VWFmt(string SCBCID,string SApradhNo,string CRNO, string FMTID,string FrmDate, string ToDate,string CaseStatusId,string DISTRICTID,string DCFCID,string SIBNO)
        {
            string str = string.Empty;
            string sid = string.Empty;
            DET_CRIME_FEMALE_CHILDREN model = new DET_CRIME_FEMALE_CHILDREN();
            String FDATE=string.Empty;
            String TDATE= string.Empty;
            string dfif= string.Empty;
            try
            {
                var str_role = HttpContext.Session.GetString("SRole");
                var strsecid = HttpContext.Session.GetString("SID");
                if (!string.IsNullOrEmpty(strsecid))
                {
                    if (!string.IsNullOrEmpty(FrmDate))
                    {
                        DateTime nextHearingDate;
                        bool isValid = DateTime.TryParseExact(
                            FrmDate,
                            "dd-MM-yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out nextHearingDate
                        );
                        if (isValid)
                        {
                            //FDATE = nextHearingDate;
                            ViewBag.FrmDate = nextHearingDate.ToString("dd-MM-yyyy");
                            FDATE = nextHearingDate.ToString("MM-dd-yyyy"); ;// Or any desired format
                        }
                        else
                        {
                            ViewBag.FrmDate = "";
                        }
                    }
                    if (!string.IsNullOrEmpty(ToDate))
                    {
                        DateTime nextHearingDate1;
                        bool isValid = DateTime.TryParseExact(
                            ToDate,
                            "dd-MM-yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out nextHearingDate1
                        );

                        if (isValid)
                        {
                            // Use nextHearingDate1 as DateTime variable for further processing
                            ViewBag.ToDate = nextHearingDate1.ToString("dd-MM-yyyy"); // Or any desired format
                            TDATE = nextHearingDate1.ToString("MM-dd-yyyy"); ;
                        }
                        else
                        {
                            ViewBag.ToDate = "";
                        }
                    }
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        dfif = DCFCID;
                        model.DCFCID = Convert.ToInt32(dfif);
                    }
                    else
                    {
                        model.DCFCID = 0;
                    }
                    if (!string.IsNullOrEmpty(strsecid))
                    {
                        sid = strsecid;
                        if (!string.IsNullOrEmpty(str_role))
                        {
                            strrole = str_role;
                        }
                        if (!string.IsNullOrEmpty(strrole))
                        {
                            
                            var VwLst = await _Fmt.viewformatsrh(SApradhNo, SCBCID, FMTID, sid, FDATE, TDATE, CaseStatusId, DISTRICTID, dfif, SIBNO, strrole,CRNO);
                            ViewBag.FmtLst = VwLst;
                        }
                        
                    }
                    else
                    {
                        await SessionExpired();
                    }
                    var FMT = await _Fmt.GetALLFMTLst();
                    ViewBag.FMTID = new SelectList(FMT, "FMTID", "FNAME");
                    var CSTAT = await _sun.GetStatusLst();
                    ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
                    var dist = await _Fmt.GetDistrictSectorwiseLst(strsecid);
                    ViewBag.Did = new SelectList(dist, "DistrictId", "DistrictName", model.DISTRICTID);
                }
                else
                {
                    await SessionExpired();
                }
            }
            catch (Exception ex)
            {
               await SessionExpired();
               throw;
            }
            return View(model);

        }


        private async Task<IActionResult> SessionExpired()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> FmtInsertUpdate(string DCFCID)
        {
            ViewBag.GavahaCnt = null;
            ViewBag.ABHILST = null;
            DET_CRIME_FEMALE_CHILDREN model = new DET_CRIME_FEMALE_CHILDREN();
           var strdistrict = HttpContext.Session.GetString("SDistrict");
           var strrole= HttpContext.Session.GetString("SRole");
           var vsecid= HttpContext.Session.GetString("SID");
            try
            {
                //Here updated by Feild is your Role feild
                model.UPDATEDBY = strrole;
                model.ABHIYUKTHKAIVIRUDHMSG_ID = 0;
                model.GOVTOFFICER = 0;
                string str = string.Empty;
                if (!string.IsNullOrEmpty(strdistrict))
                {
                    ViewBag.msg = 0;
                    ViewBag.dfid = 0;
                    int DFID = 0;
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        DFID = Convert.ToInt32(DCFCID);
                        var Lst = _dbContext.FMT1_DET_CRIME_FEMALE_CHILDREN.Find(DFID);
                        if (Lst != null)
                        {
                            model.DCFCID = Lst.DCFCID;
                            model.SECTORID = Lst.SECTORID;
                            model.DISTRICTID = Lst.DISTRICTID;
                            model.SECTORID = Lst.SECTORID;
                            model.YEAR = Lst.YEAR;
                            model.DAKILDATE = (Lst.DAKILDATE != null ? Convert.ToDateTime(Lst.DAKILDATE.ToString()) : null);
                            // model.NEXT_HEARING_DATE = (Lst.NEXT_HEARING_DATE != null ? Convert.ToDateTime(Lst.NEXT_HEARING_DATE) : null);
                            model.ARROPVICHARANTHITHI = (Lst.ARROPVICHARANTHITHI != null ? Convert.ToDateTime(Lst.ARROPVICHARANTHITHI) : null);
                            model.ABHIYUKT = Lst.ABHIYUKT;
                            model.BNAME = Lst.BNAME;
                            model.FATHERNAME = (Lst.FATHERNAME != null ? Lst.FATHERNAME : null); //Lst.FATHERNAME;
                            model.ADDRESS = (Lst.ADDRESS != null ? Lst.ADDRESS : null); //Lst.ADDRESS;
                            model.MOBILENO = (Lst.MOBILENO != null ? Lst.MOBILENO : null); //Lst.MOBILENO;
                            model.LETTERNO = Lst.LETTERNO;
                            model.LETTERDATE = (Lst.LETTERDATE != null ? Convert.ToDateTime(Lst.LETTERDATE) : null);
                            model.CBCID_NO = (Lst.CBCID_NO != null ? Lst.CBCID_NO : null);
                            model.APRADHNO = (Lst.APRADHNO != null ? Lst.APRADHNO : null);
                            model.COURTID = (Lst.COURTID != 0 ? Lst.COURTID : 0); //Lst.COURTID;
                            model.PSHAKSHI_NAME = Lst.PSHAKSHI_NAME;
                            model.RSHAKSHI_NAME = Lst.RSHAKSHI_NAME;
                            model.ADATHAN_STATUS = Lst.ADATHAN_STATUS;
                            model.ACT = Lst.ACT;
                            model.THANAID = Lst.THANAID;
                            model.FMTID = Lst.FMTID;
                            model.APRADHYEAR = Lst.APRADHYEAR;
                            model.CaseStatusId = Lst.CaseStatusId;
                            model.TransferFrm = Lst.TransferFrm;
                            model.CNRNO = Lst.CNRNO;
                            model.PLNO = Lst.PLNO;
                            model.GOVTOFFICER = (Lst.GOVTOFFICER != null ? Lst.GOVTOFFICER : 0); 
                            model.SIBNO = (Lst.SIBNO != null ? Lst.SIBNO : null);
                            model.SIBNOYR = Lst.SIBNOYR;
                            model.PSAVE = (Lst.PSAVE != 0 ? Lst.PSAVE : 0); //Lst.COURTID;
                            model.FRMDATE = (Lst.FRMDATE != null ? Convert.ToDateTime(Lst.FRMDATE.ToString()) : null);
                        }
                    }
                    else
                    {
                        model.SECTORID = 0;
                        model.DISTRICTID = 0;
                        
                    }
                    if (model.SECTORID != 0)
                    {
                        vsecid = (Convert.ToString(model.SECTORID));
                    }
                    if (model.DISTRICTID != 0)
                    {
                        strdistrict = (Convert.ToString(model.DISTRICTID));
                    }

                   if(!string.IsNullOrEmpty(strrole))
                    {
                        string strole = strrole;
                        if (strole == "2")
                        {
                            model.SECTORID = Convert.ToInt32(vsecid);
                            var dist = await _Fmt.GetDistrictSectorwiseLst(vsecid);
                            ViewBag.Did = new SelectList(dist, "DistrictId", "DistrictName", model.DISTRICTID);
                            var SEC = await _Fmt.GetSectorLst(vsecid);
                            ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME", model.SECTORID);
                            var THANALST = await _Fmt.GetThanaLst(strdistrict);
                            ViewBag.THANAID = new SelectList(THANALST, "THANAID", "THANANAME", model.THANAID);
                        }
                        if (strole == "3")
                        {
                            model.SECTORID = 9;
                            var dist = await _Fmt.GetDistrictSectorwiseLst("0");
                            ViewBag.Did = new SelectList(dist, "DistrictId", "DistrictName", model.DISTRICTID);
                            var SEC = await _Fmt.GetSectorKhandLst("9");
                            ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME", model.SECTORID);
                            var THANALST = await _Fmt.GetThanaLst("79");
                            ViewBag.THANAID = new SelectList(THANALST, "THANAID", "THANANAME", model.THANAID);
                        }
                    }
                    var CRT = await _Fmt.GetCourtLst();
                    ViewBag.CRTID = new SelectList(CRT, "COURTID", "NAMEOFCOURT", model.COURTID);
                    var YR = await _Fmt.GetYRLst();
                    ViewBag.YEAR = new SelectList(YR, "YrId", "Year", model.YEAR);
                    var APYR = await _Fmt.GetYRLst();
                    ViewBag.APYEAR = new SelectList(YR, "YrId", "Year", model.APRADHYEAR);
                    var SBYR = await _Fmt.GetYRLst();
                    ViewBag.SBYEAR = new SelectList(YR, "YrId", "Year", model.SIBNOYR);
                    var FMTLST = await _Fmt.GetALLFMTLst();
                    ViewBag.FMTID = new SelectList(FMTLST, "FMTID", "FNAME");
                    var CSTAT = await _Fmt.GetStatusLst();
                    ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus");
                    
                    if (!string.IsNullOrEmpty(DCFCID))
                    {
                        var GHCNT = await _Fmt.GetGavahaCnt(DCFCID ?? "0");
                        ViewBag.GavahaCnt = GHCNT;
                        var ABHIYUKTCNT = await _Fmt.GetAbhiyuktCnt(DCFCID ?? "0");
                        ViewBag.ABHILST = ABHIYUKTCNT;
                        var INVESTCNT = await _Fmt.GetINVESTCnt(DCFCID ?? "0");
                        ViewBag.INVESTLSTCNT = INVESTCNT;
                    }
                    else
                    {
                        ViewBag.GavahaCnt = null;
                        ViewBag.ABHILST = null;
                        ViewBag.INVESTLSTCNT = null;
                    }
                }
                else
                {
                    await SessionExpired();
                }
            }
            catch (Exception ex)
            {
              await  SessionExpired();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FmtInsertUpdate(string DCFCID, DET_CRIME_FEMALE_CHILDREN model)
        {
           var strdistrict = HttpContext.Session.GetString("SDistrict");
           var strrole = HttpContext.Session.GetString("SRole");
           var vsecid = HttpContext.Session.GetString("SID");
            model.UPDATEDBY = strrole;
            ViewBag.GavahaCnt = null;
            ViewBag.ABHILST = null;
            try
            {
                if(!string.IsNullOrEmpty(strrole))
                {
                    model.CREATEDBY = strrole;
                }
                else
                {
                    await SessionExpired();
                }
                if (!String.IsNullOrEmpty(strdistrict))
                {
                    if (!String.IsNullOrEmpty(Request.Form["DCFCID"]))
                    {
                        model.DCFCID = Convert.ToInt32(Request.Form["DCFCID"]);
                    }
                    if (!string.IsNullOrEmpty(strdistrict))
                    {
                        ViewBag.msg = 0;
                        string str = string.Empty;
                        if (!String.IsNullOrEmpty(Request.Form["DAKILDATE"]))
                        {
                            str = string.Empty;
                            str = Request.Form["DAKILDATE"].ToString();
                            string[] ps = str.Split('-');
                            str = ps[2] + "/" + ps[1] + "/" + ps[0];
                            model.DAKILDATE = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                        }
                        else
                        {
                            model.DAKILDATE = null;
                        }
                        if (!String.IsNullOrEmpty(Request.Form["ARROPVICHARANTHITHI"]))
                        {
                            str = string.Empty;
                            str = Request.Form["ARROPVICHARANTHITHI"].ToString();
                            string[] ps = str.Split('-');
                            str = ps[2] + "/" + ps[1] + "/" + ps[0];
                            model.ARROPVICHARANTHITHI = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                        }
                        else
                        {
                            model.ARROPVICHARANTHITHI = null;
                        }
                        if (!String.IsNullOrEmpty(Request.Form["LETTERDATE"]))
                        {
                            str = string.Empty;
                            str = Request.Form["LETTERDATE"].ToString();
                            string[] ps = str.Split('-');
                            str = ps[2] + "/" + ps[1] + "/" + ps[0];
                            model.LETTERDATE = string.IsNullOrEmpty(str) ? null : Convert.ToDateTime(str);
                        }
                        else
                        {
                            model.LETTERDATE = null;
                        }
                        var stat = await _Fmt.AddFormatAsync(model);
                        if (stat != null)
                        {
                            ViewBag.msg = stat.VMEM;
                            ViewBag.dfid = stat.DMEM;
                            model.DCFCID = Convert.ToInt32(stat.DMEM);
                        }

                        if (!string.IsNullOrEmpty(strrole))
                        {
                            string strole = strrole;
                            if (strole == "2")
                            {
                                model.SECTORID = Convert.ToInt32(vsecid);
                                var dist = await _Fmt.GetDistrictSectorwiseLst(vsecid);
                                ViewBag.Did = new SelectList(dist, "DistrictId", "DistrictName", model.DISTRICTID);
                                var SEC = await _Fmt.GetSectorLst(vsecid);
                                ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME", model.SECTORID);
                                var THANALST = await _Fmt.GetThanaLst(strdistrict);
                                ViewBag.THANAID = new SelectList(THANALST, "THANAID", "THANANAME", model.THANAID);
                            }
                            if (strole == "3")
                            {
                                model.SECTORID = 9;
                                var dist = await _Fmt.GetDistrictSectorwiseLst("0");
                                ViewBag.Did = new SelectList(dist, "DistrictId", "DistrictName", model.DISTRICTID);
                                var SEC = await _Fmt.GetSectorKhandLst("9");
                                ViewBag.SECID = new SelectList(SEC, "SECTORID", "SEC_ENAME", model.SECTORID);
                                var THANALST = await _Fmt.GetThanaLst("79");
                                ViewBag.THANAID = new SelectList(THANALST, "THANAID", "THANANAME", model.THANAID);
                            }
                        }
                        var CRT = await _Fmt.GetCourtLst();
                        ViewBag.CRTID = new SelectList(CRT, "COURTID", "NAMEOFCOURT", model.COURTID);
                        var FMTLST = await _Fmt.GetALLFMTLst();
                        ViewBag.FMTID = new SelectList(FMTLST, "FMTID", "FNAME");
                        var YR = await _Fmt.GetYRLst();
                        ViewBag.YEAR = new SelectList(YR, "YrId", "Year", model.YEAR);
                        var APYR = await _Fmt.GetYRLst();
                        ViewBag.APYEAR = new SelectList(YR, "YrId", "Year", model.APRADHYEAR);
                        var SBYR = await _Fmt.GetYRLst();
                        ViewBag.SBYEAR = new SelectList(YR, "YrId", "Year", model.SIBNOYR);
                        var CSTAT = await _Fmt.GetStatusLst();
                        ViewBag.CaseStatusId = new SelectList(CSTAT, "CaseStatusId", "CStatus",model.CaseStatusId);
                        if (!string.IsNullOrEmpty(DCFCID))
                        {

                            var GHCNT = await _Fmt.GetGavahaCnt(DCFCID ?? "0");
                            ViewBag.GavahaCnt = GHCNT;
                            var ALst = await _Fmt.GetAbhiyuktCnt(DCFCID ?? "0");
                            ViewBag.ABHILST = ALst;
                            var INVESTCNT = await _Fmt.GetINVESTCnt(DCFCID ?? "0");
                            ViewBag.INVESTLSTCNT = INVESTCNT;
                        }
                        else
                        {
                            ViewBag.GavahaCnt = 0;
                            ViewBag.ABHILST = 0;
                            ViewBag.INVESTLSTCNT = 0;
                        }
                    }
                    else
                    {
                        await SessionExpired();
                    }
                }
                else
                {
                    await SessionExpired();
                }
            }
            catch (Exception ex)
            {
                await SessionExpired();
            }
            return View(model);
        }

    }
}
