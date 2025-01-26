using Microsoft.AspNetCore.Mvc;
using SurveyForm.Data;
using SurveyForm.Models;
using System.Text;

namespace SurveyForm.Controllers
{
    public class OtpVerificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OtpVerificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OTP()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendOtp(Otp home)
        {
            home.MobileNumber = "01223456789";
            home.OtpLength = home.MobileNumber.Length;
            home.OtpDigit = 5; // Length of the OTP

            home.otp = GenerateUniqueOtp(home);

            TempData["otp"] = home.otp;
            TempData["timestamp"] = DateTime.Now;

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
    }
}
