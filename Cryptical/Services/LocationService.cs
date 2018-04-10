using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Cryptical.Services
{
    //Helper service to extract user's location
    public class LocationService
    {
        // geolocation object for tracking user location
        private Geolocator _geolocator;

        // handle user changing location
        public event EventHandler<Geoposition> PositionChanged;

        //users current location
        public Geoposition CurrentPosition { get; private set; }

        public Task<bool> InitializeAsync()
        {
            return InitializeAsync(1);
        }

        // The level of accuracy of the user's location
        public Task<bool> InitializeAsync(uint desiredAccuracyInMeters)
        {
            return InitializeAsync(desiredAccuracyInMeters, (double)desiredAccuracyInMeters);
        }

        // request user's location & begin monitoring
        public async Task<bool> InitializeAsync(uint desiredAccuracyInMeters, double movementThreshold)
        {
            if (_geolocator != null)
            {
                _geolocator.PositionChanged -= Geolocator_PositionChanged;
                _geolocator = null;
            }

            //request permision for location data
            var access = await Geolocator.RequestAccessAsync();
            //result retured by user choice
            bool result;
            // cover cases of location allowed/not
            switch (access)
            {
                // user allows location access
                case GeolocationAccessStatus.Allowed:
                    _geolocator = new Geolocator
                    {
                        DesiredAccuracyInMeters = desiredAccuracyInMeters,
                        MovementThreshold = movementThreshold
                    };

                    result = true;
                    break;
                case GeolocationAccessStatus.Unspecified: // user does not specify
                case GeolocationAccessStatus.Denied: //user denies location access
                default:
                    result = false;
                    break;
            }

            return result;
        }

        //start monitoring user's location
        public async Task StartListeningAsync()
        {
            if (_geolocator == null)
            {
                throw new InvalidOperationException("ExceptionLocationServiceStartListeningCanNotBeCalled");
            }

            _geolocator.PositionChanged += Geolocator_PositionChanged;



            CurrentPosition = await _geolocator.GetGeopositionAsync();
        }


        //stop monitoring location & wind down gracefully
        public void StopListening()
        {
            if (_geolocator == null)
            {
                return;
            }

            _geolocator.PositionChanged -= Geolocator_PositionChanged;
        }

        // if the users location changes
        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (args == null)
            {
                return;
            }

            CurrentPosition = args.Position;

            // update changed locations
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                PositionChanged?.Invoke(this, CurrentPosition);
            });
        }
    }
}
