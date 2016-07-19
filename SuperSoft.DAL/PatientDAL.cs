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
    /// Patient数据访问层，可使用显示事物
    /// </summary>
    public class PatientDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public PatientDAL()
        {
            sqlConnection = new SqlConnection(Const.DbConnectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private SqlConnection sqlConnection;

        #region 数据库操作字符串SQL语句

        //14个字段
        private const string selectCount = "SELECT COUNT(*) FROM Patients";

        private const string insert = @"INSERT INTO Patients(Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId) 
VALUES(@Id,@FirstName,@LastName,@DateOfBirth,@Weight,@Height,@Gender,@Photo,@EMail,@TelephoneNumbers,@PostalCode,@Address,@Diagnosis,@DoctorId)";

        //private const string deleteById = "DELETE FROM Patients WHERE Id=@Id";

        private const string deleteById = "P_DeletePatientAllInfo";

        //private const string deleteByIds = "DELETE FROM Patients WHERE Id IN (@Ids)";

        private const string updateById = @"UPDATE Patients SET FirstName=@FirstName,LastName=@LastName,DateOfBirth=@DateOfBirth,Weight=@Weight,Height=@Height,
Gender=@Gender,Photo=@Photo,EMail=@EMail,TelephoneNumbers=@TelephoneNumbers,PostalCode=@PostalCode,Address=@Address,Diagnosis=@Diagnosis,DoctorId=@DoctorId WHERE Id =@Id";

        private const string selectById = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,
EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId FROM Patients WHERE Id =@Id";

        //private const string selectPaging = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
        //FROM Patients ORDER BY Id ";

        private const string selectPaging = @"SELECT top (@PageSize) Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId  
FROM ( SELECT ROW_NUMBER() OVER (ORDER BY Id DESC) AS tmpId,Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId  FROM Patients)AS t 
WHERE tmpId > @OffsetCount";

        //        private const string selectByFirstName = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
        //FROM Patients WHERE FirstName like %@FirstName% ORDER BY Id DESC LIMIT @PageSize OFFSET @OffsetCount";
        //private const string selectByFirstNameCount = "SELECT COUNT(*) FROM Patients WHERE FirstName LIKE %@FirstName%";

        private const string selectByFirstName = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE FirstName like @FirstName";

        private const string selectByLastName = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE LastName like @LastName";

        private const string selectByDateOfBirth = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE DateOfBirth = @DateOfBirth";

        private const string selectByWeight = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE Weight = @Weight";

        private const string selectByHeight = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE Height = @Height";

        private const string selectByGender = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE Gender = @Gender";

        private const string selectByEMail = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE EMail like @EMail";

        private const string selectByTelephoneNumbers = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE TelephoneNumbers like @TelephoneNumbers";

        private const string selectByPostalCode = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE PostalCode like @PostalCode";

        private const string selectByAddress = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE Address like @Address";

        private const string selectByDiagnosis = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE Diagnosis like @Diagnosis";

        private const string selectByDoctorId = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE DoctorId=@DoctorId";

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
        public virtual void Insert(Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlParameter[] parms = new SqlParameter[14]
               {
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@FirstName", entity.FirstName),
                    new SqlParameter("@LastName", entity.LastName),
                    new SqlParameter("@DateOfBirth", entity.DateOfBirth),
                    new SqlParameter("@Weight", entity.Weight),
                    new SqlParameter("@Height", entity.Height),
                    new SqlParameter("@Gender", entity.Gender),
                    new SqlParameter("@Photo",  entity.Photo),
                    new SqlParameter("@EMail", entity.EMail),
                    new SqlParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SqlParameter("@PostalCode", entity.PostalCode),
                    new SqlParameter("@Address", entity.Address),
                    new SqlParameter("@Diagnosis", entity.Diagnosis),
                    new SqlParameter("@DoctorId", entity.DoctorId)
               };
                parms[7].SqlDbType = SqlDbType.Image;//设置参数类型
                parms[13].SqlDbType = SqlDbType.UniqueIdentifier;//设置参数类型

                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, insert, parms);
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SqlTransaction transaction, Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlParameter[] parms = new SqlParameter[14]
                {
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@FirstName", entity.FirstName),
                    new SqlParameter("@LastName", entity.LastName),
                    new SqlParameter("@DateOfBirth", entity.DateOfBirth),
                    new SqlParameter("@Weight", entity.Weight),
                    new SqlParameter("@Height", entity.Height),
                    new SqlParameter("@Gender", entity.Gender),
                    new SqlParameter("@Photo",  entity.Photo),
                    new SqlParameter("@EMail", entity.EMail),
                    new SqlParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SqlParameter("@PostalCode", entity.PostalCode),
                    new SqlParameter("@Address", entity.Address),
                    new SqlParameter("@Diagnosis", entity.Diagnosis),
                    new SqlParameter("@DoctorId", entity.DoctorId)
                };
                parms[7].SqlDbType = SqlDbType.Image;//设置参数类型
                parms[13].SqlDbType = SqlDbType.UniqueIdentifier;//设置参数类型

                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert, parms);
            }
        }

        ///// <summary>
        ///// 创建实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Insert(ICollection<Patient> entitys)
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
        //public virtual void Insert(SqlTransaction transaction, ICollection<Patient> entitys)
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
        /// 删除对象,调用数据库存储过程,会删除所有患者相关的数据和产品运行信息等数据
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
                int result = 0;
                SqlParameter[] parms = new SqlParameter[2]
                {
                    new SqlParameter("@PatientId", id),
                    new SqlParameter("@Result", result)
                };
                parms[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.StoredProcedure, deleteById, parms);
                if (result != 0)
                {
                    LogHelper.Error("SqlError:" + result.ToString(), null);
                }
            }
        }

        ///// <summary>
        ///// 删除对象，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
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
        ///// 删除对象,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(Patient entity)
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
        ///// 删除对象，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(SqlTransaction transaction, Patient entity)
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
        ///// 删除实体对象集合,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(ICollection<Patient> entitys)
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
        ///// 删除实体对象集合，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(SqlTransaction transaction, ICollection<Patient> entitys)
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
        public virtual void Update(Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlParameter[] parms = new SqlParameter[14]
               {
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@FirstName", entity.FirstName),
                    new SqlParameter("@LastName", entity.LastName),
                    new SqlParameter("@DateOfBirth", entity.DateOfBirth),
                    new SqlParameter("@Weight", entity.Weight),
                    new SqlParameter("@Height", entity.Height),
                    new SqlParameter("@Gender", entity.Gender),
                    new SqlParameter("@Photo",  entity.Photo),
                    new SqlParameter("@EMail", entity.EMail),
                    new SqlParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SqlParameter("@PostalCode", entity.PostalCode),
                    new SqlParameter("@Address", entity.Address),
                    new SqlParameter("@Diagnosis", entity.Diagnosis),
                    new SqlParameter("@DoctorId", entity.DoctorId)
               };
                parms[7].SqlDbType = SqlDbType.Image;//设置参数类型
                parms[13].SqlDbType = SqlDbType.UniqueIdentifier;//设置参数类型
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, updateById, parms);
            }
        }

        ///// <summary>
        ///// 更新对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Update(SqlTransaction transaction, Patient entity)
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
        //            new SqlParameter("@DateOfBirth", entity.DateOfBirth),
        //            new SqlParameter("@Weight", entity.Weight),
        //            new SqlParameter("@Height", entity.Height),
        //            new SqlParameter("@Gender", entity.Gender),
        //            new SqlParameter("@Photo", entity.Photo),
        //            new SqlParameter("@EMail", entity.EMail),
        //            new SqlParameter("@TelephoneNumbers", entity.TelephoneNumbers),
        //            new SqlParameter("@PostalCode", entity.PostalCode),
        //            new SqlParameter("@Address", entity.Address),
        //            new SqlParameter("@Diagnosis", entity.Diagnosis),
        //            new SqlParameter("@DoctorId", entity.DoctorId),
        //            new SqlParameter("@Id", entity.Id)
        //            );
        //    }
        //}

        ///// <summary>
        ///// 更新实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">将要编辑的实体对象集合</param>
        //public virtual void Update(ICollection<Patient> entitys)
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
        //public virtual void Update(SqlTransaction transaction, ICollection<Patient> entitys)
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
        public virtual Patient GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            Patient result = null;
            if (id != Guid.Empty)
            {
                using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectById,
                      new SqlParameter("@Id", id)
                      ))
                {
                    if (reader.HasRows)
                    {
                        result = new Patient();
                    }
                    while (reader.Read())
                    {
                        result.Id = reader.GetGuid(0);
                        result.FirstName = reader.GetString(1);
                        result.LastName = reader.GetString(2);
                        result.DateOfBirth = reader.GetDateTime(3);
                        result.Weight = reader.GetInt32(4);
                        result.Height = reader.GetInt32(5);
                        result.Gender = reader.GetBoolean(6);
                        if (!reader.IsDBNull(7))
                        {
                            long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                            if (length > 0)
                            {
                                var blob = new Byte[length];
                                reader.GetBytes(7, 0, blob, 0, blob.Length);
                                result.Photo = blob;
                            }
                        }
                        result.EMail = reader.GetValue(8).GetString();
                        result.TelephoneNumbers = reader.GetValue(9).GetString();
                        result.PostalCode = reader.GetValue(10).GetString();
                        result.Address = reader.GetValue(11).GetString();
                        result.Diagnosis = reader.GetValue(12).GetString();
                        if (reader.IsDBNull(13))
                        {
                            result.DoctorId = null;
                        }
                        else
                        {
                            result.DoctorId = reader.GetGuid(13);
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
        public virtual ICollection<Patient> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectPaging,
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@OffsetCount", offsetCount)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
                    }
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
        //public virtual ICollection<Patient> SelectByFirstName(string firstName, int pageIndex, int pageSize, out int recordCount)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    recordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, CommandType.Text, selectByFirstNameCount,
        //       new SqlParameter("@FirstName", firstName)
        //       ));
        //    int offsetCount = (pageIndex - 1) * pageSize;
        //    ICollection<Patient> resultList = null;
        //    using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByFirstName,
        //        new SqlParameter("@FirstName", firstName),
        //        new SqlParameter("@PageSize", pageSize),
        //        new SqlParameter("@OffsetCount", offsetCount)
        //        ))
        //    {
        //        if (reader.HasRows)
        //        {
        //            resultList = new System.Collections.ObjectModel.Collection<Patient>();
        //        }
        //        while (reader.Read())
        //        {
        //            Patient result = new Patient();
        //            result.Id = reader.GetGuid(0);
        //            result.FirstName = reader.GetString(1);
        //            result.LastName = reader.GetString(2);
        //            result.DateOfBirth = reader.GetDateTime(3);
        //            result.Weight = reader.GetInt32(4);
        //            result.Height = reader.GetInt32(5);
        //            result.Gender = reader.GetBoolean(6);
        //            if (!reader.IsDBNull(7))
        //            {
        //                long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
        //                if (length > 0)
        //                {
        //                    var blob = new Byte[length];
        //                    reader.GetBytes(7, 0, blob, 0, blob.Length);
        //                    result.Photo = blob;
        //                }
        //            }
        //            result.EMail = reader.GetValue(8).GetString();
        //            result.TelephoneNumbers = reader.GetValue(9).GetString();
        //            result.PostalCode = reader.GetValue(10).GetString();
        //            result.Address = reader.GetValue(11).GetString();
        //            result.Diagnosis = reader.GetValue(12).GetString();
        //            result.DoctorId = reader.GetGuid(13);
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
        public virtual ICollection<Patient> SelectByFirstName(string firstName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByFirstName,
                new SqlParameter("@FirstName", "%" + firstName + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="lastName">lastName</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByLastName(string lastName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByLastName,
                new SqlParameter("@LastName", "%" + lastName + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="dateOfBirth">dateOfBirth</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByDateOfBirth(DateTime dateOfBirth)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByDateOfBirth,
                new SqlParameter("@DateOfBirth", dateOfBirth)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="weight">weight</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByWeight(int weight)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByWeight,
                new SqlParameter("@Weight", weight)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="height">height</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByHeight(int height)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByHeight,
                new SqlParameter("@Height", height)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="gender">gender</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByGender(bool gender)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByGender,
                new SqlParameter("@Gender", gender)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="eMail">eMail</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByEMail(string eMail)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByEMail,
                new SqlParameter("@EMail", "%" + eMail + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="telephoneNumbers">telephoneNumbers</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SselectByTelephoneNumbers(string telephoneNumbers)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByTelephoneNumbers,
                new SqlParameter("@TelephoneNumbers", "%" + telephoneNumbers + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="postalCode">postalCode</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByPostalCode(string postalCode)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByPostalCode,
                new SqlParameter("@PostalCode", "%" + postalCode + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="address">address</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByAddress(string address)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByAddress,
                new SqlParameter("@Address", "%" + address + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="diagnosis">diagnosis</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByDiagnosis(string diagnosis)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByDiagnosis,
                new SqlParameter("@Diagnosis", "%" + diagnosis + "%")
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
        /// <param name="doctorId">doctorId</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByDoctorId(Guid doctorId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<Patient> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByDoctorId,
                new SqlParameter("@doctorId", doctorId)
                ))
            {

                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<Patient>();
                }
                while (reader.Read())
                {
                    Patient result = new Patient();
                    result.Id = reader.GetGuid(0);
                    result.FirstName = reader.GetString(1);
                    result.LastName = reader.GetString(2);
                    result.DateOfBirth = reader.GetDateTime(3);
                    result.Weight = reader.GetInt32(4);
                    result.Height = reader.GetInt32(5);
                    result.Gender = reader.GetBoolean(6);
                    if (!reader.IsDBNull(7))
                    {
                        long length = reader.GetBytes(7, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(7, 0, blob, 0, blob.Length);
                            result.Photo = blob;
                        }
                    }
                    result.EMail = reader.GetValue(8).GetString();
                    result.TelephoneNumbers = reader.GetValue(9).GetString();
                    result.PostalCode = reader.GetValue(10).GetString();
                    result.Address = reader.GetValue(11).GetString();
                    result.Diagnosis = reader.GetValue(12).GetString();
                    if (reader.IsDBNull(13))
                    {
                        result.DoctorId = null;
                    }
                    else
                    {
                        result.DoctorId = reader.GetGuid(13);
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
