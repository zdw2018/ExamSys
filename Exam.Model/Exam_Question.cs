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
    /// 试题信息表
    /// </summary>
    public class Exam_Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 题目编号
        /// </summary>
        public int QuestionID { get; set; }
        /// <summary>
        /// 题库编号
        /// </summary>

        public int LibraryID { get; set; }
        [StringLength(200)]
        /// <summary>
        /// 试题描述
        /// </summary>
        public string QuestionDescribe { get; set; }
        [StringLength(10)]
        /// <summary>
        /// 试题答案
        /// </summary>
        public string QuestionAnswer { get; set; }
        [StringLength(200)]
        /// <summary>
        /// 试题解析
        /// </summary>
        public string QuestionParse { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public int Score { get; set; }
        public virtual Exam_Library Exam_Library { get; set; }
        public virtual List<Exam_QuestionOptions> Exam_QuestionOption { get; set; }
      
    }
}
