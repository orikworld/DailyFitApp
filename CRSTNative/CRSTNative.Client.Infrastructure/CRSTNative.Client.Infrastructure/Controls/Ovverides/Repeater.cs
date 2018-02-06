using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Controls.Ovverides
{
    public class Repeater<T> : StackLayout
    {
        #region Bindable

        /// <summary>
        /// The take first as default property
        /// </summary>
        public static readonly BindableProperty TakeFirstAsDefaultProperty = BindableProperty.Create(
            nameof(TakeFirstAsDefault),
            typeof(bool),
            typeof(Repeater<T>),
            true);

        /// <summary>
        /// Bindable ItemsSource property
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<T>),
            typeof(Repeater<T>),
            propertyChanged: ItemsSourceChanged);

        /// <summary>
        /// Bindable SelectedItem property
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(T),
            typeof(Repeater<T>),
            default(T),
            BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

        /// <summary>
        /// Bindable ItemTemplate Property
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(Repeater<T>));

        /// <summary>
        /// Bindable ItemSelectedCommand
        /// </summary>
        public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
            nameof(ItemSelectedCommand),
            typeof(ICommand),
            typeof(Repeater<T>));

        /// <summary>
        /// Items that won't trigger ItemSelectedCommand and SelectedItemChanged event 
        /// </summary>
        public static readonly BindableProperty DisabledItemsProperty = BindableProperty.Create(
            nameof(DisabledItems),
            typeof(IEnumerable<T>),
            typeof(Repeater<T>));

        /// <summary>
        /// The need to refresh property
        /// </summary>
        public static readonly BindableProperty IsChildrenRefreshedProperty = BindableProperty.Create(
            nameof(IsChildrenRefreshed),
            typeof(bool),
            typeof(Repeater<T>),
            false,
            propertyChanged: NeedToRefreshChanged);

        /// <summary>
        /// The attach gesture recognizer property
        /// </summary>
        public static readonly BindableProperty AttachGestureRecognizerProperty = BindableProperty.Create(
            nameof(AttachGestureRecognizer),
            typeof(bool),
            typeof(Repeater<T>),
            true);

        /// <summary>
        /// The separator style property
        /// </summary>
        public static readonly BindableProperty SeparatorStyleProperty = BindableProperty.Create(
            nameof(SeparatorStyle),
            typeof(Style),
            typeof(Repeater<T>));

        #endregion

        #region Private Fields

        /// <summary>
        /// Holds observable collection source
        /// </summary>
        private ObservableCollection<T> _observableSource;

        #endregion

        #region Events

        /// <summary>
        /// SelectedItemChanged event declaration
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Repeater()
        {
            DefaultItemSelectedCommand = new Command<T>(DefaultItemSelectedCommandExecute,
                item => !DisabledItems?.Contains(item) ?? true);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// .NET ItemSource property over Bindable ItemSourceProperty
        /// </summary>
        public IEnumerable<T> ItemsSource
        {
            get => (IEnumerable<T>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// .NET SelectedItem property over Bindable SelectedItemProperty
        /// </summary>
        public T SelectedItem
        {
            get => (T)GetValue(SelectedItemProperty);
            set
            {
                if (AttachGestureRecognizer)
                {
                    SetValue(SelectedItemProperty, value);
                }
            }

        }

        /// <summary>
        /// .NET ItemTemplate property over Bindable ItemTemplateProperty
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// .NET DisabledItems property over Bindable DisabledItemsProperty
        /// </summary>
        public IEnumerable<T> DisabledItems
        {
            get => (IEnumerable<T>)GetValue(DisabledItemsProperty);
            set => SetValue(DisabledItemsProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [need to refresh].
        /// </summary>
        public bool IsChildrenRefreshed
        {
            get => (bool)GetValue(IsChildrenRefreshedProperty);
            set => SetValue(IsChildrenRefreshedProperty, value);
        }

        /// <summary>
        /// Identifies whether GestureRecogniser should be attach to the item
        /// </summary>
        public bool AttachGestureRecognizer
        {
            get => (bool)GetValue(AttachGestureRecognizerProperty);
            set => SetValue(AttachGestureRecognizerProperty, value);
        }

        /// <summary>
        /// ObservableSource property
        /// </summary>
        protected ObservableCollection<T> ObservableSource
        {
            get => _observableSource;
            set
            {
                if (_observableSource != null)
                {
                    _observableSource.CollectionChanged -= CollectionChanged;
                }

                _observableSource = value;

                if (_observableSource != null)
                {
                    _observableSource.CollectionChanged += CollectionChanged;
                }
            }
        }

        /// <summary>
        /// Identifies whether repeater has even children
        /// </summary>
        public bool HasEvenChildren { get; set; }

        /// <summary>
        /// Gets or sets the bottom style.
        /// </summary>
        public Style SeparatorStyle
        {
            get => (Style)GetValue(SeparatorStyleProperty);
            set => SetValue(SeparatorStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use separator].
        /// </summary>
        public bool UseSeparator { get; set; }

        /// <summary>
        /// Holds defaultItemSelected command to set SelectedItem and then execute ItemSelectedCommand
        /// </summary>
        protected ICommand DefaultItemSelectedCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [take first as default].
        /// </summary>
        public bool TakeFirstAsDefault
        {
            get => (bool)GetValue(TakeFirstAsDefaultProperty);
            set => SetValue(TakeFirstAsDefaultProperty, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// SelectedItem command
        /// </summary>
        public ICommand ItemSelectedCommand
        {
            get => (ICommand)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

        #endregion

        #region StackLayout overrides 

        /// <summary>
        /// Layouts children - all child will have equal width
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width allowed</param>
        /// <param name="height">Height allowed</param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (HasEvenChildren && Children.Any())
            {
                var averageWidth = width / Children.Count;
                var startXPoint = x;

                foreach (var child in Children)
                {
                    child.Layout(new Rectangle(startXPoint, y, averageWidth, height));
                    startXPoint += averageWidth;
                }
            }
            else
            {
                base.LayoutChildren(x, y, width, height);
            }
        }

        #endregion

        #region PropertyChanged handlers

        /// <summary>
        /// ItemSourceChanged event handler
        /// </summary>
        /// <param name="bindable">Bindable object that triggers event</param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!Equals(oldValue, newValue))
            {
                var itemsLayout = (Repeater<T>)bindable;
                itemsLayout.SetItems();
            }
        }

        /// <summary>
        /// SelectedItemChanged event handler
        /// </summary>
        /// <param name="bindable">Bindable object that triggers event</param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!Equals(oldValue, newValue))
            {
                var itemsView = (Repeater<T>)bindable;
                itemsView.SetSelectedItem(newValue, oldValue);
            }
        }

        #endregion

        #region Items Rendering

        /// <summary>
        /// SetItems into StackLayout Children property
        /// </summary>
        protected virtual void SetItems()
        {
            Children.Clear();

            if (ItemsSource == null)
            {
                ObservableSource = null;

                return;
            }

            foreach (var item in ItemsSource)
            {
                var view = GetItemView(item, ItemTemplate);

                Children.Add(view);

                if (UseSeparator && !item.Equals(ItemsSource.Last()))
                {
                    Children.Add(new BoxView
                    {
                        Style = SeparatorStyle
                    });
                }
            }

            if (TakeFirstAsDefault && ItemsSource.Any() && SelectedItem == null)
            {
                SelectedItem = ItemsSource.First();
            }

            InitializeObservableSource();
        }

        /// <summary>
        /// Initialize ObservableSource in case ItemSource is observable collection
        /// </summary>
        protected void InitializeObservableSource()
        {
            var type = ItemsSource.GetType();
            var isObs = type.IsConstructedGenericType && (type.GetGenericTypeDefinition() == typeof(ObservableCollection<>));

            if (isObs)
            {
                ObservableSource = ItemsSource as ObservableCollection<T>;
            }
        }

        /// <summary>
        /// Applies ItemTemplate to provided value 
        /// </summary>
        /// <param name="item">Provided Value</param>
        /// <param name="dataTemplate">Data Template</param>
        /// <returns>Applied ItemTemplate</returns>
        protected virtual View GetItemView(object item, DataTemplate dataTemplate)
        {
            var content = dataTemplate.CreateContent();

            var view = content as View;

            if (view == null)
            {
                return null;
            }

            view.BindingContext = item;

            if (AttachGestureRecognizer)
            {
                var gesture = new TapGestureRecognizer
                {
                    Command = DefaultItemSelectedCommand,
                    CommandParameter = item
                };

                view.GestureRecognizers.Add(gesture);
            }

            return view;
        }


        /// <summary>
        /// Set selected item and triggers SelectedItemChanged event
        /// </summary>
        /// <param name="selectedItem">Selected item</param>
        /// <param name="oldSelectedItem">Old selected item</param>
        protected virtual void SetSelectedItem(object selectedItem, object oldSelectedItem)
        {
            SelectedItemChanged?.Invoke(this, new SelectedItemChangedEventArgs(selectedItem));
        }

        /// <summary>
        /// CollectionChanged handler
        /// </summary>
        /// <param name="sender">Sender(Collection that was changed)</param>
        /// <param name="e">Event args</param>
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int index = e.NewStartingIndex;

                        foreach (var item in e.NewItems.Cast<T>())
                        {
                            Children.Insert(index++, GetItemView(item, ItemTemplate));
                        }

                        break;
                    }
                case NotifyCollectionChangedAction.Move:
                    {
                        var item = ObservableSource[e.OldStartingIndex];

                        Children.RemoveAt(e.OldStartingIndex);
                        Children.Insert(e.NewStartingIndex, GetItemView(item, ItemTemplate));

                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        //var childToRemover = Children[e.OldStartingIndex];
                        //await childToRemover.TranslateTo(-childToRemover.Width, childToRemover.AnchorY, ControlsConstants.SWIPE_ANIMATION_TIME);

                        Children.RemoveAt(e.OldStartingIndex);

                        break;
                    }
                case NotifyCollectionChangedAction.Replace:
                    {
                        Children.RemoveAt(e.OldStartingIndex);
                        Children.Insert(e.NewStartingIndex, GetItemView(ObservableSource[e.NewStartingIndex], ItemTemplate));

                        break;
                    }
                case NotifyCollectionChangedAction.Reset:
                    {
                        //var aimationTasks = new List<Task>();

                        //foreach (var child in Children)
                        //{
                        //    aimationTasks.Add(child.TranslateTo(-child.Width, child.AnchorY, ControlsConstants.SWIPE_ANIMATION_TIME));
                        //}

                        //await Task.WhenAll(aimationTasks);

                        Children.Clear();

                        foreach (var item in ItemsSource)
                        {
                            Children.Add(GetItemView(item, ItemTemplate));
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Set selected item and then executes ItemSelectedCommand
        /// </summary>
        /// <param name="item">Selected Item</param>
        private void DefaultItemSelectedCommandExecute(T item)
        {
            if (AttachGestureRecognizer && (ItemSelectedCommand?.CanExecute(item) ?? false))
            {
                ItemSelectedCommand.Execute(item);
            }

            SelectedItem = item;
        }

        /// <summary>
        /// Refreshes the children.
        /// </summary>
        private void RefreshChildren()
        {
            var items = new List<View>(Children);
            Children.Clear();

            foreach (var item in items)
            {
                Children.Add(item);
            }
        }

        /// <summary>
        /// Needs to refresh changed.
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void NeedToRefreshChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                var itemsView = (Repeater<T>)bindable;
                itemsView.RefreshChildren();
            }
        }

        #endregion
    }
}
