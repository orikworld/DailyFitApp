using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DailyFitNative.Infrastructure.Core.ViewModels.Implementations
{
    public class NotifyPropertyChangedImplementation : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region PropertyChanged Implementation

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

        protected void SendPropertyChanged(Expression<Func<object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;

	        var memberExpression = lambda.Body is UnaryExpression body
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

        protected void SendPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
