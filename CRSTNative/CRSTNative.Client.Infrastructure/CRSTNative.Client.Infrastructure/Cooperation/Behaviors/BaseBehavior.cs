using System;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Cooperation.Behaviors
{
    /// <summary>
    /// BaseBehavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Xamarin.Forms.Behavior{T}" />
    public abstract class BaseBehavior<T>: Behavior<T> where T : BindableObject
    {
        #region Properties

        /// <summary>
        /// Gets the associated object.
        /// </summary>

        public T AssociatedObject { get; private set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method on this object.
        /// </summary>
        /// <param name="bindable">The bindable object to which the behavior was attached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
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

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bindable object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        /// <summary>
        /// Override this method to execute an action when the BindingContext changes.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [binding context changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        #endregion
    }
}
