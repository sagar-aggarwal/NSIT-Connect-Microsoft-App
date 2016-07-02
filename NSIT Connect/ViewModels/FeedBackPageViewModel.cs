using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    class FeedBackPageViewModel :ViewModelBase
    {
        private Visibility _progressVisibility = Visibility.Collapsed;
        public Visibility ProgressVisibility
        { get { return _progressVisibility; } set { Set(() => ProgressVisibility, ref _progressVisibility, value); } }

        private Uri _sourceuri = null;
        public Uri SourceUri
        { get { return _sourceuri; } set { Set(() => SourceUri, ref _sourceuri, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                ProgressVisibility = Visibility.Visible;
                SourceUri = new Uri("http://goo.gl/forms/DS8To6mufz");
            }
            else
            {
                ProgressVisibility = Visibility.Collapsed;
                var mssg = new MessageDialog("Check You Internet Connection");
                await mssg.ShowAsync();
            }
            await Task.CompletedTask;
        }
    }
}
