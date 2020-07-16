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
    /// 试题选项信息表
    /// </summary>
    public class Exam_QuestionOptions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 选项编号
        /// </summary>
        public int OptionID { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual Exam_Question Exam_Question { get; set; }
        [ForeignKey("Exam_Question")]
        /// <summary>
        /// 试题编号
        /// </summary> 
        public int QuestionID { get; set; }
        [StringLength(200)]
        /// <summary>
        /// 选项详情
        /// </summary>
        public string OptionDescribe { get; set; }
        [StringLength(2)]
        /// <summary>
        /// 选项值
        /// </summary>
        public string OptionCode { get; set; }
       /// <summary>
       /// 创建时间
       /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
      

    }
}
