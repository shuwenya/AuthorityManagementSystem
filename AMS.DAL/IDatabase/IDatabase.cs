using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMS.DAL
{
    public interface IDatabase
    {
        #region 属性
        /// <summary>
        /// 获取当前使用的数据库访问上下文对象
        /// </summary>
        public DbContext dbContext { get; set; }
        /// <summary>
        /// 事物对象
        /// </summary>
        public IDbContextTransaction dbContextTransaction { get; set; }
        #endregion

        #region 方法

        #region 事务提交
        Task<IDatabase> BeginTrans();
        Task<int> CommitTrans();
        Task RollbackTrans();
        Task Close();
        #endregion

        #endregion
    }
}
