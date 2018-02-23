using System;
using System.Net;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
            //initialise 
            List<Coin> cryptoCoins = initCoins();
            cryptoCoins = getCoinData(cryptoCoins);
 
            //make call to API to get the latest cryptocurrency values
           // getLatestCurVal();
        }

        /*  private void amountToConvert_TextChanged(object sender, TextChangedEventArgs e)
          {
              var exchangeRate = 1.2;
         /     var valToConvert = Convert.ToDouble(amountToConvert.Text);
              double convertedval = valToConvert * exchangeRate;

              if (amountToConvert.Text != null) {
                  convertedTextbox.Text = String.Format("{0:0.00}", convertedval);
              }

          }
          */
        private void convertedTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //this method pulls the latest data from the cryptoCompare api
        private void getLatestCurVal() {
        /*    Debug.WriteLine("Making API Call...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
                //get ethereum value
                HttpResponseMessage response = client.GetAsync("price?fsym=ETH&tsyms=BTC,USD,EUR&extraParams=cyptical").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Result: " + result);
                JToken token = JObject.Parse(result);
                double price = double.Parse(token.SelectToken("EUR").ToString());
                Debug.WriteLine("Price: " + price);
                ethereumTextbox.Text = String.Format("{0:0.00}", price);
            }
            //  Debug.ReadLine();
            */
        }

        /* Sends request to API for latest cryptocurrency values
         * Refreshes & gets katest vals every minute
         * Returns the list of coins populated with the most recent data from API */
        private List<Coin> getCoinData(List<Coin> cryptoCoins) {
            // Get data for the cryptocurrencies in coin list
            int NUM_COINS = 4;

            //init HTTP client
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                // API base URL for request
                client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
                
                // Retreive & add data to each specific coin
                foreach (Coin c in cryptoCoins)
                {
                    //        https://min-api.cryptocompare.com/data/pricemultifull?fsyms=ETH&tsyms=EUR
                    // HTTP request
                    //    HttpResponseMessage response = client.GetAsync(String.Format("pricemultifull?fsyms={0}&tsyms=EUR&extraParams=cyptical", c.name)).Result;
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
                    c.price =(double)double.Parse(token.SelectToken(String.Format("RAW.{0}.EUR.PRICE", c.name)).ToString());

                    // Populate textboxes to display to user
                    switch (c.name)
                    {
                        case "ETH":
                            ETHPriceText.Text = String.Format("{0:0.00}", c.price);
                            break;
                        case "BTC":
                            BTCPriceText.Text = String.Format("{0:0.00}", c.price);
                            break;
                        case "BCH":
                            BCHPriceText.Text = String.Format("{0:0.00}", c.price);
                            break;
                        case "LTC":
                            LTCPriceText.Text = String.Format("{0:0.00}", c.price);
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
        private List<Coin> initCoins() {
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
