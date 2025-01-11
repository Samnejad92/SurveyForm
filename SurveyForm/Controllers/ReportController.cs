using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using SurveyForm.Data;
using SurveyForm.Models;
using System.Dynamic;

namespace SurveyForm.Controllers
{
    public class ReportController : Controller
    {
        private readonly SurveyFormContext _viewContext;
        public ReportController(ILogger<HomeController> logger, SurveyFormContext viewContext)
        {
            _viewContext = viewContext;
        }
        public IActionResult Report()
        {
            dynamic model = new ExpandoObject();
            model.Total = GetTotal();
            model.GroupByUnit = GetGroupByUnit();
            model.GroupBySex = GetGroupBySex();
            model.GroupByWorkExperience = GetGroupByWorkExperience();

            //var groupByUnit1 = from v in _viewContext.ViewResults
            //                   where v.Unit == "IT"
            //                   select new
            //                   {
            //                       v.Unit,
            //                       v.Sex,
            //                       v.PersonnelNumber,
            //                       v.WorkExperiences,
            //                       v.Age,
            //                       v.Position,
            //                       v.Education,
            //                       v.Answer,
            //                       v.Question
            //                   };

            return View(model);
        }
        private List<ViewResults> GetTotal()
        {
            return _viewContext.ViewResults.ToList();
        }
        private List<ViewResults> GetGroupByUnit()
        {
            var groupByUnit = from v in _viewContext.ViewResults
                              where v.Unit == "IT"
                              select v;
            List<SurveyForm.Models.ViewResults> groupByUnitList = groupByUnit.Select(m => new SurveyForm.Models.ViewResults { PersonnelNumber = m.PersonnelNumber }).Distinct().ToList();

            return groupByUnitList;
        }
        private List<ViewResults> GetGroupBySex()
        {
            var groupBySex = from v in _viewContext.ViewResults
                             where v.Sex == "Male"
                             select v;
            List<SurveyForm.Models.ViewResults> groupBySexList = groupBySex.Select(m => new SurveyForm.Models.ViewResults { PersonnelNumber = m.PersonnelNumber }).Distinct().ToList();

            return groupBySexList;
        }
        private List<ViewResults> GetGroupByWorkExperience()
        {

            var groupByWorkExperience = from v in _viewContext.ViewResults
                                        where v.WeID == 2
                                        select v;
            List<SurveyForm.Models.ViewResults> groupByWorkExperienceList = groupByWorkExperience.Select(m => new SurveyForm.Models.ViewResults { PersonnelNumber = m.PersonnelNumber }).Distinct().ToList();

            return groupByWorkExperienceList;
        }

        public IActionResult Bar()
        {
            Random rnd = new Random();
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Technology",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Sales",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Marketing",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Human Resource",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Research and Development",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Acconting",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Support",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Logistics",
                Quantity = rnd.Next(10)
            });
            return View(lstModel);
        }
        public IActionResult Pie()
        {
            Random rnd = new Random();
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Beer",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Wine",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Whisky",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Water",
                Quantity = rnd.Next(10)
            });
            return View(lstModel);
        }
        public IActionResult Line()
        {
            Random rnd = new Random();
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Brazil",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "USA",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Portugal",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Russia",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Ireland",
                Quantity = rnd.Next(10)
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Germany",
                Quantity = rnd.Next(10)
            });
            return View(lstModel);
        }
        public IActionResult Stacked()
        {
            Random rnd = new Random();
            var lstModel = new List<StackedViewModel>();
            var count = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "1" select t).Count();
            lstModel.Add(new StackedViewModel
                {
                    StackedDimensionOne = "IT",
                    LstData = new List<SimpleReportViewModel>()
                   {
                       new SimpleReportViewModel()
                    {
                            DimensionOne = "کاملا مخالفم",
                           Quantity = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "1" select t).Count()
                    },
                       new SimpleReportViewModel()
                    {
                            DimensionOne = "مخالفم",
                           Quantity = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "2" select t).Count()
                       },
                       new SimpleReportViewModel()
                    {
                            DimensionOne = "تا حدودی مخالفم",
                           Quantity = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "3" select t).Count()
                   },
                   new SimpleReportViewModel()
                   {
                       DimensionOne="تا حدودی موافقم",
                       Quantity = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "4" select t).Count()
                   },
                   new SimpleReportViewModel()
                   {
                       DimensionOne="موافقم",
                       Quantity = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "5" select t).Count()
                   },
                   new SimpleReportViewModel()
                   {
                       DimensionOne="کاملا موافقم",
                       Quantity = (from t in _viewContext.ViewResults where t.Unit == "IT" && t.ansVAL == "6" select t).Count()
                   }
    }
            });
            lstModel.Add(new StackedViewModel
            {
                StackedDimensionOne = "FI",
                LstData = new List<SimpleReportViewModel>()
                   {
                       new SimpleReportViewModel()
                    {
                            DimensionOne = "کاملا مخالفم",
                           Quantity = (from t in _viewContext.ViewResults where t.Unit == "FI" && t.ansVAL == "1" select t).Count()
                    },
                       new SimpleReportViewModel()
                    {
                            DimensionOne = "مخالفم",
                           Quantity = (from t in _viewContext.ViewResults where t.Unit == "FI" && t.ansVAL == "2" select t).Count()
                       },
                       new SimpleReportViewModel()
                    {
                            DimensionOne = "تا حدودی مخالفم",
                           Quantity = (from t in _viewContext.ViewResults where t.Unit == "FI" && t.ansVAL == "3" select t).Count()
                   },
                   new SimpleReportViewModel()
                   {
                       DimensionOne="تا حدودی موافقم",
                       Quantity = (from t in _viewContext.ViewResults where t.Unit == "FI" && t.ansVAL == "4" select t).Count()
                   },
                   new SimpleReportViewModel()
                   {
                       DimensionOne="موافقم",
                       Quantity = (from t in _viewContext.ViewResults where t.Unit == "FI" && t.ansVAL == "5" select t).Count()
                   },
                   new SimpleReportViewModel()
                   {
                       DimensionOne="کاملا موافقم",
                       Quantity = (from t in _viewContext.ViewResults where t.Unit == "FI" && t.ansVAL == "6" select t).Count()
                   }
    }
            });
            //lstModel.Add(new StackedViewModel
            //{
            //    StackedDimensionOne = "Third Quarter",
            //    LstData = new List<SimpleReportViewModel>()
            //   {
            //       new SimpleReportViewModel()
            //       {
            //           DimensionOne="TV",
            //           Quantity = rnd.Next(10)
            //       },
            //       new SimpleReportViewModel()
            //       {
            //           DimensionOne="Games",
            //           Quantity = rnd.Next(10)
            //       },
            //       new SimpleReportViewModel()
            //       {
            //           DimensionOne="Books",
            //           Quantity = rnd.Next(10)
            //       }
            //   }
            //});
            //lstModel.Add(new StackedViewModel
            //{
            //    StackedDimensionOne = "Fourth Quarter",
            //    LstData = new List<SimpleReportViewModel>()
            //   {
            //       new SimpleReportViewModel()
            //       {
            //           DimensionOne="TV",
            //           Quantity = rnd.Next(10)
            //       },
            //       new SimpleReportViewModel()
            //       {
            //           DimensionOne="Games",
            //           Quantity = rnd.Next(10)
            //       },
            //       new SimpleReportViewModel()
            //       {
            //           DimensionOne="Books",
            //           Quantity = rnd.Next(10)
            //       }
            //   }
            //});
            return View(lstModel);
        }
    }
}
