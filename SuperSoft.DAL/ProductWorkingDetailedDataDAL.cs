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
    /// ProductWorkingDetailedData数据访问层，可使用显示事物
    /// </summary>
    public class ProductWorkingDetailedDataDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ProductWorkingDetailedDataDAL()
        {
            sqlConnection = new SqlConnection(Const.DbConnectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private SqlConnection sqlConnection;

        #region 数据库操作字符串SQL语句
        //3个字段
        private const string selectCount = "SELECT COUNT(*) FROM ProductWorkingDetailedDatas";
        private const string insert = "INSERT INTO ProductWorkingDetailedDatas(Id,ProductWorkingSummaryDataId,Content) VALUES(@Id,@ProductWorkingSummaryDataId,@Content)";
        private const string deleteById = "DELETE FROM ProductWorkingDetailedDatas WHERE Id=@Id";
        private const string deleteByIds = "DELETE FROM ProductWorkingDetailedDatas WHERE Id IN (@Ids)";

        private const string updateById = "UPDATE ProductWorkingDetailedDatas SET ProductWorkingSummaryDataId=@ProductWorkingSummaryDataId,Content=@Content WHERE Id =@Id";

        private const string selectById = "SELECT Id,ProductWorkingSummaryDataId,Content FROM ProductWorkingDetailedDatas WHERE Id =@Id";

        private const string selectPaging = "SELECT Id,ProductWorkingSummaryDataId,Content FROM ProductWorkingDetailedDatas ORDER BY Id DESC LIMIT @PageSize OFFSET @OffsetCount";
        private const string selectByProductWorkingSummaryDataId = "SELECT Id,ProductWorkingSummaryDataId,Content FROM ProductWorkingDetailedDatas WHERE ProductWorkingSummaryDataId=@ProductWorkingSummaryDataId ORDER BY Id DESC LIMIT @PageSize OFFSET @OffsetCount";
        private const string selectByProductWorkingSummaryDataIdCount = "SELECT COUNT(*) FROM ProductWorkingDetailedDatas WHERE ProductWorkingSummaryDataId=@ProductWorkingSummaryDataId";

        private const string selectByProductWorkingSummaryDataId2 = "SELECT Id,ProductWorkingSummaryDataId,Content FROM ProductWorkingDetailedDatas WHERE ProductWorkingSummaryDataId=@ProductWorkingSummaryDataId";


        private const string deleteByProductWorkingSummaryDataIds = "DELETE FROM ProductWorkingDetailedDatas WHERE ProductWorkingSummaryDataId IN (@ProductWorkingSummaryDataIds)";

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
        public virtual void Insert(ProductWorkingDetailedData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, insert,
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@ProductWorkingSummaryDataId", entity.ProductWorkingSummaryDataId),
                    new SqlParameter("@Content", entity.Content)
                    );
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SqlTransaction transaction, ProductWorkingDetailedData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert,
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@ProductWorkingSummaryDataId", entity.ProductWorkingSummaryDataId),
                    new SqlParameter("@Content", entity.Content)
                    );
            }
        }

        /// <summary>
        /// 创建实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param Name="entitys">实体对象集合</param>
        public virtual void Insert(ICollection<ProductWorkingDetailedData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SqlTransaction tran = sqlConnection.BeginTransaction();
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
        public virtual void Insert(SqlTransaction transaction, ICollection<ProductWorkingDetailedData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SqlTransaction tran = transaction;
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
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, deleteById,
                   new SqlParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(SqlTransaction transaction, Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, deleteById,
                   new SqlParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(ProductWorkingDetailedData entity)
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
        public virtual void Delete(SqlTransaction transaction, ProductWorkingDetailedData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlTransaction tran = transaction;
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
        public virtual void Delete(ICollection<ProductWorkingDetailedData> entitys)
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
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, deleteByIds, new SqlParameter("@Ids", sb.ToString()));
                sb.Clear();
                sb = null;
            }
        }

        /// <summary>
        /// 删除实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(SqlTransaction transaction, ICollection<ProductWorkingDetailedData> entitys)
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

        /// <summary>
        /// 删除集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="productWorkingSummaryDataIds">集合</param>
        public virtual void DeleteByProductWorkingSummaryDataIds(SqlTransaction transaction, ICollection<Guid> productWorkingSummaryDataIds)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (productWorkingSummaryDataIds != null && productWorkingSummaryDataIds.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var v in productWorkingSummaryDataIds)
                {
                    sb.Append(v);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 2, 1);
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, deleteByProductWorkingSummaryDataIds,
                    new SqlParameter("@ProductWorkingSummaryDataIds", sb.ToString()));
                sb.Clear();
                sb = null;
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public virtual void Update(ProductWorkingDetailedData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, updateById,
                    new SqlParameter("@ProductWorkingSummaryDataId", entity.ProductWorkingSummaryDataId),
                    new SqlParameter("@Content", entity.Content),
                    new SqlParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Update(SqlTransaction transaction, ProductWorkingDetailedData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
                    new SqlParameter("@ProductWorkingSummaryDataId", entity.ProductWorkingSummaryDataId),
                    new SqlParameter("@Content", entity.Content),
                    new SqlParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(ICollection<ProductWorkingDetailedData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SqlTransaction tran = sqlConnection.BeginTransaction();
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
        public virtual void Update(SqlTransaction transaction, ICollection<ProductWorkingDetailedData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SqlTransaction tran = transaction;
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
        public virtual ProductWorkingDetailedData GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ProductWorkingDetailedData result = null;
            if (id != Guid.Empty)
            {
                using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectById,
                      new SqlParameter("@Id", id)
                      ))
                {
                    if (reader.HasRows)
                    {
                        result = new ProductWorkingDetailedData();
                    }
                    while (reader.Read())
                    {
                        result.Id = reader.GetGuid(0);
                        result.ProductWorkingSummaryDataId = reader.GetGuid(1);
                        if (!reader.IsDBNull(2))
                        {
                            long length = reader.GetBytes(2, 0, null, 0, int.MaxValue);
                            if (length > 0)
                            {
                                var blob = new Byte[length];
                                reader.GetBytes(7, 0, blob, 0, blob.Length);
                                result.Content = blob;
                            }
                        }
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
        public virtual ICollection<ProductWorkingDetailedData> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingDetailedData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectPaging,
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@OffsetCount", offsetCount)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingDetailedData>();
                }
                while (reader.Read())
                {
                    ProductWorkingDetailedData result = new ProductWorkingDetailedData();
                    result.Id = reader.GetGuid(0);
                    result.ProductWorkingSummaryDataId = reader.GetGuid(1);
                    if (!reader.IsDBNull(2))
                    {
                        long length = reader.GetBytes(2, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Content = blob;
                        }
                    }
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="productWorkingSummaryDataId">productWorkingSummaryDataId</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual ICollection<ProductWorkingDetailedData> SelectByProductWorkingSummaryDataId(Guid productWorkingSummaryDataId, int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, CommandType.Text, selectByProductWorkingSummaryDataIdCount,
                 new SqlParameter("@ProductWorkingSummaryDataId", productWorkingSummaryDataId)
                 ));
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingDetailedData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByProductWorkingSummaryDataId,
                new SqlParameter("@ProductWorkingSummaryDataId", productWorkingSummaryDataId),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@OffsetCount", offsetCount)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingDetailedData>();
                }
                while (reader.Read())
                {
                    ProductWorkingDetailedData result = new ProductWorkingDetailedData();
                    result.Id = reader.GetGuid(0);
                    result.ProductWorkingSummaryDataId = reader.GetGuid(1);
                    if (!reader.IsDBNull(2))
                    {
                        long length = reader.GetBytes(2, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Content = blob;
                        }
                    }
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="productWorkingSummaryDataId">productWorkingSummaryDataId</param>
        /// <returns></returns>
        public virtual ICollection<ProductWorkingDetailedData> SelectByProductWorkingSummaryDataId(Guid productWorkingSummaryDataId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<ProductWorkingDetailedData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByProductWorkingSummaryDataId2,
                new SqlParameter("@ProductWorkingSummaryDataId", productWorkingSummaryDataId)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingDetailedData>();
                }
                while (reader.Read())
                {
                    ProductWorkingDetailedData result = new ProductWorkingDetailedData();
                    result.Id = reader.GetGuid(0);
                    result.ProductWorkingSummaryDataId = reader.GetGuid(1);
                    if (!reader.IsDBNull(2))
                    {
                        long length = reader.GetBytes(2, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Content = blob;
                        }
                    }
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