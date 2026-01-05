using CBCID_APPLICATION.Data;
using CBCID_APPLICATION.ICBCID;
using CBCID_APPLICATION.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace CBCID_APPLICATION.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IChangePassword _cpass;
        private readonly ApplicationDBContext _dbContext;

        string strrole = "";
        String vsecid = "";
        String strdistrict = string.Empty;
        public PasswordController(IChangePassword cpass, ApplicationDBContext dBContext)
        {
            _cpass = cpass;
            _dbContext = dBContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassword()
        {
            string strempid = string.Empty;
            string struname = string.Empty;
            Utility_Users model = new Utility_Users();
            strempid = HttpContext.Session.GetString("Userid");
            struname = HttpContext.Session.GetString("UNAME");
            if (!string.IsNullOrEmpty(strempid))
            {
                model.UserName = struname;
                model.EmployeeId = strempid;
            }
            else
            {
                HttpContext.Session.Clear();
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> UpdatePassword(Utility_Users model)
        {
            var stat = await _cpass.UpdatePasswordAsync(model);
            if (stat != null)
            {
                ViewBag.msg = stat.VMEM;
                if(stat.VMEM=="1")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
