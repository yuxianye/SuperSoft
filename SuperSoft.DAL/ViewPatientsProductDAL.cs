using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SuperSoft.DAL
{
    /// <summary>
    /// ViewPatientsProduct数据访问层
    /// 只能查询数据
    /// </summary>
    public class ViewPatientsProductDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ViewPatientsProductDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //8个字段
        private const string selectByPatientId = @"SELECT Id,PatientId,FirstName,LastName,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime 
FROM ViewPatientsProducts WHERE PatientId=@PatientId ORDER BY Id";


        private const string selectBySerialNumber = @"SELECT Id,PatientId,FirstName,LastName,SerialNumber,ProductVersion,ProductModel,TotalWorkingTime 
FROM ViewPatientsProducts WHERE SerialNumber=@SerialNumber ORDER BY Id DESC";

        #endregion

        #region GetByCondition

        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <returns></returns>
        public virtual IEnumerable<ViewPatientsProduct> SelectByPatientId(Guid patientId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<ViewPatientsProduct> resultList = new System.Collections.ObjectModel.Collection<ViewPatientsProduct>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByPatientId,
                new SQLiteParameter("@PatientId", patientId)
                ))
            {
                while (reader.Read())
                {
                    ViewPatientsProduct result = new ViewPatientsProduct();
                    result.Id = reader.GetGuid(0);
                    result.PatientId = reader.GetGuid(1);
                    result.FirstName = reader.GetString(2);
                    result.LastName = reader.GetString(3);
                    result.SerialNumber = reader.GetString(4);
                    result.ProductVersion = reader.GetString(5);
                    result.ProductModel = reader.GetInt32(6);
                    result.TotalWorkingTime = reader.GetInt32(7);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="serialNumber">serialNumber</param>
        /// <returns></returns>
        public virtual IEnumerable<ViewPatientsProduct> SelectBySerialNumber(string serialNumber)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<ViewPatientsProduct> resultList = new System.Collections.ObjectModel.Collection<ViewPatientsProduct>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectBySerialNumber,
                new SQLiteParameter("@SerialNumber", serialNumber)
                ))
            {
                while (reader.Read())
                {
                    ViewPatientsProduct result = new ViewPatientsProduct();
                    result.Id = reader.GetGuid(0);
                    result.PatientId = reader.GetGuid(1);
                    result.FirstName = reader.GetString(2);
                    result.LastName = reader.GetString(3);
                    result.SerialNumber = reader.GetString(4);
                    result.ProductVersion = reader.GetString(5);
                    result.ProductModel = reader.GetInt32(6);
                    result.TotalWorkingTime = reader.GetInt32(7);
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