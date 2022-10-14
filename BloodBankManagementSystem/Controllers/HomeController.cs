using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BloodBankManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View("HomePage");
        }
        public ActionResult DonorRegistration()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            RegistrationModel oRegistrationModel = oRegistrationModelManager.GetAllStateGender();
            return View("DonorRegistration",oRegistrationModel);
        }
        [HttpPost]
        public ActionResult DonorRegistration(RegistrationModel oRegistrationModel)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            oRegistrationModel.Role = "donor";
            oRegistrationModelManager.InsertRegistration(oRegistrationModel);
            return View("HomePage");
        }
        [HttpPost]
        public JsonResult GetCityList(string state)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            List<string> lstCity = oRegistrationModelManager.GetAllCities(state);
            return Json(lstCity);
        }
        [HttpPost]
        public JsonResult GetCost(string bg,string product)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            int Cost = oRegistrationModelManager.GetCostOfBlood(bg, product);
            return Json(Cost);
        }
        public ActionResult AcceptorsRegistration()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            RegistrationModel oRegistrationModel = oRegistrationModelManager.GetAllStates();
            return View("AcceptorsRegistration", oRegistrationModel);
        }
        [HttpPost]
        public ActionResult AcceptorsRegistration(RegistrationModel oRegistrationModel)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            oRegistrationModel.Role = "acceptor";
            oRegistrationModelManager.InsertRegistration(oRegistrationModel);
            return View("HomePage");
        }
        public ActionResult HospitalRegistration()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            RegistrationModel oRegistrationModel = oRegistrationModelManager.GetAllStates();
            return View("HospitalRegistration", oRegistrationModel);
        }
        [HttpPost]
        public ActionResult HospitalRegistration(RegistrationModel oRegistrationModel)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            oRegistrationModel.Role = "hospital";
            oRegistrationModelManager.InsertRegistration(oRegistrationModel);
            return View("HomePage");
        }
        public ActionResult StockAvailability()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            BloodStockCostViewModel oBloodStockCostViewModel = new BloodStockCostViewModel
            {
                LstStockAvailability = oRegistrationModelManager.GetBloodStockAvailability(),
                LstBloodCost = oRegistrationModelManager.GetBloodCosts()
            };
            return View("StockAvailability", oBloodStockCostViewModel);
        }
        public ActionResult UnregisteredBloodRequest()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            UnregisteredUserModel oUnregisteredUserModel = oRegistrationModelManager.GetAllStatesBloodDetails();
            return View("UnregisteredBloodRequest", oUnregisteredUserModel);
        }
        [HttpPost]
        public ActionResult UnregisteredBloodRequest(UnregisteredUserModel oUnregisteredUserModel)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            oRegistrationModelManager.UnregisteredUserBloodRequest(oUnregisteredUserModel);
            return View("HomePage");
        }
        [HttpPost]
        public ActionResult Login(LoginModel oLoginModel)
        {
            LoginModelManager oLoginModelManager = new LoginModelManager();
            LoginModel loginModel = oLoginModelManager.UserAuth(oLoginModel);
            if (loginModel.Isvalid == 1)
            {
                Session["name"] = loginModel.Name;
                Session["rid"] = loginModel.Rid;
                Session.Timeout = 60;
                FormsAuthentication.SetAuthCookie(loginModel.Name, false);
                var authTicket = new FormsAuthenticationTicket(1, loginModel.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false, loginModel.Role);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                if (loginModel.Role== "donor")
                {
                    return RedirectToAction("DonorDashboard", "Validated");
                }
                else if (loginModel.Role == "hospital")
                {
                    return RedirectToAction("HospitalDashboard", "Validated");
                }
                else if (loginModel.Role == "acceptor")
                {
                    return RedirectToAction("AcceptorsDashboard", "Validated");
                }
                else if (loginModel.Role == "admin")
                {
                    return RedirectToAction("AdminDashboard", "Validated");
                }
            }
            else
            {
                loginModel.Msg = "Wrong Email/Mobile or Password";
                return View("Homepage", loginModel);
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Homepage", "Home");
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}