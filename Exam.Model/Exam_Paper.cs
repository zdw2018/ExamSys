using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 试卷编号
        /// </summary>
        public int PaperID { get; set; }
        [ForeignKey("Exam_User")]
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 试卷规则编号
        /// </summary>
        public int RuleID { get; set; }
        public virtual Exam_PaperRule Exam_PaperRule { get; set; }
        /// <summary>
        /// 试卷总分
        /// </summary>
        public int TotalScore { get; set; }
        /// <summary>
        /// 考试分数
        /// </summary>
        public int UserScore { get; set; }
        [StringLength(8)]
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual Exam_User Exam_User { get; set; }
        /// <summary>
        /// 试卷状态  0 未提交 1 已经提交
        /// </summary>
        public bool States { get; set; }

    }
}
