namespace SurveyForm.Models
{
    public class EssayAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int PersonnelNumber { get; set; }
        public string QuestionName { get; set; }
        public string QuestionAnswer { get; set; }
    }
}
