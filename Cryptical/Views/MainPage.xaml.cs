using System;
using System.Collections.Generic;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Cryptical.Views
{
    /// <summary>
    /// The MainPage handles the navigation throughout the app. 
    /// Once the splash screen has finished, the Mainpage constructs the menu for navigation 
    /// NavigationViewItems are displayed to the user as options in the menu bar
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // navigate to home page
            ContentFrame.Navigate(typeof(Views.CurrencyPage));
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
    
            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "prices")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }

            ContentFrame.Navigated += On_Navigated;
        }

        // Navigate to the page the user clicks on 
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(Views.SettingsPage));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }
            
        }

        //Switch for selected item
        private void NavView_Navigate(NavigationViewItem item)
        {
            //close the pane
            NavView.IsPaneOpen = false;

            switch (item.Tag)
            {
                case "prices":
                    ContentFrame.Navigate(typeof(Views.CurrencyPage));
                    break;

                case "news":
                    ContentFrame.Navigate(typeof(Views.NewsPage));
                    break;

                case "map":
                    ContentFrame.Navigate(typeof(Views.MapPage));
                    break;
            }
        }

        // When the naviaget
        private void On_Navigated(object sender, NavigationEventArgs e)
        {

            if (ContentFrame.SourcePageType == typeof(Views.SettingsPage))
            {
                NavView.SelectedItem = NavView.SettingsItem as NavigationViewItem;
            }
            else
            {
                //holds reference to all pages in the app accessible via menu
                Dictionary<Type, string> lookup = new Dictionary<Type, string>(){
                    {typeof(Views.CurrencyPage), "prices"},
                    {typeof(Views.NewsPage), "news"},
                    {typeof(Views.MapPage), "map"},
                };

                String stringTag = lookup[ContentFrame.SourcePageType];

                // set the new SelectedItem  
                foreach (NavigationViewItemBase item in NavView.MenuItems)
                {
                    if (item is NavigationViewItem && item.Tag.Equals(stringTag))
                    {
                        item.IsSelected = true;
                        break;
                    }
                }
            }
        }

        }

    }