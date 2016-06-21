using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    /// <summary>
    /// 可持久到数据库的领域模型的基类。
    /// 子类的字段名称类型和数据库中的表字段一一对应
    /// </summary>
    [Serializable]
    public abstract class EntityBase<TKey> : MyClassBase
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
