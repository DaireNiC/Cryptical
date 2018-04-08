using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Cryptical.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrencyPage : Page
    {
        public CurrencyPage()
        {
            this.InitializeComponent();
            //initialise 
            List<Coin> cryptoCoins = initCoins();
            cryptoCoins = getCoinData(cryptoCoins);
        }


        /* Sends request to API for latest cryptocurrency values
  * Refreshes & gets katest vals every minute
  * Returns the list of coins populated with the most recent data from API */
        private List<Coin> getCoinData(List<Coin> cryptoCoins)
        {
            //init HTTP client
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                // API base URL for request
                client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");

                // Retreive & add data to each specific coin
                foreach (Coin c in cryptoCoins)
                {
                    // HTTP request
                    // Ensure 200 OK
                    HttpResponseMessage response = client.GetAsync(String.Format
                        ("pricemultifull?fsyms={0}&tsyms=EUR", c.name)).Result;
                    response.EnsureSuccessStatusCode();
                    //response from API
                    string result = response.Content.ReadAsStringAsync().Result;
                    //Jtoken to access specific part of JSON result
                    JToken token = JObject.Parse(result);
                    Debug.WriteLine("Result: " + result);
                    //  c.price = double.Parse(token.SelectToken(String.Format("RAW.{}.EUR.PRICE", c.name) ).ToString());
                    //   c.price = double.Parse(token.SelectToken("PRICE").ToString());
                    c.price = (double)double.Parse(token.SelectToken(String.Format("RAW.{0}.EUR.PRICE", c.name)).ToString());
                    c.high24hr = (double)double.Parse(token.SelectToken(String.Format("RAW.{0}.EUR.HIGH24HOUR", c.name)).ToString());
                    c.low24hr = (double)double.Parse(token.SelectToken(String.Format("RAW.{0}.EUR.LOW24HOUR", c.name)).ToString());
                    // Populate textboxes to display to user
                    switch (c.name)
                    {
                        case "ETH":
                            ETHPriceText.Text = String.Format("{0:0.00}", c.price);
                            ETHhigh24hr.Text = String.Format("24 Hour High: {0:0.00}", c.high24hr);
                            ETHlow24hr.Text = String.Format("24 Hour Low: {0:0.00}", c.low24hr);
                            break;
                        case "BTC":
                            BTCPriceText.Text = String.Format("{0:0.00}", c.price);
                            BTChigh24hr.Text = String.Format("24 Hour High: {0:0.00}", c.high24hr);
                            BTClow24hr.Text = String.Format("24 Hour Low: {0:0.00}", c.low24hr);
                            break;
                        case "BCH":
                            BCHPriceText.Text = String.Format("{0:0.00}", c.price);
                            BCHhigh24hr.Text = String.Format("24 Hour High: {0:0.00}", c.high24hr);
                            BCHlow24hr.Text = String.Format("24 Hour Low: {0:0.00}", c.low24hr);
                            break;
                        case "LTC":
                            LTCPriceText.Text = String.Format("{0:0.00}", c.price);
                            LTChigh24hr.Text = String.Format("24 Hour High: {0:0.00}", c.high24hr);
                            LTClow24hr.Text = String.Format("24 Hour Low: {0:0.00}", c.low24hr);
                            break;
                        default:
                            break;
                    }

                }
            }
            return cryptoCoins;
        }


        /* Create coin objects to hold crypto values
         * Only called once when entering the App */
        private List<Coin> initCoins()
        {
            // List to hold coin objects
            List<Coin> cryptoCoins = new List<Coin>();

            // Create Coins to represent top 4 cryptoCurrencies & add to list
            Coin BTC = new Coin("BTC", 0.0, 0.0, 0.0);
            cryptoCoins.Add(BTC);
            Coin ETH = new Coin("ETH", 0.0, 0.0, 0.0);
            cryptoCoins.Add(ETH);
            Coin BCH = new Coin("BCH", 0.0, 0.0, 0.0);
            cryptoCoins.Add(BCH);
            Coin LTC = new Coin("LTC", 0.0, 0.0, 0.0);
            cryptoCoins.Add(LTC);

            return cryptoCoins;
        }
    }
}
