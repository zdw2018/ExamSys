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
    /// 试卷规则表
    /// </summary>
    public class Exam_PaperRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        /// <summary>
        /// 试卷规则编号
        /// </summary>
        public int PaperRuleID { get; set; }
        [StringLength(20)]
        /// <summary>
        /// 考试名称
        /// </summary>
        public string RuleName { get; set; }
        /// <summary>
        /// 考试开始时间
        /// </summary>
        public DateTime RuleStartDate { get; set; }
        /// <summary>
        /// 考试结束时间
        /// </summary>
        public DateTime RuleEndDate { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 题目数量
        /// </summary>
        public int QuestionNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool States { get; set; }
    }
}
