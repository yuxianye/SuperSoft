using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Utility
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionMethod
    {
        #region object

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            var type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }
            finally
            {
                type = null;
            }
            return (T)result;
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            var type = typeof(T);
            try
            {
                result = type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            finally
            {
                type = null;
            }
            return (T)result;
        }


        #endregion

        #region Type

        /// <summary>
        /// 判断指定类型是否为数值类型
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>是否是数值类型</returns>
        public static bool IsNumeric(this Type type)
        {
            return type == typeof(byte)
                   || type == typeof(short)
                   || type == typeof(int)
                   || type == typeof(long)
                   || type == typeof(sbyte)
                   || type == typeof(ushort)
                   || type == typeof(uint)
                   || type == typeof(ulong)
                   || type == typeof(decimal)
                   || type == typeof(double)
                   || type == typeof(float);
        }

        /// <summary>
        /// 判断当前类型的对象能分配于指定泛型类型
        /// </summary>
        /// <param name="givenType">给定类型</param>
        /// <param name="genericType">泛型类型</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (!genericType.IsGenericType)
            {
                return false;
            }
            var interfaceTypes = givenType.GetInterfaces();
            if (
                interfaceTypes.Any(
                    interfaceType =>
                        interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }
            interfaceTypes = null;
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            var baseType = givenType.BaseType;
            if (baseType == null)
            {
                return false;
            }
            return IsAssignableToGenericType(baseType, genericType);
        }


        #endregion

        #region MemberInfo

        /// <summary>
        /// 获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string ToDescription(this MemberInfo member, bool inherit = false)
        {
            var desc = member.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? null : desc.Description;
        }

        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool AttributeExists<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Any(m => m as T != null);
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).SingleOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        #endregion

        #region byte[]

        /// <summary>
        /// 字节数组转16进制字符串  例如0xae00cf => "AE00CF "
        /// </summary>
        /// <param name="bytes">字节数组，例如0xae00cf</param>
        /// <returns>字节数据的16进制字符串</returns>
        public static string ToHexString(this byte[] bytes) // 0xae00cf => "AE00CF "
        {
            var hexString = default(string);
            if (bytes != null && bytes.Length > 0)
            {
                var strB = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString(@"X2"));
                }
                hexString = strB.ToString();
                strB.Clear();
                strB = null;
            }
            return hexString;
        }


        /// <summary>
        /// byte数组转Int16数值，本方法适用于高位在前低位在后的顺序。
        /// </summary>
        /// <param name="src">至少2个字节长度。否则返回0</param>
        /// <param name="offset">如果此值大于src.Length - 2，则返回0</param>
        /// <returns></returns>
        public static int BytesToInt16(this byte[] src, int offset)
        {
            if (src.Length < 2)
            {
                return 0;
            }
            if (offset > src.Length - 2)
            {
                return 0;
            }
            int value;
            value = ((src[offset] & 0xFF) << 8)
                    | (src[offset + 1] & 0xFF);
            return value;
        }

        /// <summary>
        /// </summary>
        /// <param name="bytes">原数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="length">截取的长度</param>
        /// <returns></returns>
        public static byte[] SubBytes(this byte[] bytes, int startIndex, int length)
        {
            byte[] value;
            if (bytes == null || bytes.Length < startIndex + length)
            {
                return null;
            }
            value = new byte[length];
            System.Array.Copy(bytes, startIndex, value, 0, length);

            //其他实现方法1
            //for (var i = 0; i < length; i++)
            //{
            //    value[i] = bytes[i + startIndex];
            //}

            //其他实现方法2
            //ParallelLoopResult result = Parallel.For(0, length, ctr =>
            //{
            //    value[ctr] = bytes[ctr + startIndex];
            //});
            return value;
        }

        /// <summary>
        /// 字节数组转十进制 字符串// 0x010203 => "010203 "
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToDecString(this byte[] bytes)
        {
            var hexString = string.Empty;

            if (bytes != null)
            {
                var strB = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString().PadLeft(2, '0'));
                }
                hexString = strB.ToString();
                strB.Clear();
                strB = null;
            }
            return hexString;
        }

        #endregion

        #region DateTime

        /// <summary>
        /// 当前时间是否周末
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday) ? true : false;

            //其他实现方法
            //DayOfWeek[] weeks = { DayOfWeek.Saturday, DayOfWeek.Sunday };
            //return weeks.Contains(dateTime.DayOfWeek);
        }

        /// <summary>
        /// 当前时间是否工作日
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static bool IsWeekday(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday) ? true : false;

            //其他实现方法
            //DayOfWeek[] weeks =
            //{
            //    DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
            //    DayOfWeek.Friday
            //};
            //return weeks.Contains(dateTime.DayOfWeek);
        }

        /// <summary>
        /// 获取当前时间月末的日期
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// 获取当前时间月末的日期
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.GetFirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取当前时间月末的日期
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// 获取当前时间月末的日期
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31);
        }

        private static int guidkey = 0;
        /// <summary>
        /// 返回Guid用于数据库操作，特定的时间代码可以提高检索效率
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>COMB类型 Guid 数据</returns>
        public static Guid ToGuid(this DateTime dateTime)
        {
            //var guidArray = Guid.NewGuid().ToByteArray();
            //var dtBase = new DateTime(1900, 1, 1);
            ////获取用于生成byte字符串的天数与毫秒数
            //var days = new TimeSpan(dateTime.Ticks - dtBase.Ticks);
            //var msecs = new TimeSpan(dateTime.Ticks - new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).Ticks);
            ////转换成byte数组
            ////注意SqlServer的时间计数只能精确到1/300秒
            //var daysArray = BitConverter.GetBytes(days.Days);
            //var msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            ////反转字节以符合SqlServer的排序
            //Array.Reverse(daysArray);
            //Array.Reverse(msecsArray);

            ////把字节复制到Guid中
            //Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            //Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            //daysArray = null;
            //msecsArray = null;
            //var result = new Guid(guidArray);
            //return result;

            var guidString = dateTime.ToString("yyyyMMddHHmmssfffffff") + guidkey++.ToString("00000000000");
            //var guidString = dateTime.ToString("yyyyMMddHHmmssfffffff") + "00000000000";
            var result = Guid.Parse(guidString);
            return result;
        }

        #endregion

        #region Enum

        /// <summary>
        /// 获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="enumeration"> </param>
        /// <returns> </returns>
        public static string ToDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var members = type.GetMember(enumeration.CastTo<string>());
            if (members.Length > 0)
            {
                return members[0].ToDescription();
            }
            return enumeration.CastTo<string>();
        }

        #endregion

        #region Guid

        /// <summary>
        /// 从SQL Server 返回的Guid中生成时间信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Guid guid)
        {
            var baseDate = new DateTime(1900, 1, 1);
            var daysArray = new byte[4];
            var msecsArray = new byte[4];
            var guidArray = guid.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            var days = BitConverter.ToInt32(daysArray, 0);
            var msecs = BitConverter.ToInt32(msecsArray, 0);

            var date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);
            daysArray = null;
            msecsArray = null;
            guidArray = null;
            return date;
        }

        #endregion

        #region IQueryable

        /// <summary>
        /// 把IQueryable[T]集合按指定属性与排序方式进行排序
        /// </summary>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <typeparam name="T">动态类型</typeparam>
        /// <returns>排序后的数据集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            // PublicHelper.CheckArgument(propertyName, "propertyName");
            return QueryableHelper<T>.OrderBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把IQueryable[T]集合按指定属性排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表属性排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, PropertySortCondition sortCondition)
        {
            // PublicHelper.CheckArgument(sortCondition, "sortCondition");
            return source.OrderBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
        }

        #region 辅助操作类

        private static class QueryableHelper<T>
        {
            private static readonly ConcurrentDictionary<string, LambdaExpression> Cache = new ConcurrentDictionary<string, LambdaExpression>();

            internal static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string propertyName,
                ListSortDirection sortDirection)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return sortDirection == ListSortDirection.Ascending
                    ? Queryable.OrderBy(source, keySelector)
                    : Queryable.OrderByDescending(source, keySelector);
            }

            internal static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string propertyName,
                ListSortDirection sortDirection)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return sortDirection == ListSortDirection.Ascending
                    ? Queryable.ThenBy(source, keySelector)
                    : Queryable.ThenByDescending(source, keySelector);
            }

            private static LambdaExpression GetLambdaExpression(string propertyName)
            {
                if (Cache.ContainsKey(propertyName))
                {
                    return Cache[propertyName];
                }
                var param = Expression.Parameter(typeof(T));
                var body = Expression.Property(param, propertyName);
                var keySelector = Expression.Lambda(body, param);
                Cache[propertyName] = keySelector;
                return keySelector;
            }
        }

        #endregion

        #endregion


        #region



        #endregion









    }

}
