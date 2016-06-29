using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    public class LocationPageViewModel : ViewModelBase
    {
        public ObservableCollection<LocationItem> lpanel { get; set; }
        string[] arrlocations = { "NSIT Hotspots", "Cafes", "NightClub", "Restuarant", "Malls", "Bowling", "Food", "Movies", "Amusment Park", "Park" };
        string[] Source = {
              "ms-appx:///Assets/Location/nsit_hotspots.jpg",
              "ms-appx:///Assets/Location/cafes.jpg",
              "ms-appx:///Assets/Location/night_club.jpg",
              "ms-appx:///Assets/Location/restuarant.png",
              "ms-appx:///Assets/Location/malls.jpg",
              "ms-appx:///Assets/Location/bowling.jpg",
              "ms-appx:///Assets/Location/food.jpg",
              "ms-appx:///Assets/Location/movies.jpg",
              "ms-appx:///Assets/Location/amusment.jpg",
              "ms-appx:///Assets/Location/park.jpg",
        };
        private string[] searches = { "Empty" , "cafe", "night_club", "restaurant", "shopping_mall", "bowling_alley", "food", "movie_theater", "amusement_park", "park" };

        public LocationPageViewModel()
        {
            lpanel = new ObservableCollection<LocationItem>();
            for (int i = 0; i < arrlocations.Length; i++)
            {
                lpanel.Add(new LocationItem() { Name = arrlocations[i], source = new Uri(Source[i]) , Key = searches[i] ,Number = i});
            }
            Selected = lpanel[0];
        }

        private LocationItem _selected;
        public object Selected
        {
            get { return _selected; }
            set
            {
                var message = value as LocationItem;
                Set(ref _selected, message);
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Selected = suspensionState[nameof(Selected)];
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Selected)] = Selected;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoHangoutPage() =>
                    NavigationService.Navigate(typeof(Views.HangoutPage), Selected);


    }
}
