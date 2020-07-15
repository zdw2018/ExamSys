using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    /// <summary>
    /// 试卷信息表
    /// </summary>
    public class Exam_Paper
    {
        /// <summary>
        /// 试卷编号
        /// </summary>
        public int PaperID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 试卷规则编号
        /// </summary>
        public int RuleID { get; set; }
        /// <summary>
        /// 试卷总分
        /// </summary>
        public int TotalScore { get; set; }
        /// <summary>
        /// 考试分数
        /// </summary>
        public int UserScore { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
      
    }
}
