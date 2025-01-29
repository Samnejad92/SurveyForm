using Microsoft.VisualBasic;

namespace SurveyForm.Models
{
    public class LogSentMessage
    {
        public int ID { get; set; }
        public string Msg { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime SendTime { get; set; }
        public string Shop { get; set; }
        public string MobileNo { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
    }
}
