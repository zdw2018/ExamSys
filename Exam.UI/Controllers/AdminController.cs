using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.UI.Filter;
using Utility;
namespace Exam.UI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[StudentFilter]
        public ActionResult Index()
        {

            //var s = SessionHelper.GetSession(CommonFeild.SessionName);
            //var s = Session[CommonFeild.SessionName];
       
            return View();
        }
        public ActionResult Main()
        {
            return View();
        }
    }
}