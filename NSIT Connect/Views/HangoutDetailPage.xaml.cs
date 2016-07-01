using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NSIT_Connect.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HangoutDetailPage : Page
    {
        


        public HangoutDetailPage()
        {
            this.InitializeComponent();
        }

        private async void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            var maps = sender as MapControl;
            var accessStatus = await Geolocator.RequestAccessAsync();
            if(accessStatus == GeolocationAccessStatus.Allowed)
            {
                //current location
                Geolocator geolocator = new Geolocator();
                Geoposition pos = await geolocator.GetGeopositionAsync();
                Geopoint myLocation = pos.Coordinate.Point;
                BasicGeoposition myLocationBasic = new BasicGeoposition() {Latitude = pos.Coordinate.Latitude , Longitude = pos.Coordinate.Longitude };

                //destination coordinates
                BasicGeoposition desPosition = new BasicGeoposition() { Latitude = ViewModel.SelectedHangout.Latii, Longitude = ViewModel.SelectedHangout.Longi };
                Geopoint destPoint = new Geopoint(desPosition);


                // Create a MapIcon.
                MapIcon currentLocation = new MapIcon();
                currentLocation.Location = myLocation;
                currentLocation.NormalizedAnchorPoint = new Point(0.5, 1.0);
                currentLocation.Title = "Current Location";
                currentLocation.ZIndex = 0;
                currentLocation.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue-marker-icon.png"));

               
                // Destination
                MapIcon destination = new MapIcon();
                destination.Location = destPoint;
                destination.NormalizedAnchorPoint = new Point(0.5, 1.0);
                destination.Title = ViewModel.SelectedHangout.Name;
                destination.ZIndex = 0;
                destination.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/red-marker-icon.png"));

                //Map Route Finder
                MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteAsync(
                                        new Geopoint(myLocationBasic),
                                        new Geopoint(desPosition),
                                        MapRouteOptimization.Time,
                                        MapRouteRestrictions.None);

                //Route Intialize
                if (routeResult.Status == MapRouteFinderStatus.Success)
                {
                    //Distance-Time-Direction
                    ViewModel.Time = "Total estimated time : " + Math.Round((decimal)routeResult.Route.EstimatedDuration.TotalMinutes,2).ToString() + " min";
                    ViewModel.Distance = "Total length : " + Math.Round((decimal)routeResult.Route.LengthInMeters / 1000,3).ToString() + " km";
                    StringBuilder routeInfo = new StringBuilder();
                    routeInfo.Append("DIRECTIONS\n");
                    foreach (MapRouteLeg leg in routeResult.Route.Legs)
                    {
                        foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                        {
                            routeInfo.AppendLine(maneuver.InstructionText);
                        }
                    }
                    ViewModel.Directions = routeInfo.ToString();

                    // Use the route to initialize a MapRouteView.
                    MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                    viewOfRoute.RouteColor = (Color)this.Resources["PrimaryColor"];
                    viewOfRoute.OutlineColor = Colors.Black;

                    // Add the new MapRouteView to the Routes collection
                    // of the MapControl.
                    maps.Routes.Add(viewOfRoute);

                    // Fit the MapControl to the route.
                    await maps.TrySetViewBoundsAsync(
                          routeResult.Route.BoundingBox,
                          null,
                          Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                }


                //map initialization
                maps.MapElements.Add(destination);
                maps.MapElements.Add(currentLocation);
                maps.Center = myLocation;
                maps.ZoomLevel = 14;
                maps.LandmarksVisible = true;
            }
        }
    }
}
