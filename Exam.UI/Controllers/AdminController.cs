using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.UI.Filter;
using Utility;
namespace Exam.UI.Controllers
{
    [StudentFilter]
    public class AdminController : Controller
    {
        // GET: Admin
        
        public ActionResult Index()
        {              
            return View();
        }
        public ActionResult Main()
        {
            return View();
        }
    }
}