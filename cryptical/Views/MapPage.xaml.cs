using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


using Cryptical.Services;

using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

//Resource (1): https://docs.microsoft.com/en-us/windows/uwp/maps-and-location/get-location

namespace Cryptical.Views
{
    public sealed partial class MapPage : Page
    {
        // Setting map Zoom level & default (best practice according to dev docs)
        private const double DefaultZoomLevel = 17;
        private double _zoomLevel;

        //Adding the location service for cleaner code 
        private readonly LocationService _locationService;

        public Geopoint Center { get; private set; }
        public double ZoomLevel { get; private set; }

        // set default location in case location fails
        // co-ordinates for dublin city 
        private readonly BasicGeoposition _defaultPosition = new BasicGeoposition()
        {
            Latitude = 27.609425,
            Longitude = 22.3417
        };

        public MapPage()
        {
            Loaded += MapPage_Loaded;
            Unloaded += MapPage_Unloaded;
            _locationService = new LocationService();
            Center = new Geopoint(_defaultPosition);
            ZoomLevel = DefaultZoomLevel;
            InitializeComponent();
        }
        //on page load...
        private async void MapPage_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        private void MapPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Cleanup();
        }

        public async Task InitializeAsync()
        {
            //access location service 
            if (_locationService != null)
            {
                //alter geo co ordinates in event of change of position
                _locationService.PositionChanged += LocationService_PositionChanged;

                var initializationSuccessful = await _locationService.InitializeAsync();

                if (initializationSuccessful)
                {
                    await _locationService.StartListeningAsync();
                }

                if (initializationSuccessful && _locationService.CurrentPosition != null)
                {
                    //get the current location point 
                    Center = _locationService.CurrentPosition.Coordinate.Point;
                }
                else
                {
                    //if fails to get location --> make it the default co ordinates
                    Center = new Geopoint(_defaultPosition);
                }
            }

            if (mapControl != null)
            {
                //your map service token. Obtained from https://www.bingmapsportal.com/
                mapControl.MapServiceToken = "y17BMedIEL9Y8XkAIHTT~0c72K3B_3x-Ffl7B3K3Otw~AlV5wgF2S4ocZBzmdGrantbUcfOa0p72Bk3zSSmlB10YoDcrPn6OQBo09pDXx7ZT";

                // Set the map location.
                mapControl.Center = Center;
                Debug.WriteLine("geo loc: " + mapControl.Center.Position.ToString());
                //add the icon to the map to display user locaiton
                AddMapIcon(mapControl.Center, "Your Location");
                Debug.WriteLine("center to str loc: " + Center.Position.ToString());

            }
        }
        //metho to plot all the locations of business in Ireland that allow for payment in Bitcoin/Cryptocurrencies
        public void plotCryptoLocations()
        {
            //adding a new location to plot
            BasicGeoposition point = new BasicGeoposition();
            point.Latitude = 53.3;
            point.Longitude = -6.80;
            Geopoint point_one = new Geopoint(point);

            AddMapIcon(point_one, "second_locaction");

        }




        //gracefully stop location monitoring 
        public void Cleanup()
        {
            if (_locationService != null)
            {
                _locationService.PositionChanged -= LocationService_PositionChanged;
                _locationService.StopListening();
            }
        }

        private void LocationService_PositionChanged(object sender, Geoposition geoposition)
        {
            if (geoposition != null)
            {
                Center = geoposition.Coordinate.Point;
            }
        }

        private void AddMapIcon(Geopoint position, string title)
        {
            MapIcon mapIcon = new MapIcon()
            {
                Location = position,
               //NormalizedAnchorPoint = new Point(0.5, 1.0),
                Title = title,
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/map.png")),
                ZIndex = 0
            };
            mapControl.MapElements.Add(mapIcon);
        }
    }
}