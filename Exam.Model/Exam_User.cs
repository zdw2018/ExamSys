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
    /// 用户信息表
    /// </summary>
   public class Exam_User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
        [StringLength(20)]
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        [StringLength(32)]
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public bool States { get; set; }
       [StringLength(11)]
        /// <summary>
       /// 联系电话
       /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(8)]       
        public string RealName { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        [StringLength(8)]
        /// <summary>
        /// 添加人
        /// </summary>
        public string CreateName { get; set; }
    }
}
