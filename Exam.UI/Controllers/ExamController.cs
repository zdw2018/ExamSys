using Exam.BLL;
using Exam.Model;
using Exam.UI.Filter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace Exam.UI.Controllers
{
    [AdminFilter]
    public class ExamController : Controller
    {
        private static Dictionary<int, List<Exam_Answer>> listanswer = new Dictionary<int, List<Exam_Answer>>();
        //private static List<Exam_Answer> listanswer = new List<Exam_Answer>();
        //private static List<Exam_Answer> Newlistanswer = new List<Exam_Answer>();
        // GET: Exam
        public ActionResult Index(int page = 1)
        {
            IPagedList list = PaperRuleService.GetListEnable(page);
            return View(list);
        }
        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="ruleid"></param>
        /// <returns></returns>
        public ActionResult BeginExam(int ruleid)
        {
           

            var currentuser = Session[CommonFeild.SessionName] as Exam_User;
            ViewBag.Rule = PaperRuleService.FindPaperRuleByID(ruleid);
            var list = ExamPaperService.GeneratePaper(ruleid, currentuser.UserID);
            listanswer.Add(currentuser.UserID, list);
            //获取questioID数组
            List<Exam_Question> questionlist = new List<Exam_Question>();
            List<ExamPaperBLL> paperbll = new List<ExamPaperBLL>();
            foreach (var item in listanswer[currentuser.UserID])
            {
                ExamPaperBLL examPaperBLL = new ExamPaperBLL();
                examPaperBLL.Exam_Question = QuestionService.GetdataByID(item.QuestionID);
                examPaperBLL.AnswerOptionID = item.AnswerOptionID;
                paperbll.Add(examPaperBLL);
                // questionlist.Add(QuestionService.GetdataByID(item.QuestionID));
            }
            return View(paperbll);
        }
        /// <summary>
        /// 单选题
        /// </summary>
        /// <param name="data"></param>
        public void GetRadioData(string data)
        {
            var currentuser = Session[CommonFeild.SessionName] as Exam_User;
            string[] arry = data.Split(',');
            string optionid = arry[0];
            int questionid = Convert.ToInt32(arry[1]);
            //防止并发
            lock (this)
            {
                var answer = listanswer[currentuser.UserID].Where(x => x.QuestionID == questionid).FirstOrDefault();
                answer.AnswerOptionID = optionid;
            }

        }
        /// <summary>
        /// 多选题
        /// </summary>
        /// <param name="data"></param>
        /// <param name="check"></param>
        public void GetChechData(string data, bool check)
        {
            var currentuser = Session[CommonFeild.SessionName] as Exam_User;
            string[] arry = data.Split(',');
            string optionid = arry[0];
            int questionid = Convert.ToInt32(arry[1]);
            //判断是否是选中 或者取消
            if (check)
            {
                //防止并发
                lock (this)
                {
                    var answer = listanswer[currentuser.UserID].Where(x => x.QuestionID == questionid).FirstOrDefault();
                    answer.AnswerOptionID += optionid + ",";

                }
            }
            else
            {
                //防止并发
                lock (this)
                {
                    var answer = listanswer[currentuser.UserID].Where(x => x.QuestionID == questionid).FirstOrDefault();
                    string temp = answer.AnswerOptionID;
                    if (temp.Contains(optionid))
                    {
                        int index = temp.IndexOf(optionid);
                        string newstr = temp.Remove(index, optionid.Length + 1);
                        answer.AnswerOptionID = newstr;
                    }
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save()
        {
            var currentuser = Session[CommonFeild.SessionName] as Exam_User;

            try
            {
                foreach (var item in listanswer[currentuser.UserID])
                {
                    if (item.AnswerOptionID != "")
                    {
                        if (item.AnswerOptionID.EndsWith(","))
                        {
                            string temp = item.AnswerOptionID;
                            item.AnswerOptionID = temp.Remove(temp.Length - 1, 1);
                        }
                    }
                    AnswerService.Update(item);
                }
            }
            catch (Exception ex)
            {

                return Json(new { data = false, msg = "保存失败" + ex });
            }
            return Json(new { data = true, msg = "保存成功" });

        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Conmit()
        {
            var currentuser = Session[CommonFeild.SessionName] as Exam_User;
            int paperid = 0;
            try
            {
                foreach (var item in listanswer[currentuser.UserID])
                {
                    paperid = item.PaperID;
                    if (item.AnswerOptionID != "")
                    {
                        if (item.AnswerOptionID.EndsWith(","))
                        {
                            string temp = item.AnswerOptionID;
                            item.AnswerOptionID = temp.Remove(temp.Length - 1, 1);
                        }
                    }
                    //提交试卷
                    AnswerService.Update(item);
                }
                //更新试卷状态 获取分数
                AnswerService.GetScore(paperid, currentuser.UserID);
            }
            catch (Exception ex)
            {

                return Json(new { data = false, msg = "提交失败" + ex });
            }
            return Json(new { data = true, msg = "提交成功" });
        }
        /// <summary>
        /// 试卷详情
        /// </summary>
        /// <param name="PaperID">试卷编号</param>
        /// <returns></returns>
        public ActionResult ExamDetail(int ruleid)
        {
            var currentuser = Session[CommonFeild.SessionName] as Exam_User;

            //获取考试信息
            var paper = ExamPaperService.CheckPaper(ruleid,currentuser.UserID);

            ViewBag.Rule = PaperRuleService.FindPaperRuleByID(paper.RuleID);
            ViewBag.Score = paper.UserScore;
            //获取答题信息 
            List<Exam_Answer> list = AnswerService.GetAnswer(currentuser.UserID,paper.PaperID);
            //加载试卷模型
            List<ExamPaperBLL> paperbll = new List<ExamPaperBLL>();
            foreach (var item in list)
            {
                ExamPaperBLL examPaperBLL = new ExamPaperBLL();
                examPaperBLL.Exam_Question = QuestionService.GetdataByID(item.QuestionID);
                examPaperBLL.AnswerOptionID = item.AnswerOptionID;
                paperbll.Add(examPaperBLL);
               
            }
            return View(paperbll);
        }
    }
}