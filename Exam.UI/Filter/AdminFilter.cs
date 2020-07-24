using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Exam.Model;

namespace Exam.UI.Filter
{
    public class AdminFilter : AuthorizeAttribute
    {


        /// <summary>
        /// 用户授权，验证用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //授权验证 验证Login 有没有AllowAnonymousAttribute 特性
            //if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            //{
            //    return;
            //}

            var session = filterContext.HttpContext.Session;
            if (session[CommonFeild.SessionName] != null)
            {
                Exam_User user = filterContext.HttpContext.Session[CommonFeild.SessionName] as Exam_User;
                //如果是老师进入相关页面 返回到学生的主页面
                if (user.UserType == 1)
                {
                    filterContext.Result = new RedirectResult("~/Admin/Index");
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