using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// The Coin object is used to format data recieved from the Crypto currency api.
/// Stores values for 24hr/high/low & name & current value
/// </summary>

namespace Cryptical
{
    class Coin
    {
        public Coin(string name, double price, double high24hr, double low24hr)
        {
            this.name = name;
            this.price = price;
            this.high24hr = high24hr;
            this.low24hr = low24hr;
        }

        public string name { get; set; }
        public double price { get; set; }
        public double high24hr { get; set; }
        public double low24hr { get; set; }
    }
}
