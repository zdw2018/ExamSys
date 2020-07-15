using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
   public class Exam_User
    {
        public int UserID { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool States { get; set; }
        public string Phone { get; set; }
        public string RealName { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateName { get; set; }
    }
}
