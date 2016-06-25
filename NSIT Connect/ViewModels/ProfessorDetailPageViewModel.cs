using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    public class ProfessorDetailPageViewModel : ViewModelBase
    {
        private LocationItem _selected = default(LocationItem);
        public object Selected
        {
            get { return _selected; }
            set
            {
                var message = value as LocationItem;
                Set(ref _selected, message);
            }
        }
        public ProfessorDetailPageViewModel()
        {

        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Selected = (suspensionState.ContainsKey(nameof(Selected))) ? suspensionState[nameof(Selected)] : parameter;
            LocationItem SelectedObject = Selected as LocationItem;
            if(SelectedObject != null)
            {
                var mssg = new MessageDialog(SelectedObject.Name);
                await mssg.ShowAsync();
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

    }
}
