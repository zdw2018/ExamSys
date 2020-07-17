using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Model;
using Exam.DAL;

namespace Exam.BLL
{
    public class StudentMannerService
    {
        /// <summary>
        /// 获去所有学生数据
        /// </summary>
        /// <param name="lmid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IPagedList GetList(int page=1)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            int pagesize = 10;
            IPagedList list = db.Exam_User.Where(x => x.UserType == 0).OrderBy(x => x.UserID).ToPagedList(page, pagesize);
            return list;
        }
    }
}
