using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Cryptical
{
    public class NewsItem
    {

        public string URL { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
       public  Image ThumbImage { get; set; }
    }

}
