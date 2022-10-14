using BloodBankManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BloodBankManagementSystem.Controllers
{
    public class ValidatedController : Controller
    {
        [Authorize(Roles ="donor")]
        public ActionResult DonorDashboard()
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            return View("DonorDashboard");
        }
        public ActionResult DonorsBloodDonateRequest()
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            DonateBloodRequestModel oDonateBloodRequestModel = oDonateBloodRequestModelManager.GetBloodDetails();
            return View("DonorsDonateRequest", oDonateBloodRequestModel);
        }
        public JsonResult GetBloodCost(string bg)
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            int cost = oDonateBloodRequestModelManager.GetCostOfBlood(bg);
            return Json(cost);
        }
        [HttpPost]
        [Authorize(Roles = "donor")]
        public ActionResult DonorBloodDonateRequest(DonateBloodRequestModel oDonateBloodRequestModel)
        {
            oDonateBloodRequestModel.Rid =  Convert.ToInt32(Session["rid"]);
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            oDonateBloodRequestModelManager.InsertBloodDonateRequest(oDonateBloodRequestModel);
            DonateBloodRequestModel donateBloodRequestModel = oDonateBloodRequestModelManager.GetBloodDetails();
            donateBloodRequestModel.Message = "Your Request has been sent to Blood Bank, Blood Bank Respond to you soon.";
            return View("DonorDashboard", donateBloodRequestModel);
        }
        [Authorize(Roles ="acceptor")]
        public ActionResult AcceptorsDashboard()
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();            
            List<DonorHospitalDetailsModel> lstDonorHospitalDetails = oDonateBloodRequestModelManager.GetDOnorHospitalsDetails();
            return View("AcceptorsDashboard", lstDonorHospitalDetails);
        }

        public ActionResult AcceptorsSednBloodRequest()
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            BloodRequestModel oBloodRequestModel = oDonateBloodRequestModelManager.GetAllBloodProductDetails();
            return View("AcceptorsBloodRequest", oBloodRequestModel);
        }
        [HttpPost]
        public ActionResult DetailsofDonorsHospitals(int Id)
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            DonorHospitalDetailsModel oDonorHospitalDetailsModel = oDonateBloodRequestModelManager.GetDetails(Id);
            return PartialView("_DetailsofDonorsHospitals", oDonorHospitalDetailsModel);
        }
        public ActionResult AcceptorsHistory()
        {
            int Rid = Convert.ToInt32(Session["rid"]);
            LastHistoryModelManager oLastHistoryModelManager = new LastHistoryModelManager();
            List<LastHistoryModel> lstLastHistoryModel = oLastHistoryModelManager.GetAcceptorHistory(Rid);
            return View("AcceptorsHistory",lstLastHistoryModel);
        }
        public JsonResult GetCost(string bg, string product)
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            int Cost = oRegistrationModelManager.GetCostOfBlood(bg, product);
            return Json(Cost);
        }
        [HttpPost]
        [Authorize(Roles = "acceptor")]
        public ActionResult AcceptorBloodRequest(BloodRequestModel oBloodRequestModel)
        {
            oBloodRequestModel.Rid = Convert.ToInt32(Session["rid"]);
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            oDonateBloodRequestModelManager.InsertBloodRequest(oBloodRequestModel);
            BloodRequestModel bloodRequestModel = oDonateBloodRequestModelManager.GetAllBloodProductDetails();
            bloodRequestModel.Message = "Your Request has been sent to Blood Bank, Blood Bank Respond to you soon.";
            return View("AcceptorsBloodRequest", bloodRequestModel);
        }
        [Authorize(Roles = "hospital")]
        public ActionResult HospitalDashboard()
        {
            return View("HospitalDashboard");
        }
        [Authorize(Roles = "hospital")]
        public ActionResult HospitalRequestForBlood()
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            BloodRequestModel oBloodRequestModel = oDonateBloodRequestModelManager.GetAllBloodProductDetails();
            return View("HospitalRequestForBlood", oBloodRequestModel);
        }
        [HttpPost]
        public ActionResult HospitalRequestForBlood(BloodRequestModel oBloodRequestModel)
        {
            oBloodRequestModel.Rid = Convert.ToInt32(Session["rid"]);
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            oDonateBloodRequestModelManager.InsertBloodRequest(oBloodRequestModel);
            BloodRequestModel bloodRequestModel = oDonateBloodRequestModelManager.GetAllBloodProductDetails();
            bloodRequestModel.Message = "Your Request has been sent to Blood Bank, Blood Bank Respond to you soon.";
            return View("HospitalRequestForBlood", bloodRequestModel);
        }
        [Authorize(Roles = "hospital")]
        public ActionResult HospitalDonateBloodRequest()
        {
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            DonateBloodRequestModel oDonateBloodRequestModel = oDonateBloodRequestModelManager.GetBloodDetails();
            return View("HospitalDonateBloodRequest", oDonateBloodRequestModel);
        }
        [HttpPost]
        public ActionResult HospitalDonateBloodRequest(DonateBloodRequestModel oDonateBloodRequestModel)
        {
            oDonateBloodRequestModel.Rid = Convert.ToInt32(Session["rid"]);
            DonateBloodRequestModelManager oDonateBloodRequestModelManager = new DonateBloodRequestModelManager();
            oDonateBloodRequestModelManager.InsertBloodDonateRequest(oDonateBloodRequestModel);
            DonateBloodRequestModel donateBloodRequestModel = oDonateBloodRequestModelManager.GetBloodDetails();
            donateBloodRequestModel.Message = "Your Request has been sent to Blood Bank, Blood Bank Respond to you soon.";
            return View("HospitalDonateBloodRequest", donateBloodRequestModel);
        }
        [Authorize(Roles = "hospital")]
        public ActionResult StockAvailability()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            List<StockAvailabilityModel> lstStockAvailabilityModels = oRegistrationModelManager.GetBloodStockAvailability();
            return View("HospitalCheckStockAvailability",lstStockAvailabilityModels);
        }
        [Authorize(Roles = "admin")]
        public ActionResult AdminDashboard()
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            BadgeValuesModel oBadgeValuesModel = oBloodRequestModelManager.GetNumberOfRequests();
            ViewBag.Bloodreq = oBadgeValuesModel.BloodReq;
            ViewBag.Donatereq = oBadgeValuesModel.DonateReq;
            ViewBag.Unregreq = oBadgeValuesModel.UnregReq;
            List<DataPoint> dataPoints = oBloodRequestModelManager.GetGraphDetails();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View("AdminDashboard");
        }
        [ChildActionOnly]
        public ActionResult BloodReqGraph()
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            List<DataPoint> dataPoints = oBloodRequestModelManager.GetGraphDetails();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return PartialView("_BloodReqGraph");
        }
        [ChildActionOnly]
        public ActionResult StockGraph()
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            List<DataPoint> lstDataPoints = oBloodRequestModelManager.GetStockData();
            ViewBag.StockPoints = JsonConvert.SerializeObject(lstDataPoints);
            return PartialView("_StockGraph");
        }
        [Authorize(Roles = "admin")]
        public ActionResult PendingBloodRequests()
        {
            BloodRequestModelManager bloodRequestModelManager = new BloodRequestModelManager();
            BloodRequestViewModel oBloodRequestViewModel = new BloodRequestViewModel()
            {
                ListBloodRequest = bloodRequestModelManager.GetPendingBloodRequests(),
                ListSearchValues = bloodRequestModelManager.GetddlCityValues()
            };
            return View("PendingBloodRequests",oBloodRequestViewModel);
        }
        public ActionResult RespondToBloodRequest(int Rid,string Name,string Mobile,string Email)
        {
            return PartialView("_RespondToBloodRequest");
        }
        [HttpPost]
        public JsonResult SendMailToUser(string toEmail, string subject, string emailBody)
        {
            bool result = false;
            result = SendEmail(toEmail, subject, emailBody);
            return Json(result,JsonRequestBehavior.AllowGet);
        }        
        public bool SendEmail(string toEmail,string subject,string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["senderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["senderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail,toEmail,subject,emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                return false;
            }
        }
        //public bool RespondBloodRequest(RespondToBloodRequestDonateModel respondToBloodRequestDonateModel)
        //{
        //    try
        //    {
        //            var senderEmail = new MailAddress("prabhuling66@gmail.com", "Prabhu");
        //            var receiver = respondToBloodRequestDonateModel.Email;
        //            var receiverEmail = new MailAddress(receiver, "Receiver");
        //            var password = "Pintu14317";
        //            var sub = "Blood Availability";
        //            var body = respondToBloodRequestDonateModel.Message;
        //        var smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            Timeout = 100000,
        //                DeliveryMethod = SmtpDeliveryMethod.Network,
        //                UseDefaultCredentials = false,
        //                Credentials = new NetworkCredential(senderEmail.Address, password)
        //            };
        //            using (var mess = new MailMessage(senderEmail, receiverEmail)
        //            {
        //                Subject = "Blood Request",
        //                Body = body,
        //                IsBodyHtml=true,
        //                BodyEncoding=UTF8Encoding.UTF8
        //            })
        //            {
        //                smtp.Send(mess);
        //            }
        //            return RedirectToAction("PendingBloodRequests");
                
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Some Error";
        //        return View("Error");
        //    }
            
        //}
        [HttpPost]
        public bool Update()
        {
            return false;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SearchBloodRequest(SearchValuesBloodRequest ListSearchValues)
        {
            BloodRequestModelManager bloodRequestModelManager = new BloodRequestModelManager();
            BloodRequestViewModel oBloodRequestViewModel = new BloodRequestViewModel()
            {
                ListBloodRequest = bloodRequestModelManager.GetSearchedBloodRequests(ListSearchValues),
                ListSearchValues = bloodRequestModelManager.GetddlCityValues()
            };
            return View("PendingBloodRequests", oBloodRequestViewModel);
        }
        [Authorize(Roles = "admin")]
        public ActionResult DonorBloodRequests()
        {
            BloodRequestModelManager bloodRequestModelManager = new BloodRequestModelManager();
            BloodRequestViewModel oBloodRequestViewModel = new BloodRequestViewModel()
            {
                ListBloodRequest = bloodRequestModelManager.GetPendingDonateRequests(),
                ListSearchValues = bloodRequestModelManager.GetddlCityValues()
            };
            return View("DonorBloodRequests", oBloodRequestViewModel);
        }
        public ActionResult RespondToDonateRequest(int Rid, string Name, string Mobile, string Email)
        {
            return PartialView("_RespondToDonateRequest");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SearchDonorRequest(SearchValuesBloodRequest ListSearchValues)
        {
            BloodRequestModelManager bloodRequestModelManager = new BloodRequestModelManager();
            BloodRequestViewModel oBloodRequestViewModel = new BloodRequestViewModel()
            {
                ListBloodRequest = bloodRequestModelManager.GetSearchedDonateRequests(ListSearchValues),
                ListSearchValues = bloodRequestModelManager.GetddlCityValues()
            };
            return View("DonorBloodRequests", oBloodRequestViewModel);
        }
        [Authorize(Roles = "admin")]
        public ActionResult UnregisteredBloodRequest()
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            UnregisteredViewModel oUnregisteredViewModel = new UnregisteredViewModel()
            {
                LstUnregisteredUserModels = oBloodRequestModelManager.GetAllUnregisteredBloodRequests(),
                SearchValuesModel = oBloodRequestModelManager.GetAllCities()
            };
            return View("UnregisteredBloodRequest", oUnregisteredViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SearchUnregisteredRequest(UnregisteredSearchValuesModel SearchValuesModel)
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            UnregisteredViewModel oUnregisteredViewModel = new UnregisteredViewModel()
            {
                LstUnregisteredUserModels = oBloodRequestModelManager.GetAllUnregisteredSearchedRequests(SearchValuesModel),
                SearchValuesModel = oBloodRequestModelManager.GetAllCities()
            };
            return View("UnregisteredBloodRequest", oUnregisteredViewModel);
        }
        public ActionResult RespondToUnregisteredRequest(RespondToUregisteredRequestModel oRespondToUregisteredRequestModel)
        {
            return PartialView("_RespondToUnregisteredRequest");
        }
        [HttpPost]
        public JsonResult SendMessage(string Mobile,string Message)
        {
            bool result = false;
            try
            {
                String message = HttpUtility.UrlEncode(Message);

                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                        {
                            {"apikey" , "Fe36ccwDf2A-Qzwwgyn2K6qTmAPqaqDleFlPxkqscm"},
                            {"numbers" , Mobile},
                            {"message" , message},
                            {"sender" , "TXTLCL"}
                        });
                    // string result = System.Text.Encoding.UTF8.GetString(response);
                    result = true;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult UpdateStockAvailabilityDetails()
        {
            RegistrationModelManager oRegistrationModelManager = new RegistrationModelManager();
            BloodStockCostViewModel oBloodStockCostViewModel = new BloodStockCostViewModel
            {
                LstStockAvailability = oRegistrationModelManager.GetBloodStockAvailability(),
                LstBloodCost = oRegistrationModelManager.GetBloodCosts()
            };
            return View("UpdateStockAvailabilityDetails", oBloodStockCostViewModel);
        }
        
        public ActionResult UpdateBloodStockAvailability(StockAvailabilityModel oStockAvailabilityModel)
        {
            return PartialView("_UpdateBloodStock");
        }
        public ActionResult UpdateBloodCost(BloodCost oBloodCost)
        {
            return PartialView("_UpdateBloodCost");
        }
        [HttpPost]
        public ActionResult UpdateBloodStock(StockAvailabilityModel oStockAvailabilityModel)
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            oBloodRequestModelManager.UpdateBloodStockAvailability(oStockAvailabilityModel);
            return RedirectToAction("UpdateStockAvailabilityDetails");
        }
        [HttpPost]
        public ActionResult UpdateCostofBlood(BloodCost oBloodCost)
        {
            BloodRequestModelManager oBloodRequestModelManager = new BloodRequestModelManager();
            oBloodRequestModelManager.UpdateBloodCost(oBloodCost);
            return RedirectToAction("UpdateStockAvailabilityDetails");
        }
        [Authorize(Roles = "admin")]
        public ActionResult RegisteredClientDetails()
        {
            RegisteredClientModelManager registeredClientModelManager = new RegisteredClientModelManager();
            List<RegisteredClientModel> lstRegisteredClientModels = registeredClientModelManager.GetAllRegisteredClientDetails();
            return View("RegisteredClientDetails",lstRegisteredClientModels);
        }
        [HttpPost]
        public ActionResult UpdateRegisteredClient(RegisteredClientModel oRegisteredClientModel)
        {
            return PartialView("_UpdateRegisteredClient");
        }
        [HttpPost]
        public ActionResult UpdateClientDetails(RegisteredClientModel oRegisteredClientModel)
        {
            RegisteredClientModelManager registeredClientModelManager = new RegisteredClientModelManager();
            registeredClientModelManager.UpdateClientDetails(oRegisteredClientModel);
            return RedirectToAction("RegisteredClientDetails");
        }
    }
}