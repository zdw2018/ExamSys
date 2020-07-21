using Exam.BLL;
using Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.UI.Controllers
{
    public class QuestionController : Controller
    {
        public ActionResult Index(int id = 0, int page = 1)
        {
            ViewBag.ID = id;
            var list = QuestionService.GetList(id, page);
            return View(list);
        }
       
        // GET: Question
        [HttpPost]
        public ActionResult Index(string questionname = "", int page = 1)
        {   
            var list = QuestionService.GetList(questionname, page);
            return View(list);
        }
        /// <summary>
        /// 删除题目   会将挂题目的选项删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteQuestion(int id)
        {
            try
            {
                int res = QuestionService.Delete(id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "删除失败" + ex, success = false });

            }
            return Json(new { msg = "删除成功", success = true });
        }

        public ActionResult Edit(int questionid)
        {
            //获取试题信息
            var data = QuestionService.GetdataByID(questionid);
            //获取题库信息
            ViewBag.Lib = LibraryService.GetAll();
            //获取选项信息
            ViewBag.option = QuestionOptionsService.GetOptions(questionid);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int questionid, int score, string questionparse, string questionanswer, int libraryid, string questiondescribe)
        {
            try
            {
                Exam_Question question = new Exam_Question
                {
                    LibraryID = libraryid,
                    QuestionAnswer = questionanswer,
                    QuestionDescribe = questiondescribe,
                    QuestionID = questionid,
                    QuestionParse = questionparse,
                    Score = score
                };
                QuestionService.Update(question);
            }
            catch (Exception ex)
            {

                return Json(new { msg = "修改失败" + ex, success = false });
            }

            return Json(new { msg = "修改成功", success = true });

        }
    }
}