using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using MyToolkit.Multimedia;

namespace NSIT_Connect.ViewModels
{
    public class VideoDetailPageViewModel : ViewModelBase
    {
        private Uri sourceuri;
        private YVideoItem _selected;
        public YVideoItem Selected
        {
            get { return _selected; }
            set
            {
                var message = value as YVideoItem;
                Set(ref _selected, message);
            }
        }

        public Uri SourceUri
        {
            get { return sourceuri; }
            set { Set(ref sourceuri, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            
            Selected = (suspensionState.ContainsKey(nameof(Selected))) ? suspensionState[nameof(Selected)] as YVideoItem : parameter as YVideoItem;
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {

                    if (!Selected.VideoID.Equals(string.Empty))
                    {
                        //Get The Video Uri and set it as a player source
                        var url = await YouTube.GetVideoUriAsync(Selected.VideoID, YouTubeQuality.QualityHigh);
                        SourceUri = url.Uri;
                    }


                }
                else
                {
                    MessageDialog message = new MessageDialog("You're not connected to Internet!");
                    await message.ShowAsync();
                }
            }
            catch { }
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
    }
}
