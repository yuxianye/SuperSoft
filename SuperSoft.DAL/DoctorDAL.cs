using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;

namespace SuperSoft.DAL
{
    /// <summary>
    /// Doctor数据访问层，可使用显示事物
    /// </summary>
    public class DoctorDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public DoctorDAL()
        {
            sqlConnection = new SqlConnection(Const.DbConnectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        //private System.Data.SQLite.SQLiteConnection sQLiteConnection;
        private SqlConnection sqlConnection;

        #region 数据库操作字符串SQL语句
        //3个字段
        private const string selectCount = "SELECT COUNT(*) FROM Doctors";

        private const string insert = "INSERT INTO Doctors(Id,FirstName,LastName) VALUES(@Id,@FirstName,@LastName)";

        private const string deleteById = "DELETE FROM Doctors WHERE Id=@Id";

        //private const string deleteByIds = "DELETE FROM Doctors WHERE Id IN (@Ids)";

        private const string updateById = "UPDATE Doctors SET FirstName=@FirstName,LastName=@LastName WHERE Id =@Id";

        private const string selectById = "SELECT Id,FirstName,LastName FROM Doctors WHERE Id =@Id";

        //private const string selectPaging = "SELECT Id,FirstName,LastName FROM Doctors ORDER BY Id DESC WHERE Id NOT IN (SELECT Id )";
        private const string selectPaging = "SELECT top (@PageSize) Id,FirstName,LastName FROM ( SELECT ROW_NUMBER() OVER (ORDER BY Id DESC) AS tmpId,Id,FirstName,LastName FROM Doctors)AS t WHERE tmpId > @OffsetCount";

        //private const string selectByFirstName = "SELECT Id,FirstName,LastName FROM Doctors WHERE FirstName LIKE %@FirstName% ORDER BY Id DESC LIMIT @PageSize OFFSET @OffsetCount";

        //private const string selectByFirstNameCount = "SELECT COUNT(*) FROM Doctors WHERE FirstName LIKE %@FirstName%";

        private const string selectByFirstName = @"SELECT Id,FirstName,LastName FROM Doctors WHERE FirstName LIKE @FirstName ORDER BY Id DESC";

        private const string selectByLastName = @"SELECT Id,FirstName,LastName FROM Doctors WHERE LastName LIKE @LastName ORDER BY Id DESC";

        #endregion

        #region Count

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            return SqlHelper.ExecuteScalar(sqlConnection, System.Data.CommandType.Text, selectCount).GetInt();
        }

        #endregion

        #region Insert

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(Doctor entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, insert,
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@FirstName", entity.FirstName),
                    new SqlParameter("@LastName", entity.LastName)
                    );
            }
        }

        ///// <summary>
        ///// 创建对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Insert(SqlTransaction transaction, Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entity != null)
        //    {
        //        SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert,
        //            new SqlParameter("@Id", entity.Id),
        //            new SqlParameter("@FirstName", entity.Id),
        //            new SqlParameter("@LastName", entity.FirstName)
        //            );
        //    }
        //}

        ///// <summary>
        ///// 创建实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Insert(ICollection<Doctor> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entitys != null && entitys.Count() > 0)
        //    {
        //        SqlTransaction tran = sqlConnection.BeginTransaction();
        //        try
        //        {
        //            foreach (var v in entitys)
        //            {
        //                Insert(tran, v);
        //            }
        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            tran.Dispose();
        //            tran = null;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 创建实体对象集合，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Insert(SqlTransaction transaction, ICollection<Doctor> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entitys != null && entitys.Count() > 0)
        //    {
        //        SqlTransaction tran = transaction;
        //        try
        //        {
        //            foreach (var v in entitys)
        //            {
        //                Insert(tran, v);
        //            }
        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            tran.Dispose();
        //            tran = null;
        //        }
        //    }
        //}

        #endregion

        #region Delete

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, deleteById,
                   new SqlParameter("@Id", id)
                   );
            }
        }

        ///// <summary>
        ///// 删除对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="id">一个实体对象的Id</param>
        //public virtual void Delete(SqlTransaction transaction, Guid id)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (id != Guid.Empty)
        //    {
        //        SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, deleteById,
        //           new SqlParameter("@Id", id)
        //           );
        //    }
        //}

        ///// <summary>
        ///// 删除对象
        ///// </summary>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entity != null)
        //    {
        //        Delete(entity.Id);
        //    }
        //}

        ///// <summary>
        ///// 删除对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(SqlTransaction transaction, Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entity != null)
        //    {
        //        SqlTransaction tran = transaction;
        //        try
        //        {
        //            Delete(entity);
        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            tran.Dispose();
        //            tran = null;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 删除实体对象集合
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(ICollection<Doctor> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entitys != null && entitys.Count() > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (var v in entitys)
        //        {
        //            sb.Append(v.Id);
        //            sb.Append(',');
        //        }
        //        sb.Remove(sb.Length - 2, 1);
        //        SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, deleteByIds, new SqlParameter("@Ids", sb.ToString()));
        //        sb.Clear();
        //        sb = null;
        //    }
        //}

        ///// <summary>
        ///// 删除实体对象集合，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(SqlTransaction transaction, ICollection<Doctor> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entitys != null && entitys.Count() > 0)
        //    {
        //        foreach (var v in entitys)
        //        {
        //            Delete(transaction, v);
        //        }
        //    }
        //}

        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public virtual void Update(Doctor entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, updateById,
                    new SqlParameter("@FirstName", entity.FirstName),
                    new SqlParameter("@LastName", entity.LastName),
                    new SqlParameter("@Id", entity.Id)
                    );
            }
        }

        ///// <summary>
        ///// 更新对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Update(SqlTransaction transaction, Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entity != null)
        //    {
        //        SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
        //            new SqlParameter("@FirstName", entity.FirstName),
        //            new SqlParameter("@LastName", entity.FirstName),
        //            new SqlParameter("@Id", entity.Id)
        //            );
        //    }
        //}

        ///// <summary>
        ///// 更新实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">将要编辑的实体对象集合</param>
        //public virtual void Update(ICollection<Doctor> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entitys.Any())
        //    {
        //        SqlTransaction tran = sqlConnection.BeginTransaction();
        //        try
        //        {
        //            foreach (var v in entitys)
        //            {
        //                Update(tran, v);
        //            }
        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            tran.Dispose();
        //            tran = null;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 更新实体对象集合，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Update(SqlTransaction transaction, ICollection<Doctor> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    if (entitys.Any())
        //    {
        //        SqlTransaction tran = transaction;
        //        try
        //        {
        //            foreach (var v in entitys)
        //            {
        //                Update(tran, v);
        //            }
        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            tran.Dispose();
        //            tran = null;
        //        }
        //    }
        //}

        #endregion

        #region GetByCondition

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        /// <returns>一个实体对象</returns>
        public virtual Doctor GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            Doctor result = null;
            if (id != Guid.Empty)
            {
                using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectById,
                      new SqlParameter("@Id", id)
                      ))
                {
                    if (reader.HasRows)
                    {
                        result = new Doctor();
                    }
                    while (reader.Read())
                    {
                        result.Id = reader.GetGuid(0);
                        result.FirstName = reader.GetString(1);
                        result.LastName = reader.GetString(2);
                    }
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual ICollection<Doctor> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<Doctor> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectPaging,
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@OffsetCount", offsetCount)
            ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Doctor>();
                }
                while (reader.Read())
                {
                    Doctor result = new Doctor();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        ///// <summary>
        ///// 分页查询,使用Id desc排序
        ///// </summary>
        ///// <param name="firstName">firstName</param>
        ///// <param name="pageIndex">页号</param>
        ///// <param name="pageSize">页大小</param>
        ///// <param name="recordCount">记录总数</param>
        ///// <returns></returns>
        //public virtual ICollection<Doctor> SelectByFirstName(string firstName, int pageIndex, int pageSize, out int recordCount)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    recordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, CommandType.Text, selectByFirstNameCount,
        //        new SqlParameter("@FirstName", firstName)
        //        ));
        //    int offsetCount = (pageIndex - 1) * pageSize;
        //    ICollection<Doctor> resultList = null;
        //    using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByFirstName,
        //        new SqlParameter("@FirstName", firstName),
        //        new SqlParameter("@PageSize", pageSize),
        //        new SqlParameter("@OffsetCount", offsetCount)
        //        ))
        //    {
        //        if (reader.HasRows)
        //        {
        //            resultList = new System.Collections.ObjectModel.Collection<Doctor>();
        //        }
        //        while (reader.Read())
        //        {
        //            Doctor result = new Doctor();
        //            result.Id = reader.GetGuid(0);
        //            result.FirstName = reader.GetString(1);
        //            result.LastName = reader.GetString(2);
        //            resultList.Add(result);
        //        }
        //        reader.Close();
        //    }
        //    return resultList;
        //}

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="firstName">firstName</param>
        /// <returns></returns>
        public virtual ICollection<Doctor> SelectByFirstName(string firstName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Doctor> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByFirstName,
             new SqlParameter("@FirstName", "%" + firstName + "%")


                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Doctor>();
                }
                while (reader.Read())
                {
                    Doctor result = new Doctor();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="lastName">lastName</param>
        /// <returns></returns>
        public virtual ICollection<Doctor> SelectByLastName(string lastName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Doctor> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByLastName,
                new SqlParameter("@LastName", "%" + lastName + "%")

                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Doctor>();
                }
                while (reader.Read())
                {
                    Doctor result = new Doctor();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        #endregion

        #region Dispose 

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (!Equals(sqlConnection, null))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }

        #endregion
    }
}