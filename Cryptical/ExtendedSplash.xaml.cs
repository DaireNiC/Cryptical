﻿using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Cryptical.Views;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/p/?LinkID=234238

namespace Cryptical
{
    /// <summary>
    /// Refernces for Splash screen code: https://docs.microsoft.com/en-us/windows/uwp/launch-resume/add-a-splash-screen
    /// Info on using the Lottin Splash animations : https://xamlbrewer.wordpress.com/2018/02/26/an-extended-splash-screen-with-lottie-animations-in-uwp/
    /// </summary>
    partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;

        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;

            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();

            }

            // Create a Frame to act as the navigation context
            rootFrame = new Frame();
        }

        // Position the extended splash screen image in the same location as the system splash screen image.
        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;

            // Position the Lottie animation.
            var factor = splashImageRect.Width / 620;
            SplashScreenAnimation.SetValue(Canvas.LeftProperty, splashImageRect.X + 100 * factor);
            SplashScreenAnimation.SetValue(Canvas.TopProperty, splashImageRect.Y + 150 * factor);
            SplashScreenAnimation.Width = 500 * factor;
        }

        private void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (splash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        private async void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;
            await Task.Delay(5000);

            // Work done. Close the page.
            await DismissExtendedSplash();
        }

        private async Task DismissExtendedSplash()
        {
            // Navigate to the main page.
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                rootFrame = new Frame();
                rootFrame.Content = new MainPage();
                Window.Current.Content = rootFrame;
            });
        }

    }
}