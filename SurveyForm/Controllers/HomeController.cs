using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.Sexes = GetSexes();
            model.Ages = GetAges();
            model.WorkExperiences = GetWorkExperiences();
            model.Educations = GetEducations();
            model.OrganisionalPositions = GetOrganizationalPositions();
            model.MatrixValues = GetMatrixValues();
            model.MultipleChoiceQuestions = GetMultipleChoiceQuestions();

            return View(model);
        }

        private List<MatrixTable> GetMatrixValues()
        {
            return _context.MatrixTable.ToList();
        }

        private List<MultipleChoiseQuestion> GetMultipleChoiceQuestions()
        {
            return _context.MultipleChoiseQuestions.ToList();
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
