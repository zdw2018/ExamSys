using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.BLL;
using Exam.Model;
using Utility;
using System.IO;
using System.Data;
using Exam.UI.Filter;

namespace Exam.UI.Controllers
{
    [StudentFilter]
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

        public ActionResult Lead()
        {
            var list = LibraryService.GetAllEnable();
            return View(list);
        }
        [HttpPost]
        public ContentResult Lead(string libraryname, HttpPostedFileBase excelname)
        {
            try
            {
                if (excelname.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(excelname.FileName);

                    var path = Path.Combine(Server.MapPath("~/Content/ExcelTemp"), fileName);
                    excelname.SaveAs(path);
                    DataTable dt = Utility.Converter.ExcelToDataSet(path);
                    foreach (DataRow item in dt.Rows)
                    {

                        ///添加试题信息 拿到ID
                        string title = item["Title"].ToString();
                        string answer = item["RightOption"].ToString();
                        string Analyze = item["Analyze"].ToString();

                        string OptionA = item["OptionA"].ToString();
                        string OptionB = item["OptionB"].ToString();
                        string OptionC = item["OptionC"].ToString();
                        string OptionD = item["OptionD"].ToString();
                        Exam_Question q = new Exam_Question { LibraryID = Convert.ToInt32(libraryname), QuestionAnswer = answer, QuestionDescribe = title, QuestionParse = Analyze, Score = 2 };
                        int id = QuestionService.Add(q);
                        ///拿到ID继续添加选项
                        List<Exam_QuestionOptions> lists =
                            new List<Exam_QuestionOptions>()
                            {

                                new Exam_QuestionOptions{
                                    QuestionID=id,
                                    CreateTime=DateTime.Now,
                                    OptionCode="A",
                                    OptionDescribe=OptionA,
                                    UpdateTime=DateTime.Now
                                },
                                new Exam_QuestionOptions{
                                    QuestionID=id,
                                    CreateTime=DateTime.Now,
                                    OptionCode="B",
                                    OptionDescribe=OptionB,
                                    UpdateTime=DateTime.Now
                                },
                                new Exam_QuestionOptions{
                                    QuestionID=id,
                                    CreateTime=DateTime.Now,
                                    OptionCode="C",
                                    OptionDescribe=OptionC,
                                    UpdateTime=DateTime.Now
                                },
                                new Exam_QuestionOptions{
                                    QuestionID=id,
                                    CreateTime=DateTime.Now,
                                    OptionCode="D",
                                    OptionDescribe=OptionD,
                                    UpdateTime=DateTime.Now
                                },
                            };
                        QuestionOptionsService.AddOptions(lists);
                    }

                }
            }
            catch (Exception ex)
            {


                return Content("<script src='../../Content/Common/plus/jquery-3.2.1.min.js'></script><script src='../../Content/Common/frame/layui/layui.js'></script><script>layui.use('layer', function () {var $ = layui.jquery, layer = layui.layer;layer.msg('添加失败'"+ex+");window.loaction.href='/Library/Index'})</script>");
            }
            return this.Content("<script src='../../Content/Common/plus/jquery-3.2.1.min.js'></script><script src='../../Content/Common/frame/layui/layui.js'></script><script>layui.use('layer', function () {var $ = layui.jquery, layer = layui.layer;layer.msg('添加成功');window.loaction.href='/Library/Index'})</script>");
        }
        [HttpPost]
        public ActionResult Add(string libraryname, string libraryremark)
        {
            Exam_Library library = new Exam_Library()
            {
                CreatTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Library_Remark = libraryremark,
                Library_Name = libraryname,
                LibraryStates = true
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
                return Json(new { msg = "禁用失败" + ex, success = false });

            }
            return Json(new { msg = "禁用成功", success = true });

        }

        /// <summary>
        /// 启用题库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Enable(int id)
        {
            try
            {
                int res = LibraryService.DisableLibrary(id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "启用失败" + ex, success = false });

            }
            return Json(new { msg = "启用成功", success = true });

        }
    }
}