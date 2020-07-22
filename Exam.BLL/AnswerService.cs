using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Model;
using Exam.DAL;


namespace Exam.BLL
{
    public class AnswerService
    {
        /// <summary>
        /// 获取用户答题信息表中的题目数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserQuestionCount(int userid, int paperid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                int count = db.Exam_Answer.Where(x => x.UserID == userid && x.PaperID == paperid).Count();
                return count;
            }
        }

        /// <summary>
        /// 加载答题卡信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="paperid"></param>
        /// <returns></returns>
        public static List<Exam_Answer> GetAnswer(int userid, int paperid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                return db.Exam_Answer.Where(x => x.UserID == userid && x.PaperID == paperid).ToList();
            }

        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="paperid"></param>
        public static void Clear(int userid, int paperid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var list = db.Exam_Answer.Where(x => x.UserID == userid && x.PaperID == paperid);
                db.Exam_Answer.RemoveRange(list);
                db.SaveChanges();
            }
        }

    }
}
