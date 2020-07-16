using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using Exam.Model;
using System.Web.Mvc;

namespace Exam.UI.Filter
{
    public class StudentFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //授权验证 验证Login 有没有AllowAnonymousAttribute 特性
            //if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            //{
            //    return;
            //}

            var session = filterContext.HttpContext.Session;
            if (session["LoginInfo"] != null)
            {
                Exam_User user = session[CommonFeild.SessionName] as Exam_User;
                //如果是学生进入相关页面 返回到学生的主页面
                if (user.UserType == 0)
                {
                    filterContext.Result = new RedirectResult("~/Exam/Index");
                }

            }
            //如果没有授权信息 跳转到登录页面
            else
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

        }
    }
}