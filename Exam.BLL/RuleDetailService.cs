using Exam.DAL;
using Exam.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BLL
{
    public class RuleDetailService
    {
        /// <summary>
        /// 获取当前规则试题数量总和
        /// </summary>
        /// <param name="ruleid"></param>
        /// <returns></returns>
        public static int GetDetailQuestionCount(int ruleid)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            int num = db.Exam_RuleDetail.Where(x => x.PaperRuleID == ruleid).Sum(x => x.QuestionNum);

            return num;
        }
        /// <summary>
        /// 添加规则详情
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static int AddRuleDetail(Exam_RuleDetail detail)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            db.Exam_RuleDetail.Add(detail);
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除规则详情
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static int Delete(int detailid)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            var detai= db.Exam_RuleDetail.Where(x => x.RuleID == detailid).FirstOrDefault();
            db.Exam_RuleDetail.Remove(detai);
            return db.SaveChanges();
        }
        /// <summary>
        /// 更新规则详情信息
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static int Update(Exam_RuleDetail detail)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            var detai = db.Exam_RuleDetail.Where(x => x.RuleID == detail.RuleID).FirstOrDefault();
            detai.LibraryID = detai.LibraryID;
            detai.QuestionNum = detai.QuestionNum;
            return db.SaveChanges();
        }
        public static Exam_RuleDetail GetDetailByID(int id)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            var data = db.Exam_RuleDetail.Where(x => x.RuleID == id).FirstOrDefault();
            return data;
        }
        public static IPagedList GetList(int page = 1)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            int pagesize = 10;
            IPagedList list = db.Exam_RuleDetail.OrderBy(x => x.RuleID).ToPagedList(page, pagesize);
            return list;
        }
    }
}
