using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;

namespace XFItemsControl
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }

        private void SetVerticalStackLayout_Clicked(object sender, EventArgs e)
        {
            _itemsControl.ItemsLayout = (DataTemplate)Resources["VerticalStackLayoutTemplate"];
        }

        private void SetHorizontalStackLayout_Clicked(object sender, EventArgs e)
        {
            _itemsControl.ItemsLayout = (DataTemplate)Resources["HorizontalStackLayoutTemplate"];
        }

        private void SetWrapLayout_Clicked(object sender, EventArgs e)
        {
            _itemsControl.ItemsLayout = (DataTemplate)Resources["WrapLayoutTemplate"];
        }
    }

    class MainViewModel
    {
        public IEnumerable Items { get => Enumerable.Range(0, 100); }
    }
}
