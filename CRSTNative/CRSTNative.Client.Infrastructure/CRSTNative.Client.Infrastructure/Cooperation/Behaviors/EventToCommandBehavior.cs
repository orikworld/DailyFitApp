using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Cooperation.Behaviors
{
    /// <summary>
    /// EventToCommandBehavior
    /// </summary>
    public class EventToCommandBehavior : BaseBehavior<VisualElement>
    {

        #region Private Fields

        /// <summary>
        /// The handler
        /// </summary>
        private Delegate _handler;

        /// <summary>
        /// The event information
        /// </summary>
        private EventInfo _eventInfo;

        #endregion

        #region Bindable Properties

        /// <summary>
        /// The event name property
        /// </summary>
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(
            nameof(EventName), typeof(string), typeof(EventToCommandBehavior));

        /// <summary>
        /// The command property
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        /// <summary>
        /// The command parameter property
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameterProperty), typeof(object), typeof(EventToCommandBehavior));

        /// <summary>
        /// The event arguments convertor property
        /// </summary>
        public static readonly BindableProperty EventArgsConverterProperty = BindableProperty.Create(
            nameof(EventArgsConverterProperty), typeof(IValueConverter), typeof(EventToCommandBehavior));

        /// <summary>
        /// The event arguments converter parameter property
        /// </summary>
        public static readonly BindableProperty EventArgsConverterParameterProperty = BindableProperty.Create(
            nameof(EventArgsConverterParameterProperty), typeof(object), typeof(EventToCommandBehavior));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the event arguments convertor.
        /// </summary>
        public IValueConverter EventArgsConverter
        {
            get => (IValueConverter) GetValue(EventArgsConverterProperty);
            set => SetValue(EventArgsConverterProperty, value);
        }

        /// <summary>
        /// Gets or sets the event arguments converter parameter.
        /// </summary>
        public object EventArgsConverterParameter
        {
            get => GetValue(EventArgsConverterParameterProperty);
            set => SetValue(EventArgsConverterParameterProperty, value);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when [attached to].
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <exception cref="System.ArgumentException"></exception>
        protected override void OnAttachedTo(VisualElement visualElement)
        {
            base.OnAttachedTo(visualElement);

            _eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);

            if (_eventInfo == null)
            {
                throw new ArgumentException($"EventToCommand: Can't find any event named '{EventName}' on attached type");
            }

            AddEventHandler(_eventInfo, AssociatedObject);
        }

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bindable object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(VisualElement bindable)
        {
            if (_handler != null)
            {
                _eventInfo.RemoveEventHandler(AssociatedObject, _handler);
            }

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds the event handler.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="item">The item.</param>
        private void AddEventHandler(EventInfo eventInfo, object item)
        {
            var methodInfo = GetType().GetTypeInfo().GetDeclaredMethod(nameof(OnFired));
            _handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);

            eventInfo.AddEventHandler(item, _handler);
        }

        /// <summary>
        /// Called when [fired].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command != null)
            {
                var parameter = CommandParameter;

                if (parameter == null && eventArgs != null && eventArgs != EventArgs.Empty)
                {
                    parameter = eventArgs;

                    if (EventArgsConverter != null)
                    {
                        parameter = EventArgsConverter.Convert(
                            eventArgs, typeof(object), EventArgsConverterParameter, CultureInfo.InvariantCulture);
                    }
                }

                if (Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
            }
        }


        #endregion
    }
}
