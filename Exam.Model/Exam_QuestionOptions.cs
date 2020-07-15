using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    class Exam_QuestionOptions
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionDescribe { get; set; }
        public string OptionCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
      

    }
}
