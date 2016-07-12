using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace SuperSoft.Utility
{
    /// <summary>
    /// 普通自定义类都需要继承自此基类。
    /// 子类需要重写基类的DisposeManagedResources、DisposeUnmanagedResources方法。
    /// 然后在子类中调用Class.Dispose()方法释放资源，然后在使用Class=null
    /// 如果不显示调用Class.Dispose()和Class=null，那么有系统按需自动回收
    /// 使用范例
    /// public class MySampleClass : MyClassBase
    /// {
    /// object managedResources = new object();
    /// private IntPtr unmanagedResources = System.Runtime.InteropServices.Marshal.AllocHGlobal(100);
    /// protected override void DisposeManagedResources()
    /// {
    /// managedResources = null;
    /// base.DisposeManagedResources();
    /// }
    /// protected override void DisposeUnmanagedResources()
    /// {
    /// System.Runtime.InteropServices.Marshal.FreeHGlobal(unmanagedResources);
    /// base.DisposeUnmanagedResources();
    /// }
    /// }
    /// MySampleClass mySampleClass = new MySampleClass();
    /// mySampleClass.Dispose();
    /// mySampleClass = null;
    /// </summary>
    public abstract class MyClassBase : IDisposable
    {
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
        ~MyClassBase()
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

    /// <summary>
    /// 带通知的自定义类都需要继承自此基类。
    /// 子类需要重写基类的DisposeManagedResources、DisposeUnmanagedResources方法。
    /// 然后在子类中调用Class.Dispose()方法释放资源，然后在使用Class=null
    /// 如果不显示调用Class.Dispose()和Class=null，那么有系统按需自动回收
    /// 使用范例
    /// public class MySampleClass : MyClassBase
    /// {
    /// object managedResources = new object();
    /// private IntPtr unmanagedResources = System.Runtime.InteropServices.Marshal.AllocHGlobal(100);
    /// protected override void DisposeManagedResources()
    /// {
    /// managedResources = null;
    /// base.DisposeManagedResources();
    /// }
    /// protected override void DisposeUnmanagedResources()
    /// {
    /// System.Runtime.InteropServices.Marshal.FreeHGlobal(unmanagedResources);
    /// base.DisposeUnmanagedResources();
    /// }
    /// }
    /// MySampleClass mySampleClass = new MySampleClass();
    /// mySampleClass.Dispose();
    /// mySampleClass = null;
    /// </summary>
    public abstract class MyNotifyClassBase : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.(效率高)
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
                var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event.(效率低)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged(GetPropertyName(propertyExpression));
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(@"propertyExpression");
            }
            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException(@"Invalid argument", @"propertyExpression");
            }
            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException(@"Argument is not a property", @"propertyExpression");
            }
            return property.Name;
        }

        #endregion

        #region IDisposable

        // Flag for already disposed
        public bool Disposed { get; private set; }

        // finalizer:
        // Call the virtual Dispose method.
        ~MyNotifyClassBase()
        {
            Dispose(false);
        }

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

        // Virtual Dispose method
        private void Dispose(bool isDisposing)
        {
            // Don't dispose more than once.
            if (Disposed)
                return;
            if (isDisposing)
            {
                // free managed resources here.
                DisposeManagedResources();
            }
            // free unmanaged resources here.
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
