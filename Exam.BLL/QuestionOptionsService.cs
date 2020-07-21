using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exam.DAL;
using Exam.Model;
using System.Threading.Tasks;
using System.Data.Entity;
using PagedList;
using System.Security.Cryptography.X509Certificates;

namespace Exam.BLL
{
    public class QuestionOptionsService
    {      
        public static IPagedList GetList(int questionid,int page)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                int pagesize = 10;
                IPagedList list;
                if (questionid != 0)
                {
                    list = db.Exam_QuestionOptions.Where(x => x.QuestionID == questionid).OrderBy(x => x.OptionID).ToPagedList(page, pagesize);
                }
                else
                {
                    list = db.Exam_QuestionOptions.OrderBy(x => x.OptionID).ToPagedList(page, pagesize);
                }
                return list;
            }
                
        }
        /// <summary>
        /// 通过选项值 和题目编号获取正确选项编号
        /// </summary>
        /// <param name="optioncode"></param>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public static int GetOptionID(string optioncode,int questionID)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
               var data= db.Exam_QuestionOptions.Where(X=>X.OptionCode== optioncode && X.QuestionID==questionID).FirstOrDefault();
                return data.OptionID;
            }
        }

        /// <summary>
        /// 批量添加试题
        /// </summary>
        /// <param name="lists"></param>
        public static void AddOptions(List<Exam_QuestionOptions> lists)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                db.Exam_QuestionOptions.AddRange(lists);
                db.SaveChanges();
            }
                
        }
        /// <summary>
        /// 通过题目编号查询 下面的选项
        /// </summary>
        /// <param name="questionid"></param>
        /// <returns></returns>
        public static List<Exam_QuestionOptions> GetOptions(int questionid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                return db.Exam_QuestionOptions.Where(x => x.QuestionID == questionid).ToList();
            }
             
        }  
        
        /// <summary>
        /// 根据选项ID获取
        /// </summary>
        /// <param name="optionid"></param>
        /// <returns></returns>
        public static Exam_QuestionOptions GetOption(int optionid)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                return db.Exam_QuestionOptions.Where(x => x.OptionID == optionid).FirstOrDefault();
            }
              
        }
        /// <summary>
        /// 更新选项
        /// </summary>
        /// <returns></returns>
        public static int Update(Exam_QuestionOptions option)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var data = db.Exam_QuestionOptions.Where(x => x.OptionID == option.OptionID).FirstOrDefault();

                data.OptionDescribe = option.OptionDescribe;
                data.OptionCode = option.OptionCode;
                data.UpdateTime = DateTime.Now;
                return db.SaveChanges();
            }
                

        }
        public static int Delete(int id)
        {
            using (ExamSysDBContext db = new ExamSysDBContext())
            {
                var data = db.Exam_QuestionOptions.Where(x => x.OptionID == id).FirstOrDefault();
                db.Exam_QuestionOptions.Remove(data);
                return db.SaveChanges();
            }
                
        }
    }
}
