using Exam.BLL;
using Exam.Model;
using Exam.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.UI.Controllers
{
    [StudentFilter]
    public class QuestionOptionController : Controller
    {
        // GET: QuestionOption    
        public static int questiuonid = 0;
        public ActionResult Index(int id, int page = 1)
        {
            questiuonid = id;
            var list = QuestionOptionsService.GetList(id, page);
            return View(list);
        }

        public ActionResult Edit(int optionid)
        {
            ViewBag.Questionid = questiuonid;
            ViewBag.Optionid = optionid;
            var data = QuestionOptionsService.GetOption(optionid);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int id, string optioncode, string optiondescribe)
        {
            try
            {
                Exam_QuestionOptions option = new Exam_QuestionOptions
                {
                    OptionCode = optioncode,
                    OptionDescribe = optiondescribe,
                    OptionID = id
                };
                QuestionOptionsService.Update(option);
            }
            catch (Exception ex)
            {

                return Json(new { msg = "修改失败" + ex, success = false });
            }
            return Json(new { msg = "修改成功", success = true });
        }

        /// <summary>
        /// 删除题目选项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                QuestionOptionsService.Delete(id);
            }
            catch (Exception ex)
            {

                return Json(new { msg = "删除失败" + ex, success = false });
            }
            return Json(new { msg = "删除成功", success = true });
        }
    }
}