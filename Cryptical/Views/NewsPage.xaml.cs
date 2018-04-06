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

/* News Api in use: https://newsapi.org/
 * News APi wrapper for .NET :  https://github.com/hassie-dash/NewsAPI.NET
*/
namespace Cryptical.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsPage : Page

    {
        private ObservableCollection<NewsItem> newsItems = new ObservableCollection<NewsItem>();
        public ObservableCollection<NewsItem> NewsItems { get { return this.newsItems; } }

        public NewsPage()
        {
           // LoadNewsItems();
            this.InitializeComponent();
            Debug.WriteLine("init componnt executed");
            Loaded += NewsPage_LoadedAsync;
        }

        //on page load...
        private async void NewsPage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("news page method");
            //first get top 20 news articles relating to crypto currencies
            await InitializeAsync();
        }
     /*   public void LoadNewsItems() {

            for (int i = 1; i < 10; i++)
            {
                this.newsItems.Add(new NewsItem()
                {
                    Title = "Title: " + i.ToString(),
                    Subtitle = "Sub: " + i.ToString(),
                    Description = "Desc: " + i.ToString()
                });
            }
        }
        */
            
        public async Task InitializeAsync()
        {
            Debug.WriteLine("init method");
           
            INewsClient newsClient = new ClientBuilder()
            {
                ApiKey = "5fe4eb7f64af4875a48ab4ea5e50e875" 
            }.Build();

            //todo: improve query
            INewsArticles newsArticles = await newsClient.GetEverything(new EverythingBuilder()
            .WithSearchQuery("bitcoin")
            .WithSortOrder(SortOrder.PUBLISHED_AT)
            .WithLanguageQuery(Hassie.NET.API.NewsAPI.API.v2.Language.EN)
            .Build());

                // here's the first 20
                foreach (INewsArticle article in newsArticles)
                {
                    this.newsItems.Add(new NewsItem()
                    {
                        Title = article.Title,
                        Subtitle = article.Author,
                        Description = article.Description
                    });
                // title
                Debug.WriteLine(article.Title);
                    // author
                    Debug.WriteLine(article.Author);
                    // description
                    Debug.WriteLine(article.Description);
                    // url
                    Debug.WriteLine(article.URL);
                    // image
                    Debug.WriteLine(article.ImageURL);
                    // published at
                    Debug.WriteLine(article.PublishTime);
                }

        
        }

    }
}
