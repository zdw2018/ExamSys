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
        public static IPagedList GetList(int page = 1)
        {
            ExamSysDBContext db = new ExamSysDBContext();
            int pagesize = 10;
            IPagedList list = db.Exam_User.Where(x => x.UserType == 0).OrderBy(x => x.UserID).ToPagedList(page, pagesize);
            return list;
        }
        public static int InsertStudent(Exam_User user)
        {
            ExamSysDBContext dBContext = new ExamSysDBContext();
            dBContext.Exam_User.Add(user);
            return dBContext.SaveChanges();
        }
        public static Exam_User FindStudentByID(int id)
        {
            ExamSysDBContext dBContext = new ExamSysDBContext();
            var data = dBContext.Exam_User.Where(x => x.UserID == id).FirstOrDefault();
            return data;
        }
        public static int RemoveStudent(int id)
        {
            ExamSysDBContext dBContext = new ExamSysDBContext();
            var data = dBContext.Exam_User.Where(x => x.UserID == id).FirstOrDefault();
            dBContext.Exam_User.Remove(data);
            return dBContext.SaveChanges();
        }
        public static int Update(Exam_User u)
        {
            ExamSysDBContext dBContext = new ExamSysDBContext();
            var data = dBContext.Exam_User.Where(x => x.UserID == u.UserID).FirstOrDefault();
            data.UserName = u.UserName;
            data.Phone = u.Phone;
            data.RealName = u.RealName;
            return dBContext.SaveChanges();
        }
    }
}
