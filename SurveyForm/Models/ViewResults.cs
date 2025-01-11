using System;
using System.Collections.Generic;

namespace SurveyForm.Models;

public partial class ViewResults
{
    public string Unit { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public int PersonnelNumber { get; set; }

    public string? WorkExperiences { get; set; }

    public string? Age { get; set; }

    public string Position { get; set; } = null!;

    public string Education { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public string Question { get; set; } = null!;
    public int WeID { get; set; }
    public string ansVAL { get; set; }
}
