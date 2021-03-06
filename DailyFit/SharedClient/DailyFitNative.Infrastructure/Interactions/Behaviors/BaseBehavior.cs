using System;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Interactions.Behaviors
{
    public abstract class BaseBehavior<T>: Behavior<T> where T : BindableObject
    {
        #region Properties

        public T AssociatedObject { get; private set; }

        #endregion

        #region Protected Methods

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        #endregion

        #region Private Methods

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        #endregion
    }
}
