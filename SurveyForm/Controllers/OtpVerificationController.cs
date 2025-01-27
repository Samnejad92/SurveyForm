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
        [HttpPost]
        public IActionResult SendOtp(Otp home, [FromForm] string personnelNumber)
        {
            //mobile = _context.PersonnelInfos.First(z => z.PersonnelNumber == personnelNumber).ToString();
            mobile = (from p in _context.PersonnelInfos
                     where p.PersonnelNumber == personnelNumber
                      select p.MobileNumber).FirstOrDefault().ToString();
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
            else if (timestamp == null || (DateTime.Now - timestamp.Value).TotalSeconds > 60) return BadRequest("OTP Timed out");
            else if (finalDigit == storedOtp) return RedirectToAction("Index", "Home");
            else return BadRequest("Please Enter Valid OTP");
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
            //var countPhoneNumbers = _context.EssayQuestions.Count();
            //List<string> responses = new List<string>();
            string message = $":کد ورود به نظر سنجی \n {TempData["otp"]}";
            Request Model = new Request();
            //for (int i = 0; i <= countPhoneNumbers; i++)
            //{
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
                Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                //cmdText = "insert into tblTempMsg(Msg,SendDate,SendTime,Shop,MobileNo,statuscode,statusdesc)values('" + Session["msgAll"] + "','" + lblCurrentDate.Text + "','" + DateAndTime.TimeOfDay.ToString("HH:mm:ss:ffff") + "','ALL','" + ddlMobileNo.Items[i].Text + "','" + statuscode + "','" + statusdesc + "')";
                //dl.SQL(al.GetConnectionString("PlanningConnectionString"), cmdText, ref lblError);
            //}
        }

    }
}
