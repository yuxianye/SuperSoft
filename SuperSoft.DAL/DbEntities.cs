using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSoft.Model;

namespace SuperSoft.DAL
{
    /// <summary>
    /// 内部类，只在DAL内部使用
    /// 表名和数据库中的表名一致
    /// name名称和配置文件中的connectionStrings一致
    /// </summary>
    internal class DbEntities : DbContext
    {
        /// <summary>
        /// name名称和配置文件中的connectionStrings一致
        /// </summary>
        internal DbEntities()
            : base(@"name=DbEntities")
        {
        }

        /// <summary>
        /// 实体集
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
