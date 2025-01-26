using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using SurveyForm.Data;
using SurveyForm.Models;
using System.Diagnostics;
using System.Dynamic;

namespace SurveyForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult SuccessSubmit() { return View(); }
        [HttpPost]
        public IActionResult Index(IFormCollection formCollection,int[] q) {
            string organizationalUnit = Convert.ToString(formCollection["select-unit"]);
            var countEssayQues = _context.EssayQuestions.Count();
            List<string> responses = new List<string>();
            for (int i = 1; i <= countEssayQues; i++)
            {
                string response = formCollection[i.ToString()];
                responses.Add(response);
            }
            string sex = Convert.ToString(formCollection["sex"]);
            var ages = Convert.ToString(formCollection["f[1][]"]);
            var workExperiences = Convert.ToString(formCollection["fo[1][]"]);
            var educations = Convert.ToString(formCollection["foo[1][]"]);
            var positions = Convert.ToString(formCollection["foob[1][]"]);
            var answer = new Answer
            {
                Unit = organizationalUnit, // Adjust based on your question ID logic
                Sex = sex,
                Age = ages,
                WorkExperience = workExperiences,
                Edu = educations,
                Position = positions,
            };
            _context.Answers.Add(answer);
            for (int i = 0; i < q.Length; i++)
            {
                if (q[i] > 0)
                {
                    var newAnswer = new MultipleAnswer
                    {
                        QuestionId = i + 1,
                        QuestionValue = q[i].ToString()
                    };
                    _context.MultipleAnswers.Add(newAnswer);
                }
            }
            if (responses.Count > 0) {
                for (int i = 0; i < responses.Count; i++)
                {
                        var essAnswer = new EssayAnswer
                        {
                            QuestionId = i + 1,
                            QuestionAnswer = responses[i]
                        };
                        _context.EssayAnswers.Add(essAnswer);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("SuccessSubmit");
        }
        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.Sexes = GetSexes();
            model.Ages = GetAges();
            model.WorkExperiences = GetWorkExperiences();
            model.Educations = GetEducations();
            model.OrganisionalPositions = GetOrganizationalPositions();
            model.MatrixValues = GetMatrixValues();
            model.MultipleChoiceQuestions1 = GetMultipleChoiceQuestions1();
            model.MultipleChoiceQuestions2 = GetMultipleChoiceQuestions2();
            model.MultipleChoiceQuestions3 = GetMultipleChoiceQuestions3();
            model.MultipleChoiceQuestions4 = GetMultipleChoiceQuestions4();
            model.MultipleChoiceQuestions5 = GetMultipleChoiceQuestions5();
            model.MultipleChoiceQuestions6 = GetMultipleChoiceQuestions6();
            model.EssayQuestions = GetEssayQuestions();
            model.CodeUnits = GetCodeUnits();

            return View(model);
        }

        private List<CodeUnit> GetCodeUnits()
        {
            return _context.CodeUnits.ToList();
        }

        private List<EssayQuestion> GetEssayQuestions()
        {
            return _context.EssayQuestions.ToList();
        }

        private List<MatrixTable> GetMatrixValues()
        {
            return _context.MatrixTable.OrderByDescending(order => order.Id).ToList();
        }

        private List<MultipleChoiceQuestion> GetMultipleChoiceQuestions1()
        {
            return _context.MultipleChoiceQuestions.Where(a => a.QuestionGroup == 0).ToList();
        }
        private List<MultipleChoiceQuestion> GetMultipleChoiceQuestions2()
        {
            return _context.MultipleChoiceQuestions.Where(a => a.QuestionGroup == 1).ToList();
        }
        private List<MultipleChoiceQuestion> GetMultipleChoiceQuestions3()
        {
            return _context.MultipleChoiceQuestions.Where(a => a.QuestionGroup == 2).ToList();
        }
        private List<MultipleChoiceQuestion> GetMultipleChoiceQuestions4()
        {
            return _context.MultipleChoiceQuestions.Where(a => a.QuestionGroup == 3).ToList();
        }
        private List<MultipleChoiceQuestion> GetMultipleChoiceQuestions5()
        {
            return _context.MultipleChoiceQuestions.Where(a => a.QuestionGroup == 4).ToList();
        }
        private List<MultipleChoiceQuestion> GetMultipleChoiceQuestions6()
        {
            return _context.MultipleChoiceQuestions.Where(a => a.QuestionGroup == 5).ToList();
        }

        private List<Sex> GetSexes()
        {
            return _context.Sexes.ToList();
        }

        private List<WorkExperience> GetWorkExperiences()
        {
            return _context.WorkExperiences.ToList();
        }

        private List<Age> GetAges()
        {
            return _context.Ages.ToList();
        }

        private List<Education> GetEducations()
        {
            return _context.Educations.ToList();
        }

        private List<OrganizationalPosition> GetOrganizationalPositions()
        {
            return _context.OrganizationalPositions.ToList();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        }
}
