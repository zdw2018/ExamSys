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
    public class PaperRuleController : Controller
    {
        // GET: PaperRule
        public ActionResult Index(int page = 1)
        {
            IPagedList list = PaperRuleService.GetList(page);
            return View(list);
        }
        public ActionResult DeleteRuleDetail(int id)
        {
            try
            {
                RuleDetailService.Delete(id);
            }
            catch (Exception ex)
            {

                return Json(new { msg = "删除失败" + ex, success = false });
            }
            return Json(new { msg = "删除成功", success = false });
        }
        public ActionResult AddPaper()
        {
            return View();
        }
        public ActionResult RuleDetail(int id)
        {
            ViewBag.Paper =PaperRuleService.FindPaperRuleByID(id);
            ViewData["Num"] = RuleDetailService.GetDetailQuestionCount(id).ToString();
           
            var list = RuleDetailService.GetList(id);
            return View(list);
        }
        /// <summary>
        /// 编辑试卷规则详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditRuleDetail(int id)
        {
            var list = LibraryService.GetAll();
            ViewBag.data = RuleDetailService.GetDetailByID(id);
            return View(list);
        }
        [HttpPost]
        public ActionResult EditRuleDetail(int questionnum, int libraryid, int ruleid,int paperruleid,int oldnum)
        {
            try
            {
                ///查询规则详情中 试卷题目数量
                int num = RuleDetailService.GetDetailQuestionCount(paperruleid);
                //查询试卷规则 题目总数

                var data = PaperRuleService.FindPaperRuleByID(paperruleid);
                if (questionnum > data.QuestionNum - num+ oldnum)
                {
                    return Json(new { msg = "修改失败,要添加的题目数量大于试卷题目总数", success = false });
                }
                else
                {
                    Exam_RuleDetail detail = new Exam_RuleDetail { QuestionNum = questionnum, LibraryID = libraryid, PaperRuleID = paperruleid, RuleID=ruleid };
                    RuleDetailService.Update(detail);
                }
            }
            catch (Exception ex)
            {
                return Json(new { msg = "修改失败" + ex, success = false });

            }
            return Json(new { msg = "修改成功", success = false });
        }

        [HttpPost]
        public ActionResult AddPaper(string rulename, string rulestarttime, int time, int Score, int questionnum)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(rulestarttime);
                Exam_PaperRule paperRule = new Exam_PaperRule
                {
                    QuestionNum = questionnum,
                    RuleStartDate = dt,
                    RuleEndDate = dt.AddMinutes(time),
                    RuleName = rulename,
                    Score = Score,
                    States = true
                };
                PaperRuleService.InsertPaperRule(paperRule);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "添加失败" + ex, success = false });

            }
            return Json(new { msg = "添加成功", success = true });
        }
        /// <summary>
        /// 编辑试卷规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditPaper(int id)
        {
            var list = PaperRuleService.FindPaperRuleByID(id);
            return View(list);
        }
        [HttpPost]
        public ActionResult EditPaper(int id,string rulename, string rulestarttime, int time, int Score, int questionnum)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(rulestarttime);
                Exam_PaperRule paperRule = new Exam_PaperRule
                {
                    QuestionNum = questionnum,
                    RuleStartDate = dt,
                    RuleEndDate = dt.AddMinutes(time),
                    RuleName = rulename,
                    Score = Score,
                    States = true,
                    PaperRuleID=id
                };
                PaperRuleService.Update(paperRule);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "添加失败" + ex, success = false });

            }
            return Json(new { msg = "添加成功", success = true });
        }
        public ActionResult AddPaperRule()
        {

            ViewBag.Library = LibraryService.GetAll();
            var list = PaperRuleService.GetAll();
            
            return View(list);
        }
        [HttpPost]
        public ActionResult AddPaperRule(int questionnum, int libraryid, int paperruleid)
        {
            try
            {
                ///查询规则详情中 试卷题目数量
                int num = RuleDetailService.GetDetailQuestionCount(paperruleid);
                //查询试卷规则 题目总数

                var data = PaperRuleService.FindPaperRuleByID(paperruleid);
                if (questionnum > data.QuestionNum - num)
                {
                    return Json(new { msg = "添加失败,要添加的题目数量大于试卷题目总数", success = false });
                }
                else
                {
                    Exam_RuleDetail detail = new Exam_RuleDetail { QuestionNum = questionnum, LibraryID = libraryid, PaperRuleID = paperruleid };
                    RuleDetailService.AddRuleDetail(detail);
                }
            }
            catch (Exception ex)
            {
                return Json(new { msg = "添加失败" + ex, success = false });

            }
            return Json(new { msg = "添加成功", success = false });
        }

        /// <summary>
        /// 禁用试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EnablePaper(int id)
        {
            try
            {
                int res = PaperRuleService.EnablePaperRule(id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "启用失败" + ex, success = false });

            }
            return Json(new { msg = "启用成功", success = true });
        }
        /// <summary>
        /// 禁用试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DisablePaper(int id)
        {
            try
            {
                int res = PaperRuleService.DisablePaperRule(id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "禁用失败" + ex, success = false });

            }
            return Json(new { msg = "禁用成功", success = true });
        }
    }
}