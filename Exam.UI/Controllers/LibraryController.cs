using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.BLL;
using Exam.Model;

namespace Exam.UI.Controllers
{
    public class LibraryController : Controller
    {
        // GET: Library
        public ActionResult Index(int page = 1)
        {
            IPagedList list = LibraryService.GetList(page);
            return View(list);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string libraryname, string libraryremark)
        {
            Exam_Library library = new Exam_Library()
            {
                CreatTime = DateTime.Now,
                UpdateTime=DateTime.Now,
                Library_Remark = libraryremark,
                Library_Name = libraryname,
                 LibraryStates=true                  
            };
            try
            {
                int res = LibraryService.InsertLibrary(library);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "添加失败" + ex, success = false });

            }
            return Json(new { msg = "添加成功", success = true });
        }
        public ActionResult Edit(int id)
        {
            var data = LibraryService.FindLibraryByID(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(string libraryname, int id, string libraryremark)
        {
            Exam_Library library = new Exam_Library { Library_Name = libraryname, LibraryID = id, Library_Remark = libraryremark, UpdateTime = DateTime.Now };
            try
            {
                LibraryService.Update(library);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "修改失败" + ex, success = false });

            }
            return Json(new { msg = "修改成功", success = true });

        }
        /// <summary>
        /// 禁用题库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Disable(int id)
        {
            try
            {
                int res = LibraryService.DisableLibrary(id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "删除失败" + ex, success = false });

            }
            return Json(new { msg = "删除成功", success = true });

        }
    }
}