using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exam.Model;
using Exam.DAL;
using System.Threading.Tasks;
using PagedList;

namespace Exam.BLL
{
    public class QuestionService
    {
        /// <summary>
        /// 试题列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>

        public static IPagedList GetList(string questionname, int page)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                int pagesize = 10;
                IPagedList list = db.Exam_Question.Where(x => x.QuestionDescribe.Contains(questionname)).OrderBy(x => x.QuestionID).ToPagedList(page, pagesize);
                return list;
            }

        }
        public static IPagedList GetList(int id, int page)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                int pagesize = 10;
                IPagedList list;
                if (id != 0)
                {
                    list = db.Exam_Question.Where(x => x.LibraryID == id).OrderBy(x => x.QuestionID).ToPagedList(page, pagesize);
                }
                else
                {
                    list = db.Exam_Question.OrderBy(x => x.QuestionID).ToPagedList(page, pagesize);
                }
                return list;
            }
              
        }
        public static Exam_Question GetdataByID(int id)
        {

            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                return db.Exam_Question.Where(x => x.QuestionID == id).FirstOrDefault();
            }
                
        }
        /// <summary>
        /// 添加试题
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static int Add(Exam_Question question)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                db.Exam_Question.Add(question);
                db.SaveChanges();
                return question.QuestionID;
            }
               
        }
        /// <summary>
        /// 更新试题信息
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static int Update(Exam_Question question)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var data = db.Exam_Question.Where(x => x.QuestionID == question.QuestionID).FirstOrDefault();
                data.QuestionAnswer = question.QuestionAnswer;
                data.Score = question.Score;
                data.QuestionParse = question.QuestionParse;
                data.QuestionDescribe = question.QuestionDescribe;
                return db.SaveChanges();
            }
               
        }
        public static int Delete(int id)
        {

            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                //删除当前题目下所有选项
                var list = db.Exam_QuestionOptions.Where(x => x.QuestionID == id).ToList();
                db.Exam_QuestionOptions.RemoveRange(list);
                //删除题目
                var data = db.Exam_Question.Where(x => x.QuestionID == id).FirstOrDefault();
                db.Exam_Question.Remove(data);
                return db.SaveChanges();
            }
                
        }

    }
}
