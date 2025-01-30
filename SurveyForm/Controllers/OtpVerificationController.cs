using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RestSharp;
using SurveyForm.Data;
using SurveyForm.Models;
using System.Reflection;
using System.Text;
using System.Xml;

namespace SurveyForm.Controllers
{
    public class OtpVerificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static string serviceEndpoint = "http://192.168.201.190:22150/api/SendSMS/SMS";
        private static string mobile;
        public OtpVerificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OTP()
        {
            return View();
        }
        public IActionResult UserNotFound()
        {
            return View();
        }
        public IActionResult NotValidOTP()
        {
            return View();
        }
        public IActionResult OTPTimedOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendOtp(Otp home, [FromForm] string personnelNumber)
        {
            TempData["personnelNumber"] = personnelNumber;
            var personnelNumberInTempData = TempData["personnelNumber"];
            TempData.Keep();
            mobile = (from p in _context.PersonnelInfos
                     where p.PersonnelNumber == personnelNumber
                      select p.MobileNumber).FirstOrDefault().ToString();
            if (mobile == null || mobile == "0") return RedirectToAction("UserNotFound");
            home.MobileNumber = mobile;
            home.OtpLength = home.MobileNumber.Length;
            home.OtpDigit = 5; // Length of the OTP

            home.otp = GenerateUniqueOtp(home);

            TempData["otp"] = home.otp;
            TempData["timestamp"] = DateTime.Now;
            MessageSender();
            return RedirectToAction("OTP");
        }

        [HttpPost]
        public IActionResult SubmitOtp([FromForm] string finalDigit)
        {
            string storedOtp = TempData["otp"] as string;
            DateTime? timestamp = TempData["timestamp"] as DateTime?;

            if (string.IsNullOrEmpty(finalDigit)) return NoContent();
            else if (timestamp == null || (DateTime.Now - timestamp.Value).TotalSeconds > 120) return RedirectToAction("OTPTimedOut");
            else if (finalDigit == storedOtp) return RedirectToAction("Index", "Home");
            else return RedirectToAction("NotValidOTP");
        }

        private string GenerateUniqueOtp(Otp home)
        {
            var random = new Random();
            var otpBuilder = new StringBuilder();

            while (otpBuilder.Length < home.OtpDigit)
            {
                int index = random.Next(0, home.OtpLength);
                string digit = home.MobileNumber[index].ToString();

                if (!otpBuilder.ToString().Contains(digit))
                    otpBuilder.Append(digit);
            }
            return otpBuilder.ToString();
        }

        public void MessageSender()
        {
            string message = $":کد ورود به نظر سنجی \n {TempData["otp"]}";
            Request Model = new Request();
            Model.MobileNo = mobile;
            Model.MessageText = message;

                var client = new RestClient(serviceEndpoint);
                client.Timeout = 10000;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                string jsonBody = JsonConvert.SerializeObject(Model);
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                RestResponse response = (RestResponse)client.Execute(request);
                var R = JsonConvert.DeserializeObject<List<Response>>(response.Content);
                var statuscode = string.Join(",", R.Select(n => n.statuscode));
                //var statusdesc = R.Select(p => p.statusdesc);
                var statusdesc = string.Join(",", R.Select(n => n.statusdesc));
            var log = new LogSentMessage
            {
                Msg = message,
                SendDate = DateTime.Now,
                SendTime = DateAndTime.TimeOfDay,
                Shop = "null",
                MobileNo = mobile,
                StatusCode = statuscode,
                StatusDesc = statusdesc
            };
            _context.LogSentMessages.Add(log);
            _context.SaveChanges();
        }
    }
}
