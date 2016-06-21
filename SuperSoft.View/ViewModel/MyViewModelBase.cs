using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.View.ViewModel
{
    /// <summary>
    /// 继承自 GalaSoft.MvvmLight ViewModelBase 和 IDisposable
    /// 子类重写DisposeManagedResources DisposeUnmanagedResources方法释放资源
    /// </summary>
    public class MyViewModelBase : ViewModelBase, IDisposable
    {
        private object parameter;
        public object Parameter
        {
            get { return parameter; }
            set
            {
                if (Equals(parameter, value)) return;
                parameter = value;
                if (Equals(parameter, null)) return;
                OnParameterChanged();
            }
        }

        protected virtual void OnParameterChanged()
        {

        }

        // Flag for already disposed
        protected bool Disposed { get; private set; }

        // Implementation of IDisposable.
        // Call the virtual Dispose method.
        // Suppress Finalization.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            GC.ReRegisterForFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // finalizer:
        // Call the virtual Dispose method.
        ~MyViewModelBase()
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
    }
}
