using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    /// <summary>
    /// 试卷业务模型
    /// </summary>
    public class ExamPaperBLL
    {
     
        //试题选项
        //public List<Exam_QuestionOptions> Exam_QuestionOptions { get; set; }
        //题目信息
        public Exam_Question Exam_Question { get; set; }
        //用户选项
        public string AnswerOptionID { get; set; }
    }
}
