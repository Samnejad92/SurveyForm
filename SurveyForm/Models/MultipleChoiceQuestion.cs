namespace SurveyForm.Models
{
    public class MultipleChoiceQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int QuestionGroup { get; set; }
        public int QuestionOrder { get; set; }
    }
}
