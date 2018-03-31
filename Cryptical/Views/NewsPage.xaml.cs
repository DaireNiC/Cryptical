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
        public NewsPage()
        {
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
            .Build());

                // here's the first 20
                foreach (INewsArticle article in newsArticles)
                {
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
