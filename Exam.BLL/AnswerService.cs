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
        /// 获取用户错题
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<Exam_Answer> GetError(int userid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var data = db.Exam_Answer.Where(x => x.UserID == userid && x.AnswerOptionID != "" && x.AnswerOptionID != x.OptionID).ToList();
                return data;
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

        /// <summary>
        /// 保存答题内容
        /// </summary>
        /// <param name="answer"></param>
        public static void Update(Exam_Answer answer)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var data = db.Exam_Answer.Where(x => x.AnswerID == answer.AnswerID).FirstOrDefault();
                data.AnswerOptionID = answer.AnswerOptionID;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 评分
        /// </summary>
        /// <param name="paperID"></param>
        public static void GetScore(int paperID, int userid)
        {
            int Score = 0;
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                //更新试卷状态为已提交
                var data = db.Exam_Paper.Where(x => x.PaperID == paperID).FirstOrDefault();
                data.States = true;
                var list = db.Exam_Answer.Where(x => x.PaperID == paperID && x.UserID == userid).ToList();

                foreach (var item in list)
                {
                    if (item.OptionID == item.AnswerOptionID)
                    {
                        Score += QuestionService.GetScore(item.QuestionID);
                    }
                }
            }
            //将得分写入试卷表
            ExamPaperService.UpdateScore(paperID, Score);
        }

    }
}
