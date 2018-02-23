using System;
using System.Net;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//Refernces : https://stackoverflow.com/questions/48067651/getting-errors-parsing-cryptocompare-api-json-data-in-c-sharp
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace cryptical
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Debug.WriteLine("Making API Call...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
                HttpResponseMessage response = client.GetAsync("price?fsym=ETH&tsyms=BTC,USD,EUR&extraParams=cyptical").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Result: " + result);
                JToken token = JObject.Parse(result);
                double price = double.Parse(token.SelectToken("EUR").ToString());
                Debug.WriteLine("Price: " + price);
                convertedTextbox.Text = String.Format("{0:0.00}", price);
            }
          //  Debug.ReadLine();

        }

        private void amountToConvert_TextChanged(object sender, TextChangedEventArgs e)
        {
            var exchangeRate = 1.2;
            var valToConvert = Convert.ToDouble(amountToConvert.Text);
            double convertedval = valToConvert * exchangeRate;

            if (amountToConvert.Text != null) {
                convertedTextbox.Text = String.Format("{0:0.00}", convertedval);
            }
           
        }
    }
}
