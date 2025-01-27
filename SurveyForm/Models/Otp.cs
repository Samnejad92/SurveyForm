namespace SurveyForm.Models
{
    public class Otp
    {
        public int Id { get; set; }
        public string otp { get; set; }
        public int OtpLength { get; set; }
        public int OtpDigit { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateTime { get; set; }
    }
}
