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
    /// 答题信息表
    /// </summary>
    public class Exam_Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 答题编号
        /// </summary>       
        public int AnswerID { get; set; }
        [ForeignKey("Exam_User")]
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 题库编号
        /// </summary>
        public int LibraryID { get; set; }
        /// <summary>
        /// 试卷编号
        /// </summary>
        public int PaperID { get; set; }       
        /// <summary>
        /// 试题编号
        /// </summary>
        public int QuestionID { get; set; }
        /// <summary>
        /// 试题正确选项编号
        /// </summary>
        public string OptionID { get; set; }
        /// <summary>
        /// 答题选项编号
        /// </summary>
        public string AnswerOptionID { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual Exam_User Exam_User { get; set; }

    }
}
