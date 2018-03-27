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

namespace Cryptical.Views
{
    public sealed partial class MapPage : Page
    {
        // TODO WTS: Set your preferred default zoom level
        private const double DefaultZoomLevel = 17;

        private readonly LocationService _locationService;

        // TODO WTS: Set your preferred default location if a geolock can't be found.
        private readonly BasicGeoposition _defaultPosition = new BasicGeoposition()
        {
            Latitude = 27.609425,
            Longitude = -122.3417
        };

        private double _zoomLevel;

        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set { Set(ref _zoomLevel, value); }
        }

        private Geopoint _center;

        public Geopoint Center
        {
            get { return _center; }
            set { Set(ref _center, value); }
        }

        public MapPage()
        {
            Loaded += MapPage_Loaded;
            Unloaded += MapPage_Unloaded;
            _locationService = new LocationService();
            Center = new Geopoint(_defaultPosition);
            ZoomLevel = DefaultZoomLevel;
            InitializeComponent();
        }

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
            if (_locationService != null)
            {
                _locationService.PositionChanged += LocationService_PositionChanged;

                var initializationSuccessful = await _locationService.InitializeAsync();

                if (initializationSuccessful)
                {
                    Debug.WriteLine("enter 1");
                    await _locationService.StartListeningAsync();
                }

                if (initializationSuccessful && _locationService.CurrentPosition != null)
                {
                    Debug.WriteLine("SHOUSKSKSl");
                    Center = _locationService.CurrentPosition.Coordinate.Point;
                }
                else
                {
                    Center = new Geopoint(_defaultPosition);
                }
            }

            if (mapControl != null)
            {
                Debug.WriteLine("Inside map control");
                // TODO WTS: Set your map service token. If you don't have one, request at https://www.bingmapsportal.com/
                mapControl.MapServiceToken = "y17BMedIEL9Y8XkAIHTT~0c72K3B_3x-Ffl7B3K3Otw~AlV5wgF2S4ocZBzmdGrantbUcfOa0p72Bk3zSSmlB10YoDcrPn6OQBo09pDXx7ZT";

                // Set the map location.
                mapControl.Center = Center;
                Debug.WriteLine("geo loc: " + mapControl.Center.Position);

                AddMapIcon(mapControl.Center, "Map_YourLocation");
                Debug.WriteLine("center to str loc: " + Center.Position.ToString());

            }
        }

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
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Title = title,
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/map.png")),
                ZIndex = 0
            };
            mapControl.MapElements.Add(mapIcon);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}