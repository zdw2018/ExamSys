using Exam.DAL;
using Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BLL
{
    public class ExamPaperService
    {
        /// <summary>
        /// 根据试卷规则编号生成试卷，并将题目生成到答题信息表中
        /// </summary>
        /// <param name="ruleid"></param>
        /// <param name="UserID"></param>
        public static List<Exam_Answer> GeneratePaper(int ruleid, int UserID)
        {
            //定义答题卡
            List<Exam_Answer> AnswerList = new List<Exam_Answer>();
            //定义试卷变量
            List<Exam_Question> QuestionList = new List<Exam_Question>();
            //定义组卷详情变量
            List<Exam_RuleDetail> RuledetailList = RuleDetailService.GetDetailQuestion(ruleid);
            int answercount = 0;
            //查询试卷总题目数量
            int num = PaperRuleService.FindPaperRuleByID(ruleid).QuestionNum;
            //判断试卷信息表是否已经存在,如果不存在需要创建(需要考虑中途退出的同学)
            Exam_Paper paper = CheckPaper(ruleid, UserID);


            if (paper == null)
            {
                paper = CreatePaper(ruleid, UserID);
                //生成答题卡           
            }
            //判断答题卡是否存在信息
            answercount = AnswerService.GetUserQuestionCount(UserID, paper.PaperID);
            //如果存在的话 将试卷信息加载出来
            if (answercount == num)
            {
                var data = AnswerService.GetAnswer(UserID, paper.PaperID);
                AnswerList.AddRange(data);
            }
            ///如果不存在  随机生成试题
            else
            {
                using (ExamSysDBContext db = new ExamSysDBContext())
                {
                    //先将答题卡清空
                    AnswerService.Clear(UserID, paper.PaperID);
                    //根据规则详情 随机生成试题
                    foreach (var item in RuledetailList)
                    {
                        var temp = db.Exam_Question.Where(x => x.LibraryID == item.LibraryID).OrderBy(x => Guid.NewGuid()).Take(item.QuestionNum).ToList();
                        QuestionList.AddRange(temp);
                    }
                    //将试题 添加到答题卡
                    foreach (var question in QuestionList)
                    {

                        Exam_Answer answer = new Exam_Answer
                        {
                            LibraryID = question.LibraryID,
                            PaperID = paper.PaperID,
                            UserID = UserID,
                            QuestionID = question.QuestionID,                           
                            OptionID = QuestionOptionsService.GetOptionID(question.QuestionAnswer, question.QuestionID),
                        };
                                      
                        AnswerList.Add(answer);
                    }
                    //将答题信息添加到数据库
                    db.Exam_Answer.AddRange(AnswerList);
                    db.SaveChanges();
                }
            }
            return AnswerList;
        }
        /// <summary>
        /// 检查试卷是否已经生成，将试卷信息返回
        /// </summary>
        /// <param name="ruleid"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static Exam_Paper CheckPaper(int ruleid, int userid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                return db.Exam_Paper.Where(x => x.RuleID == ruleid && x.UserID == userid).FirstOrDefault();
            }
        }
        /// <summary>
        ///   创建试卷
        /// </summary>
        /// <param name="ruleid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Exam_Paper CreatePaper(int ruleid, int userid)
        {
            //获取试卷规则信息
            var rule = PaperRuleService.FindPaperRuleByID(ruleid);

            //获取用户信息
            var user = UsersService.GetUserByID(userid);
            //添加试卷规则
            Exam_Paper paper = new Exam_Paper { RealName = user.RealName, UserID = user.UserID, RuleID = rule.PaperRuleID, TotalScore = rule.Score, UserScore = 0 };
            var Newpaper = AddPaper(paper);
            return Newpaper;

        }
        /// <summary>
        /// 添加试卷
        /// </summary>
        /// <param name="paper"></param>
        /// <returns></returns>
        public static Exam_Paper AddPaper(Exam_Paper paper)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                db.Exam_Paper.Add(paper);
                db.SaveChanges();
                return paper;
            }
        }

    }
}
