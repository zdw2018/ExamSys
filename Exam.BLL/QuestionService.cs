using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exam.Model;
using Exam.DAL;
using System.Threading.Tasks;

namespace Exam.BLL
{
    public class Exam_QuestionService
    {
        public static int Add(Exam_Question question)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            db.Exam_Question.Add(question);
             db.SaveChanges();
            return question.QuestionID;
        }
    }
}
