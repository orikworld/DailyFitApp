using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Controls.Ovverides
{
    public class Repeater<T> : StackLayout
    {
        #region Bindable

        public static readonly BindableProperty TakeFirstAsDefaultProperty = BindableProperty.Create(
            nameof(TakeFirstAsDefault),
            typeof(bool),
            typeof(Repeater<T>),
            true);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<T>),
            typeof(Repeater<T>),
            propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(T),
            typeof(Repeater<T>),
            default(T),
            BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(Repeater<T>));

        public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
            nameof(ItemSelectedCommand),
            typeof(ICommand),
            typeof(Repeater<T>));

        public static readonly BindableProperty DisabledItemsProperty = BindableProperty.Create(
            nameof(DisabledItems),
            typeof(IEnumerable<T>),
            typeof(Repeater<T>));

        public static readonly BindableProperty IsChildrenRefreshedProperty = BindableProperty.Create(
            nameof(IsChildrenRefreshed),
            typeof(bool),
            typeof(Repeater<T>),
            false,
            propertyChanged: NeedToRefreshChanged);

        public static readonly BindableProperty AttachGestureRecognizerProperty = BindableProperty.Create(
            nameof(AttachGestureRecognizer),
            typeof(bool),
            typeof(Repeater<T>),
            true);

        public static readonly BindableProperty SeparatorStyleProperty = BindableProperty.Create(
            nameof(SeparatorStyle),
            typeof(Style),
            typeof(Repeater<T>));

        #endregion

        #region Private Fields

        private ObservableCollection<T> _observableSource;

        #endregion

        #region Events

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        #endregion

        #region Constructor

        public Repeater()
        {
            DefaultItemSelectedCommand = new Command<T>(DefaultItemSelectedCommandExecute,
                item => !DisabledItems?.Contains(item) ?? true);
        }

        #endregion

        #region Public Properties

        public IEnumerable<T> ItemsSource
        {
            get => (IEnumerable<T>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

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

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public IEnumerable<T> DisabledItems
        {
            get => (IEnumerable<T>)GetValue(DisabledItemsProperty);
            set => SetValue(DisabledItemsProperty, value);
        }

        public bool IsChildrenRefreshed
        {
            get => (bool)GetValue(IsChildrenRefreshedProperty);
            set => SetValue(IsChildrenRefreshedProperty, value);
        }

        public bool AttachGestureRecognizer
        {
            get => (bool)GetValue(AttachGestureRecognizerProperty);
            set => SetValue(AttachGestureRecognizerProperty, value);
        }

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

        public bool HasEvenChildren { get; set; }

        public Style SeparatorStyle
        {
            get => (Style)GetValue(SeparatorStyleProperty);
            set => SetValue(SeparatorStyleProperty, value);
        }

        public bool UseSeparator { get; set; }

        protected ICommand DefaultItemSelectedCommand { get; set; }

        public bool TakeFirstAsDefault
        {
            get => (bool)GetValue(TakeFirstAsDefaultProperty);
            set => SetValue(TakeFirstAsDefaultProperty, value);
        }

        #endregion

        #region Commands

        public ICommand ItemSelectedCommand
        {
            get => (ICommand)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

        #endregion

        #region StackLayout overrides 

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

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!Equals(oldValue, newValue))
            {
                var itemsLayout = (Repeater<T>)bindable;
                itemsLayout.SetItems();
            }
        }

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

        protected void InitializeObservableSource()
        {
            var type = ItemsSource.GetType();
            var isObs = type.IsConstructedGenericType && (type.GetGenericTypeDefinition() == typeof(ObservableCollection<>));

            if (isObs)
            {
                ObservableSource = ItemsSource as ObservableCollection<T>;
            }
        }

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

        protected virtual void SetSelectedItem(object selectedItem, object oldSelectedItem)
        {
            SelectedItemChanged?.Invoke(this, new SelectedItemChangedEventArgs(selectedItem));
        }

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

        private void DefaultItemSelectedCommandExecute(T item)
        {
            if (AttachGestureRecognizer && (ItemSelectedCommand?.CanExecute(item) ?? false))
            {
                ItemSelectedCommand.Execute(item);
            }

            SelectedItem = item;
        }

        private void RefreshChildren()
        {
            var items = new List<View>(Children);
            Children.Clear();

            foreach (var item in items)
            {
                Children.Add(item);
            }
        }

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
