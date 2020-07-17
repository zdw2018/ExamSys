using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Model;
using Exam.BLL;
using PagedList;
using Utility;

namespace Exam.UI.Controllers
{
    public class StudentMannagerController : Controller
    {
        // GET: StudentMannager
        public ActionResult Index(int page = 1)
        {
            IPagedList list = StudentMannerService.GetList(page);
            return View(list);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string UserName, string RealName, string Phone)
        {
            Exam_User u = new Exam_User()
            {
                CreateTime = DateTime.Now,
                PassWord = PassWordHelper.GetMD5("123456"),
                RealName = RealName,
                Phone = Phone,
                CreateName = "admin",
                States =true,
                 UserName=UserName, UserType=0
            };
            try
            {
                int res = UsersService.InsertUser(u);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "添加失败"+ex, success = false });

            }
            return Json(new { msg = "添加成功", success = true });   
        }
    }
}