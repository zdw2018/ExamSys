using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Model;

namespace Exam.DAL
{
    public class ExamSysDBContext : DbContext
    {

        public ExamSysDBContext():base("DBContext")
        {
            this.Database.CommandTimeout = 600000; //时间单位是毫秒
            ////初始化自动迁移
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ExamSysDBContext, Configuration>());
            
        }
        //默认生成的表名为类型的复数形式，想自定义规则时需要重写数据上下文类的OnModelCreating方法
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }    
        #region
        public DbSet<Exam_Answer> Exam_Answer { get; set; }
        public DbSet<Exam_Library> Exam_Library { get; set; }
        public DbSet<Exam_Paper> Exam_Paper { get; set; }
        public DbSet<Exam_PaperRule> Exam_PaperRule { get; set; }
        public DbSet<Exam_Question> Exam_Question { get; set; }
        public DbSet<Exam_QuestionOptions> Exam_QuestionOptions { get; set; }
        public DbSet<Exam_RuleDetail> Exam_RuleDetail { get; set; }
        public DbSet<Exam_User> Exam_User { get; set; }
        #endregion      

    }

    public class Configuration : DbMigrationsConfiguration<ExamSysDBContext>
    {
        public Configuration()
        {
            
            //开启自动迁移
            AutomaticMigrationsEnabled = true;
            //迁移的时候是否允许数据丢失
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
