using Exam.BLL;
using Exam.Model;
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
            //获取questioID数组
            List<Exam_Question> questionlist = new List<Exam_Question>();
            foreach (var item in list)
            {
                questionlist.Add(QuestionService.GetdataByID(item.QuestionID));
            }
           

            return View(questionlist);
        }
        public ActionResult ExamDetail(int ruleid)
        {
            return View();
        }
    }
}