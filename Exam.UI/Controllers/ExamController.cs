using Exam.BLL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.UI.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index(int page = 1)
        {
            IPagedList list = PaperRuleService.GetList(page);
            return View(list);
        }
        public ActionResult BeginExam(int ruleid)
        {
            ViewBag.Rule = PaperRuleService.FindPaperRuleByID(ruleid);
            var list = ExamPaperService.GeneratePaper(ruleid, 2);
            return View(list);
        }
        public ActionResult ExamDetail(int ruleid)
        {
            return View();
        }
    }
}