using Microsoft.EntityFrameworkCore;

namespace SurveyForm.Models
{
    [Keyless]
    public class Result
    {
        public string Unit { get; set; }
        public string Sex { get; set; }
        public string PersnnelNumber { get; set; }
        public string Age { get; set; }
        public string Position { get; set; }
        public string Education { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
    }
    public class SimpleReportViewModel
    {
        public string DimensionOne { get; set; }
        public int Quantity { get; set; }
    }
    public class StackedViewModel
    {
        public string StackedDimensionOne { get; set; }
        public List<SimpleReportViewModel> LstData { get; set; }
    }

}
