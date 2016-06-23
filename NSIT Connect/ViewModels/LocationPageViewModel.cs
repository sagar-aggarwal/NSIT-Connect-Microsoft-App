using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

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

    public LocationPageViewModel()
        {
            lpanel = new ObservableCollection<LocationItem>();
            for (int i = 0; i < arrlocations.Length; i++)
            {
                lpanel.Add(new LocationItem() { Name = arrlocations[i], source = new Uri(Source[i]) });
            }
        }
    }
}
