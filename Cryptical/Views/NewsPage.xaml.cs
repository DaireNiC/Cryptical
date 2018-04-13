using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using NewsAPI;

using NewsAPI.Constants;
using Hassie.NET.API.NewsAPI.Client;
using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Models;

using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace Cryptical.Views
{
    /// <summary>
    /// This Page loads the top 20 news articles relating bitcoin/cryptocurrencies
    /// It uses NewsApi along with the News APi wrapper for .NET.
    /// The user can click on any of the news articles and read them from the source website
    /// -------------------------------------------
    /// News Api in use: https://newsapi.org/
    /// News APi wrapper for .NET :  https://github.com/hassie-dash/NewsAPI.NET
    /// </summary>
    public sealed partial class NewsPage : Page

    {
        // News items are created using the repsonse from the news api request
        private ObservableCollection<NewsItem> newsItems = new ObservableCollection<NewsItem>();
        public ObservableCollection<NewsItem> NewsItems
        {
            get
            {
                return this.newsItems;
            }
        }


        public NewsPage()
        {
            this.InitializeComponent();
            Loaded += NewsPage_LoadedAsync;
        }

        //on page load...
        private async void NewsPage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            //first get top 20 news articles relating to crypto currencies
            try
            {
                //load news articles 
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                //write the exception
                Debug.WriteLine(ex);
                // Let user know articles failed to load
                ContentDialog articleloadFailure = new ContentDialog
                {
                    Title = "Error Loading News",
                    Content = "Check your internet connection and try again.",
                    PrimaryButtonText = "Refresh",
                    CloseButtonText = "Not now"
                };
                // Await user's option on dialogue box
                ContentDialogResult result = await articleloadFailure.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    // reload content
                    Loaded += NewsPage_LoadedAsync;
                }
            }
        }

        public async Task InitializeAsync()
        {
            // API key for NewsAPI
            INewsClient newsClient = new ClientBuilder()
            {
                ApiKey = "5fe4eb7f64af4875a48ab4ea5e50e875"
            }.Build();

            //News Query sent to NewsAPI
            // REturns top 20 articles relating to bitcoin
            INewsArticles newsArticles = await newsClient.GetEverything(new EverythingBuilder()
             .WithSearchQuery("bitcoin")
             .WithSearchQuery("cryptocurrency")
             .WithSortOrder(SortOrder.PUBLISHED_AT)
             .WithLanguageQuery(Hassie.NET.API.NewsAPI.API.v2.Language.EN)
             .Build());



            // The top 20 results
            foreach (INewsArticle article in newsArticles)
            {
                //Error handling : make sure there is an image to display
                if ((article.ImageURL != "") && (article.ImageURL != null))
                {

                    //  Uri uriImage = new Uri(article.ImageURL);
                    this.newsItems.Add(new NewsItem()
                    {
                        Title = article.Title,
                        Subtitle = article.SourceName,
                        Description = article.Description,
                        ImageURL = article.ImageURL,
                        URL = article.URL,
                        // create bitimap image from the url from response
                        ThumbImage = new Image
                        {
                            Source = new BitmapImage(new Uri(article.ImageURL, UriKind.Absolute))
                        }

                    });
                }


            }
        }

        // When the user clicks on an article they will be redirected to the website source
        private async void NewsItem_clickAsync(object sender, ItemClickEventArgs e)
        {
            //Cast the clicked item to its type
            NewsItem article = (NewsItem)e.ClickedItem;

            //open the source url in browser
            var uri = new Uri(article.URL);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            //If the article fails to open, handle the exception
            if (!success)
            {
                ContentDialog articleLaunchFailure = new ContentDialog
                {
                    Title = "Error Loading Article",
                    Content = "Check your internet connection and try again.",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await articleLaunchFailure.ShowAsync();
            }

        }
    }
}