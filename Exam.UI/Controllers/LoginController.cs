using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Utility;
using Exam.Model;
using Exam.BLL;
using Exam.UI.Filter;

namespace Exam.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult UserLogin(string userName, string userPwd, string ipaddress)
        {
            if (ipaddress == "")
            {
                ipaddress = "127.0.0.1";
            }
            bool b = ConfigHelper.SetConfigValue("DBContext", $"server={ipaddress};database=ExamSys;uid=sa;pwd=123456789");
            if (!b)
            {
                return Json(new { msg = "配置文件写入错误", success = false });
            }
            //程序初始化添加超级管理员
            if (UsersService.GetUserNum() == 0)
            {
                Exam_User u = new Exam_User { CreateName = "System", CreateTime = DateTime.Now, PassWord = PassWordHelper.GetMD5("18837473169"), UserName = "admin", Phone = "13787674556", RealName = "张三", States = true, UserType = 1 };
                UsersService.InsertUser(u);
            }
            var res = UsersService.GetUserNum(userName, userPwd);
            if (res != null)
            {
                //写入凭证
                Session.Timeout = 30;
                this.HttpContext.Session[CommonFeild.SessionName] = res;
               
                //登录成功
                return Json(new { Role = res.UserType, msg = "登录成功" + res.UserName, success = true });

            }
            //登录失败

            return Json(new { msg = "登录失败，请检查账号密码", success = false });
        }
    }
}