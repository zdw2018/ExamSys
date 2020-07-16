using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Exam.Model;

namespace Exam.UI.Filter
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session[CommonFeild.SessionName] != null)
            {
                Exam_User user = session[CommonFeild.SessionName] as Exam_User;
                if (user.UserType == 0)
                {
                    filterContext.Result = new RedirectResult("~/Admin/Index");
                }
                else if (user.UserType == 1)
                {
                    filterContext.Result = new RedirectResult("~/Exam/Index");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}