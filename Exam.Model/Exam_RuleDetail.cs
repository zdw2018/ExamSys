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
    /// 组卷规则详情表
    /// </summary>
    public class Exam_RuleDetail
    {
        /// <summary>
        /// 组卷规则明细编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RuleID { get; set; }
        public Exam_Library Exam_Library { get; set; }
        [ForeignKey("Exam_Library")]
        /// <summary>
        /// 题库编号
        /// </summary>       
        public int LibraryID { get; set; }
        /// <summary>
        /// 题目数量
        /// </summary>
        public int QuestionNum { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public Exam_PaperRule Exam_PaperRule { get; set; }
        [ForeignKey("Exam_PaperRule")]
        /// <summary>
        /// 试卷规则编号
        /// </summary>
        public int PaperRuleID { get; set; }

    }
}
