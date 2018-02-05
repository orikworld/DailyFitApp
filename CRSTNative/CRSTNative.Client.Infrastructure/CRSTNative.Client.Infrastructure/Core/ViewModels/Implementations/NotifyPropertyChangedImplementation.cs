using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CRSTNative.Client.Infrastructure.Core.ViewModels.Implementations
{
    /// <summary>
    /// NotifyPropertyChangedImplementation
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NotifyPropertyChangedImplementation : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region PropertyChanged Implementation

        /// <summary>
        /// Checks whether new value is supplied, and in that case updates property
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="field">Property field</param>
        /// <param name="value">Value to be set</param>
        /// <param name="propertyName">PropertyName to raise PropertyChanged event</param>
        /// <returns>True, in case property changed</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            SendPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Checks whether new value is supplied, and in that case updates property
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="field">Property field</param>
        /// <param name="value">Value to be set</param>
        /// <param name="propertyExpression">The expression that represents the property</param>
        /// <returns>True, in case property changed</returns>
        protected bool SetProperty<T>(ref T field, T value, Expression<Func<object>> propertyExpression)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            SendPropertyChanged(propertyExpression);

            return true;
        }

        /// <summary>
        /// Used for raising PropertyChanged event via Labmda expression.
        /// </summary>
        /// <param name="propertyExpression">The expression that represents the property.</param>
        protected void SendPropertyChanged(Expression<Func<object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            var body = lambda.Body as UnaryExpression;

            var memberExpression = body != null
                ? body.Operand as MemberExpression
                : lambda.Body as MemberExpression;

            var propertyInfo = memberExpression?.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                return;
            }

            var propertyName = propertyInfo.Name;

            SendPropertyChanged(propertyName);
        }

        /// <summary>
        /// Raise PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that was changed</param>
        protected void SendPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        #endregion
    }
}
