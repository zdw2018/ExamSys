using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exam.DAL;
using Exam.Model;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Exam.BLL
{
  public class QuestionOptionsService
    {
        /// <summary>
        /// 批量添加试题
        /// </summary>
        /// <param name="lists"></param>
        public static void AddOptions(List<Exam_QuestionOptions> lists)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            db.Exam_QuestionOptions.AddRange(lists);
            db.SaveChanges();
        }
    }
}
