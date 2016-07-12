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
    /// Patient数据访问层，可使用显示事物
    /// </summary>
    public class PatientDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public PatientDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //14个字段
        private const string selectCount = "SELECT COUNT(*) FROM Patients";
        private const string insert = @"INSERT INTO Patients(Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId) 
VALUES(@Id,@FirstName,@LastName,@DateOfBirth,@Weight,@Height,@Gender,@Photo,@EMail,@TelephoneNumbers,@PostalCode,@Address,@Diagnosis,@DoctorId)";
        private const string deleteById = "DELETE FROM Patients WHERE Id=@Id";
        private const string deleteByIds = "DELETE FROM Patients WHERE Id IN (@Ids)";

        private const string updateById = @"UPDATE Patients SET FirstName=@FirstName,LastName=@LastName,DateOfBirth=@DateOfBirth,Weight=@Weight,Height=@Height,
Gender=@Gender,Photo=@Photo,EMail=@EMail,TelephoneNumbers=@TelephoneNumbers,PostalCode=@PostalCode,Address=@Address,Diagnosis=@Diagnosis,DoctorId=@DoctorId WHERE Id =@Id";

        private const string selectById = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,
EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId FROM Patients WHERE Id =@Id";

        private const string selectPaging = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients ORDER BY Id DESC LIMIT @PageSize OFFSET @OffsetCount";
        private const string selectByFirstName = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE FirstName like %@FirstName% ORDER BY Id DESC LIMIT @PageSize OFFSET @OffsetCount";
        private const string selectByFirstNameCount = "SELECT COUNT(*) FROM Patients WHERE FirstName LIKE %@FirstName%";

        private const string selectByFirstName2 = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE FirstName like @FirstName";

        private const string selectByLastName = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE LastName like @LastName";

        private const string selectByDateOfBirth = @"SELECT Id,FirstName,LastName,DateOfBirth,Weight,Height,Gender,Photo,EMail,TelephoneNumbers,PostalCode,Address,Diagnosis,DoctorId 
FROM Patients WHERE DateOfBirth like @DateOfBirth";

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
            return SQLiteHelper.ExecuteScalar(sQLiteConnection, System.Data.CommandType.Text, selectCount).GetInt();
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
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, insert,
                    new SQLiteParameter("@Id", entity.Id),
                    new SQLiteParameter("@FirstName", entity.FirstName),
                    new SQLiteParameter("@LastName", entity.LastName),
                    new SQLiteParameter("@DateOfBirth", entity.DateOfBirth),
                    new SQLiteParameter("@Weight", entity.Weight),
                    new SQLiteParameter("@Height", entity.Height),
                    new SQLiteParameter("@Gender", entity.Gender),
                    new SQLiteParameter("@Photo", entity.Photo),
                    new SQLiteParameter("@EMail", entity.EMail),
                    new SQLiteParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SQLiteParameter("@PostalCode", entity.PostalCode),
                    new SQLiteParameter("@Address", entity.Address),
                    new SQLiteParameter("@Diagnosis", entity.Diagnosis),
                    new SQLiteParameter("@DoctorId", entity.DoctorId)
                    );
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SQLiteTransaction transaction, Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert,
                    new SQLiteParameter("@Id", entity.Id),
                    new SQLiteParameter("@FirstName", entity.FirstName),
                    new SQLiteParameter("@LastName", entity.LastName),
                    new SQLiteParameter("@DateOfBirth", entity.DateOfBirth),
                    new SQLiteParameter("@Weight", entity.Weight),
                    new SQLiteParameter("@Height", entity.Height),
                    new SQLiteParameter("@Gender", entity.Gender),
                    new SQLiteParameter("@Photo", entity.Photo),
                    new SQLiteParameter("@EMail", entity.EMail),
                    new SQLiteParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SQLiteParameter("@PostalCode", entity.PostalCode),
                    new SQLiteParameter("@Address", entity.Address),
                    new SQLiteParameter("@Diagnosis", entity.Diagnosis),
                    new SQLiteParameter("@DoctorId", entity.DoctorId)
                    );
            }
        }

        /// <summary>
        /// 创建实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Insert(ICollection<Patient> entitys)
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
        public virtual void Insert(SQLiteTransaction transaction, ICollection<Patient> entitys)
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
        /// 删除对象,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
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
        /// 删除对象，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
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
        /// 删除对象,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(Patient entity)
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
        /// 删除对象，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(SQLiteTransaction transaction, Patient entity)
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
        /// 删除实体对象集合,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(ICollection<Patient> entitys)
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
        /// 删除实体对象集合，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(SQLiteTransaction transaction, ICollection<Patient> entitys)
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
        public virtual void Update(Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@FirstName", entity.FirstName),
                    new SQLiteParameter("@LastName", entity.LastName),
                    new SQLiteParameter("@DateOfBirth", entity.DateOfBirth),
                    new SQLiteParameter("@Weight", entity.Weight),
                    new SQLiteParameter("@Height", entity.Height),
                    new SQLiteParameter("@Gender", entity.Gender),
                    new SQLiteParameter("@Photo", entity.Photo),
                    new SQLiteParameter("@EMail", entity.EMail),
                    new SQLiteParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SQLiteParameter("@PostalCode", entity.PostalCode),
                    new SQLiteParameter("@Address", entity.Address),
                    new SQLiteParameter("@Diagnosis", entity.Diagnosis),
                    new SQLiteParameter("@DoctorId", entity.DoctorId),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Update(SQLiteTransaction transaction, Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@FirstName", entity.FirstName),
                    new SQLiteParameter("@LastName", entity.FirstName),
                    new SQLiteParameter("@DateOfBirth", entity.DateOfBirth),
                    new SQLiteParameter("@Weight", entity.Weight),
                    new SQLiteParameter("@Height", entity.Height),
                    new SQLiteParameter("@Gender", entity.Gender),
                    new SQLiteParameter("@Photo", entity.Photo),
                    new SQLiteParameter("@EMail", entity.EMail),
                    new SQLiteParameter("@TelephoneNumbers", entity.TelephoneNumbers),
                    new SQLiteParameter("@PostalCode", entity.PostalCode),
                    new SQLiteParameter("@Address", entity.Address),
                    new SQLiteParameter("@Diagnosis", entity.Diagnosis),
                    new SQLiteParameter("@DoctorId", entity.DoctorId),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(ICollection<Patient> entitys)
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
        public virtual void Update(SQLiteTransaction transaction, ICollection<Patient> entitys)
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
        public virtual Patient GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            Patient result = null;
            if (id != Guid.Empty)
            {
                using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectById,
                      new SQLiteParameter("@Id", id)
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
                        result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectPaging,
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
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
                    result.DoctorId = reader.GetGuid(13);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="firstName">firstName</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByFirstName(string firstName, int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = Convert.ToInt32(SQLiteHelper.ExecuteScalar(sQLiteConnection, CommandType.Text, selectByFirstNameCount,
               new SQLiteParameter("@FirstName", firstName)
               ));
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<Patient> resultList = null;
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByFirstName,
                new SQLiteParameter("@FirstName", firstName),
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
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
                    result.DoctorId = reader.GetGuid(13);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByFirstName2,
                new SQLiteParameter("@FirstName", "%" + firstName + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByLastName,
                new SQLiteParameter("@LastName", "%" + lastName + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByDateOfBirth,
                new SQLiteParameter("@DateOfBirth", "%" + dateOfBirth + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByWeight,
                new SQLiteParameter("@Weight", weight)
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByHeight,
                new SQLiteParameter("@Height", height)
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByGender,
                new SQLiteParameter("@Gender", gender)
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByEMail,
                new SQLiteParameter("@EMail", "%" + eMail + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByTelephoneNumbers,
                new SQLiteParameter("@TelephoneNumbers", "%" + telephoneNumbers + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByPostalCode,
                new SQLiteParameter("@PostalCode", "%" + postalCode + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByAddress,
                new SQLiteParameter("@Address", "%" + address + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByDiagnosis,
                new SQLiteParameter("@Diagnosis", "%" + diagnosis + "%")
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
                    result.DoctorId = reader.GetGuid(13);
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
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByDoctorId,
                new SQLiteParameter("@doctorId", doctorId)
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
                    result.DoctorId = reader.GetGuid(13);
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
