using System.Collections;
using Xamarin.Forms;

namespace XFItemsControl
{
    class ItemsControl : ContentView
    {
        protected Layout<View> _itemsLayout;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ItemsControl), propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemsSourcePropertyChanged());

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsControl), propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemTemplatePropertyChanged());

        public static readonly BindableProperty ItemsLayoutProperty =
            BindableProperty.Create(nameof(ItemsLayout), typeof(DataTemplate), typeof(ItemsControl), propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemsLayoutPropertyChanged());

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplate ItemsLayout
        {
            get => (DataTemplate)GetValue(ItemsLayoutProperty);
            set => SetValue(ItemsLayoutProperty, value);
        }

        protected virtual View CreateItem(object item)
        {
            if (ItemTemplate == null)
            {
                return new Label() { Text = item.ToString() };
            }
            else
            {
                var itemView = (View)ItemTemplate.CreateContent();
                itemView.BindingContext = item;
                return itemView;
            }
        }

        protected virtual void CreateItemsLayout()
        {
            Content = ItemsLayout != null ?
                (Layout)ItemsLayout.CreateContent() :
                new StackLayout()
                {
                    Spacing = 0
                };

            if (Content is Layout<View> viewLayout)
            {
                _itemsLayout = viewLayout;
            }
            else
            {
                _itemsLayout = FindItemsHost(Content);
            }
        }

        void OnItemsSourcePropertyChanged()
        {
            if (_itemsLayout == null)
            {
                CreateItemsLayout();
            }

            _itemsLayout.Children.Clear();

            if (ItemsSource != null)
            {
                foreach (object item in ItemsSource)
                {
                    _itemsLayout.Children.Add(CreateItem(item));
                }
            }
        }

        void OnItemTemplatePropertyChanged()
        {
            if (_itemsLayout == null)
            {
                return;
            }

            OnItemsSourcePropertyChanged();
        }

        void OnItemsLayoutPropertyChanged()
        {
            CreateItemsLayout();

            OnItemsSourcePropertyChanged();
        }

        Layout<View> FindItemsHost(View currView)
        {
            if (currView is Layout<View> viewLayout && LayoutEx.GetIsItemsHost(viewLayout))
            {
                return viewLayout;
            }
            else
            {
                if (currView is Layout layoutView)
                {
                    foreach (Element e in layoutView.Children)
                    {
                        Layout<View> itemsHost = FindItemsHost((View)e);
                        if (itemsHost != null)
                        {
                            return itemsHost;
                        }
                    }
                }

                return null;
            }
        }
    }
}
