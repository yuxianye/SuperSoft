using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public struct WaveLinePoint
    {
        public WaveLinePoint(DateTime dataTime, float value)
        {
            this.DataTime = dataTime;
            this.Value = value;
        }

        #region DataTime

        /// <summary>
        /// 数据的时间点
        /// </summary>
        public DateTime DataTime;

        #endregion

        /// <summary>
        /// 值
        /// </summary>
        public float Value;

        //public override string ToString()
        //{
        //    //  return base.ToString();
        //    return this.Value.ToString() + System.Environment.NewLine + this.DataTime.ToString();
        //}
    }

    /// <summary>
    /// 数据点的工具提示
    /// </summary>
    public struct WaveLinePointToolTip
    {
        public WaveLinePointToolTip(MyPoint point, float value)
        {
            this.Point = point;
            this.Value = value;
        }

        /// <summary>
        /// 鼠标位置的点值
        /// </summary>
        public MyPoint Point;

        ///// <summary>
        ///// 
        ///// </summary>
        //public WaveLinePoint WaveLinePoint;

        /// <summary>
        /// 
        /// </summary>
        public float Value;

    }

    /// <summary>
    /// 用于数据点的值
    /// </summary>
    public struct MyPoint
    {

        public MyPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X;

        public double Y;

    }

}
