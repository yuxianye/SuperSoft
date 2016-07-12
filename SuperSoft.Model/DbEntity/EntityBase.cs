using SuperSoft.Utility;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuperSoft.Model
{
    /// <summary>
    /// 可持久到数据库的领域模型的基类。
    /// 子类的字段名称类型和数据库中的表字段一一对应
    /// </summary>
    [Serializable]
    public abstract class EntityBase<TKey> : MyNotifyClassBase
    {
        #region 构造函数

        /// <summary>
        /// 数据实体基类
        /// </summary>
        protected EntityBase()
        {
        }

        #endregion

        #region 属性

        [Key]
        public TKey Id { get; set; }

        #endregion
    }
}
