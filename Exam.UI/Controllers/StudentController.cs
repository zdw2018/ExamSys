using Exam.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.BLL;
using Utility;
using Exam.Model;

namespace Exam.UI.Controllers
{
    [AdminFilter]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var name = Session[CommonFeild.SessionName] as Exam_User;

            return View(name);
        }
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult MyInfo()
        {
           var currentuser= Session[CommonFeild.SessionName] as Exam_User;

            var data = StudentMannerService.FindStudentByID(currentuser.UserID);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(string uname, string Name, string phone, string id)
        {
            Exam_User u = new Exam_User { UserID = System.Convert.ToInt32(id), RealName = Name, UserName = uname, Phone = phone };
            try
            {
                StudentMannerService.Update(u);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "修改失败" + ex, success = false });

            }
            return Json(new { msg = "修改成功", success = true });

        }

        public ActionResult MyError()
        {
            var currentuser = Session[CommonFeild.SessionName] as Exam_User;                                   
            //获取答题信息 
            List<Exam_Answer> list = AnswerService.GetError(currentuser.UserID);
            //加载试卷模型
            List<ExamPaperBLL> paperbll = new List<ExamPaperBLL>();
            foreach (var item in list)
            {
                ExamPaperBLL examPaperBLL = new ExamPaperBLL();
                examPaperBLL.Exam_Question = QuestionService.GetdataByID(item.QuestionID);
                examPaperBLL.AnswerOptionID = item.AnswerOptionID;
                paperbll.Add(examPaperBLL);

            }
            return View(paperbll);
        }
    }
}