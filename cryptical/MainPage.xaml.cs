using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Cryptical
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
 

            hamburgerMenuControl.ItemsSource = MenuItem.GetMainItems();
            hamburgerMenuControl.OptionsItemsSource = MenuItem.GetOptionsItems();
            contentFrame.Navigate(typeof(View.CurrencyPage));
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            contentFrame.Navigate(menuItem.PageType);
        }
    }

    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public Type PageType { get; set; }

        public static List<MenuItem> GetMainItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem() { Icon = Symbol.Accept, Name = "MenuItem1", PageType = typeof(View.CurrencyPage) });
            items.Add(new MenuItem() { Icon = Symbol.Send, Name = "MenuItem2", PageType = typeof(View.MapPage) });
            items.Add(new MenuItem() { Icon = Symbol.Shop, Name = "MenuItem3", PageType = typeof(View.BlankPage1) });
            return items;
        }

        public static List<MenuItem> GetOptionsItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem() { Icon = Symbol.Setting, Name = "OptionItem1", PageType = typeof(View.CurrencyPage) });
            return items;
        }
    }
}