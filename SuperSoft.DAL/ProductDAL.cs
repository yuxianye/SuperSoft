using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SuperSoft.DAL
{
    /// <summary>
    /// Product数据访问层，可使用显示事物
    /// </summary>
    public class ProductDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ProductDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //5个字段
        private const string selectCount = "SELECT COUNT(*) FROM Products";
        private const string insert = @"INSERT INTO Products(Id,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime)
VALUES(@Id,@SerialNumber,@ProductVersion,@ProductModel,@TotalWorkingTime)";
        private const string deleteById = "DELETE FROM Products WHERE Id=@Id";
        private const string deleteByIds = "DELETE FROM Products WHERE Id IN (@Ids)";

        private const string updateById = "UPDATE Products SET SerialNumber=@SerialNumber,ProductVersion=@ProductVersion,ProductModel=@ProductModel,TotalWorkingTime=@TotalWorkingTime WHERE Id =@Id";

        private const string selectById = "SELECT Id,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime FROM Products WHERE Id =@Id";

        private const string selectPaging = "SELECT Id,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime FROM Products ORDER BY Id DESC LIMIT @PageSize OFFSET @OffictCount";
        private const string selectBySerialNumber = "SELECT Id,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime FROM Products WHERE SerialNumber=@SerialNumber ORDER BY Id DESC LIMIT @PageSize OFFSET @OffictCount";
        private const string selectBySerialNumberCount = "SELECT COUNT(*) FROM Products WHERE SerialNumber=@SerialNumber";

        private const string selectBySerialNumber2 = "SELECT Id,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime FROM Products WHERE SerialNumber=@SerialNumber ORDER BY Id DESC";

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
            return SQLiteHelper.ExecuteScalar(sQLiteConnection, System.Data.CommandType.Text, selectCount).GetInt();
        }

        #endregion

        #region Insert

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(Product entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, insert,
                    new SQLiteParameter("@Id", entity.Id),
                    new SQLiteParameter("@SerialNumber", entity.SerialNumber),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@TotalWorkingTime", entity.TotalWorkingTime)
                    );
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SQLiteTransaction transaction, Product entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert,
                   new SQLiteParameter("@Id", entity.Id),
                    new SQLiteParameter("@SerialNumber", entity.SerialNumber),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@TotalWorkingTime", entity.TotalWorkingTime)
                    );
            }
        }

        /// <summary>
        /// 创建实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Insert(IEnumerable<Product> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SQLiteTransaction tran = sQLiteConnection.BeginTransaction();
                try
                {
                    foreach (var v in entitys)
                    {
                        Insert(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 创建实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Insert(SQLiteTransaction transaction, IEnumerable<Product> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SQLiteTransaction tran = transaction;
                try
                {
                    foreach (var v in entitys)
                    {
                        Insert(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

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
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, deleteById,
                   new SQLiteParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(SQLiteTransaction transaction, Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, deleteById,
                   new SQLiteParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(Product entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                Delete(entity.Id);
            }
        }

        /// <summary>
        /// 删除对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(SQLiteTransaction transaction, Product entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteTransaction tran = transaction;
                try
                {
                    Delete(entity);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 删除实体对象集合
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(IEnumerable<Product> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var v in entitys)
                {
                    sb.Append(v.Id);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 2, 1);
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, deleteByIds, new SQLiteParameter("@Ids", sb.ToString()));
                sb.Clear();
                sb = null;
            }
        }

        /// <summary>
        /// 删除实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(SQLiteTransaction transaction, IEnumerable<Product> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                foreach (var v in entitys)
                {
                    Delete(transaction, v);
                }
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public virtual void Update(Product entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@SerialNumber", entity.SerialNumber),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@TotalWorkingTime", entity.TotalWorkingTime),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Update(SQLiteTransaction transaction, Product entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@SerialNumber", entity.SerialNumber),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@TotalWorkingTime", entity.TotalWorkingTime),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(IEnumerable<Product> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SQLiteTransaction tran = sQLiteConnection.BeginTransaction();
                try
                {
                    foreach (var v in entitys)
                    {
                        Update(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 更新实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Update(SQLiteTransaction transaction, IEnumerable<Product> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SQLiteTransaction tran = transaction;
                try
                {
                    foreach (var v in entitys)
                    {
                        Update(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        #endregion

        #region GetByCondition

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        /// <returns>一个实体对象</returns>
        public virtual Product GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                Product result = new Product();
                using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectById,
                      new SQLiteParameter("@Id", id)
                      ))
                {
                    while (reader.Read())
                    {
                        result.Id = reader.GetGuid(0);
                        result.SerialNumber = reader.GetString(1);
                        result.ProductVersion = reader.GetString(2);
                        result.ProductModel = reader.GetInt32(3);
                        result.TotalWorkingTime = reader.GetInt32(4);
                    }
                    reader.Close();
                }
                return result;
            }
            return default(Product);
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual IEnumerable<Product> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<Product> resultList = new System.Collections.ObjectModel.Collection<Product>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectPaging,
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
                ))
            {
                while (reader.Read())
                {
                    Product result = new Product();
                    result.Id = reader.GetGuid(0);
                    result.SerialNumber = reader.GetString(1);
                    result.ProductVersion = reader.GetString(2);
                    result.ProductModel = reader.GetInt32(3);
                    result.TotalWorkingTime = reader.GetInt32(4);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="serialNumber">serialNumber</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual IEnumerable<Product> SelectBySerialNumber(string serialNumber, int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = Convert.ToInt32(SQLiteHelper.ExecuteScalar(sQLiteConnection, CommandType.Text, selectBySerialNumberCount,
                 new SQLiteParameter("@SerialNumber", serialNumber)
                 ));
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<Product> resultList = new System.Collections.ObjectModel.Collection<Product>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectBySerialNumber,
                new SQLiteParameter("@SerialNumber", serialNumber),
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
                ))
            {
                while (reader.Read())
                {
                    Product result = new Product();
                    result.Id = reader.GetGuid(0);
                    result.SerialNumber = reader.GetString(1);
                    result.ProductVersion = reader.GetString(2);
                    result.ProductModel = reader.GetInt32(3);
                    result.TotalWorkingTime = reader.GetInt32(4);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="serialNumber">serialNumber</param>
        /// <returns></returns>
        public virtual IEnumerable<Product> SelectBySerialNumber(string serialNumber)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<Product> resultList = new System.Collections.ObjectModel.Collection<Product>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectBySerialNumber2,
                new SQLiteParameter("@SerialNumber", serialNumber)
                ))
            {
                while (reader.Read())
                {
                    Product result = new Product();
                    result.Id = reader.GetGuid(0);
                    result.SerialNumber = reader.GetString(1);
                    result.ProductVersion = reader.GetString(2);
                    result.ProductModel = reader.GetInt32(3);
                    result.TotalWorkingTime = reader.GetInt32(4);
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
            if (!Equals(sQLiteConnection, null))
            {
                sQLiteConnection.Close();
                sQLiteConnection.Dispose();
                sQLiteConnection = null;
            }
        }

        #endregion
    }
}