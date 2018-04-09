using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Cryptical.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            hamburgerMenuControl.ItemsSource = MenuItem.GetMainItems();
            contentFrame.Navigate(typeof(Views.NewsPage));
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
            //load the language resources
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            string MenuPrice = resourceLoader.GetString("Menu-Prices");
            items.Add(new MenuItem() { Icon = Symbol.Globe, Name = MenuPrice, PageType = typeof(Views.CurrencyPage) });

            string MenuNews = resourceLoader.GetString("Menu-News");
            items.Add(new MenuItem() { Icon = Symbol.BrowsePhotos, Name = MenuNews, PageType = typeof(Views.NewsPage) });

            string MenuPay = resourceLoader.GetString("Menu-Pay");
            items.Add(new MenuItem() { Icon = Symbol.MapPin, Name = MenuPay, PageType = typeof(Views.MapPage) });
            return items;
        }

    }

}