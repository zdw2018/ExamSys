using Exam.BLL;
using Exam.UI.Filter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Model;

namespace Exam.UI.Controllers
{
    //[StudentFilter]
    public class ExamMannageController : Controller
    {
        // GET: ExamMannage
        public ActionResult Index(int page = 1)
        {
            IPagedList list = PaperRuleService.GetList(page);
            return View(list);

        }
        /// <summary>
        /// 统计分数
        /// </summary>
        /// <param name="ruleid"></param>
        /// <returns></returns>
        public ActionResult Totle(int ruleid)
        {
            Exam_PaperRule rule = PaperRuleService.FindPaperRuleByID(ruleid);
            ViewBag.Info = rule;

            ScoreTotleModel score = ExamPaperService.GetScoreModel(ruleid);
            return View(score);
        }
        ///// <summary>
        ///// 错题统计
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult ErrorQuestion(int ruleid)
        //{

        //    return View();
        //}
    }
}