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
        /// 获取用户答题信息表中的题目
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserQuestionCount(int userid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {

                return db.Exam_Answer.Where(x => x.UserID == userid).Count();
            }
        }
        public static void Get
    }
}
