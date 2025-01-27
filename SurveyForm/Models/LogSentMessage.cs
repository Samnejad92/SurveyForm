namespace SurveyForm.Models
{
    public class LogSentMessage
    {
        public int ID { get; set; }
        public string Msg { get; set; }
        public string SendDate { get; set; }
        public DateTime SendTime { get; set; }
        public string Shop { get; set; }
        public string MobileNo { get; set; }
        public string statuscode { get; set; }
        public string statusdesc { get; set; }
    }
}
