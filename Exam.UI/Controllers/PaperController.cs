using Exam.BLL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.UI.Controllers
{
    public class PaperController : Controller
    {
        // GET: Paper
        public ActionResult Index(int page=1)
        {
            IPagedList list = PaperRuleService.GetList(page);
            return View();
        }
    }
}