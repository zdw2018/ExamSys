using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Model;
using Exam.DAL;
using PagedList;

namespace Exam.BLL
{
    public class PaperRuleService
    {
        /// <summary>
        /// 获取所有试卷
        /// </summary>
        /// <param name="lmid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IPagedList GetList(int page = 1)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                int pagesize = 10;
                IPagedList list = db.Exam_PaperRule.OrderBy(x => x.PaperRuleID).ToPagedList(page, pagesize);
                return list;
            }

        }

        /// <summary>
        /// 获取所有试卷 状态正常的
        /// </summary>
        /// <param name="lmid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IPagedList GetListEnable(int page = 1)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                int pagesize = 10;
                IPagedList list = db.Exam_PaperRule.Where(x=>x.States==true).OrderBy(x => x.PaperRuleID).ToPagedList(page, pagesize);
                return list;
            }

        }
        public static List<Exam_PaperRule> GetAll()
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var list = db.Exam_PaperRule.ToList();
                return list;
            }

        }
        /// <summary>
        /// 增加试卷
        /// </summary>
        /// <param name="library"></param>
        /// <returns></returns>
        public static int InsertPaperRule(Exam_PaperRule paperRule)
        {
            using (ExamSysDBContext dBContext = new ExamSysDBContext())
            {
                dBContext.Exam_PaperRule.Add(paperRule);
                return dBContext.SaveChanges();
            }

        }

        /// <summary>
        /// 通过ID找到该试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Exam_PaperRule FindPaperRuleByID(int id)
        {
            using (ExamSysDBContext dBContext = new ExamSysDBContext())
            {
                var data = dBContext.Exam_PaperRule.Where(x => x.PaperRuleID == id).FirstOrDefault();
                return data;
            }

        }
        /// <summary>
        /// 禁用试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DisablePaperRule(int id)
        {
            using (ExamSysDBContext dBContext = new ExamSysDBContext())
            {
                var data = dBContext.Exam_PaperRule.Where(x => x.PaperRuleID == id).FirstOrDefault();

                data.States = false;
                return dBContext.SaveChanges();
            }
        }
        /// <summary>
        /// 启用试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int EnablePaperRule(int id)
        {
            using (ExamSysDBContext dBContext = new ExamSysDBContext())
            {

                var data = dBContext.Exam_PaperRule.Where(x => x.PaperRuleID == id).FirstOrDefault();

                data.States = true;
                return dBContext.SaveChanges();
            }

        }
        /// <summary>
        /// 修改试卷信息
        /// </summary>
        /// <param name="library"></param>
        /// <returns></returns>
        public static int Update(Exam_PaperRule paperRule)
        {
            using (ExamSysDBContext dBContext = new ExamSysDBContext())
            {

                var data = dBContext.Exam_PaperRule.Where(x => x.PaperRuleID == paperRule.PaperRuleID).FirstOrDefault();
                data.RuleName = paperRule.RuleName;
                data.RuleStartDate = paperRule.RuleStartDate;
                data.RuleEndDate = paperRule.RuleEndDate;
                data.Score = paperRule.Score;
                data.QuestionNum = paperRule.QuestionNum;

                return dBContext.SaveChanges();
            }
        }
    }
}
