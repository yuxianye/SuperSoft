using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SuperSoft.View
{
    /// <summary>
    /// 用户控件的基类
    /// </summary>
    public class UserControlBase : UserControl, IDisposable
    {
        #region  释放资源

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            GC.ReRegisterForFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        protected bool Disposed { get; private set; }

        // finalizer:
        // Call the virtual Dispose method.
        ~UserControlBase()
        {
            Dispose(false);
        }
        // Virtual Dispose method
        private void Dispose(bool isDisposing)
        {
            // Don't dispose more than once.
            if (Disposed)
                return;
            if (isDisposing)
            {
                //  free managed resources here.
                DisposeManagedResources();
            }
            //  free unmanaged resources here.
            DisposeUnmanagedResources();
            // Set disposed flag:
            Disposed = true;
        }

        /// <summary>
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }
        #endregion
    }
}
