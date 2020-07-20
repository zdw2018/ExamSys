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
        public ActionResult Delete(int id)              
        {

            try
            {
                int res = StudentMannerService.RemoveStudent(id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "删除失败" + ex, success = false });

            }
            return Json(new { msg = "删除成功", success = true });

          
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
                //学生账号默认密码123456
                PassWord = PassWordHelper.GetMD5("123456"),
                RealName = RealName,
                Phone = Phone,
                CreateName = "admin",
                States =true,
                 UserName=UserName, UserType=0
            };
            try
            {
                int res = StudentMannerService.InsertStudent(u);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "添加失败"+ex, success = false });

            }
            return Json(new { msg = "添加成功", success = true });   
        }
        public ActionResult Edit(int id)
        {
         var data= StudentMannerService.FindStudentByID(id);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(string uname, string Name, string phone,string id)
        {
            Exam_User u = new Exam_User { UserID= System.Convert.ToInt32(id), RealName= Name, UserName= uname, Phone = phone };
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
    }
}