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
    public class ProfessorsPageViewModel : ViewModelBase
    {
        public ObservableCollection<LocationItem> lpanel { get; set; }
        string[] arrlocations = {"MANAGEMENT",
            "CHEMISTRY",
            "PHYSICS",
            "DEPARTMENT OF \n       MATHS",
            "HUMANITIES & MANAGEMENT",
            "ELECTRONICS & COMMUNICATION ENGG",
            "COMPUTER ENGG",
            "INSTRUMENTATION & CONTROL ENGG",
            "MANUFACTURING \n     PROCESSES & AUTOMATION ENGG",
            "INFORMATION \n TECHNOLOGY",
            "BIO-TECHNOLOGY",
            "APPLIED SCIENCES" };
        string[] Source = {
              "ms-appx:///Assets/Professors/management.png",
              "ms-appx:///Assets/Professors/chemistry.png",
              "ms-appx:///Assets/Professors/physics.png",
              "ms-appx:///Assets/Professors/math.png",
              "ms-appx:///Assets/Professors/humanities.png",
              "ms-appx:///Assets/Professors/ece.png",
              "ms-appx:///Assets/Professors/computer.png",
              "ms-appx:///Assets/Professors/instrumentation.png",
              "ms-appx:///Assets/Professors/Mechanical.png",
              "ms-appx:///Assets/Professors/it.png",
              "ms-appx:///Assets/Professors/bio2.png",
              "ms-appx:///Assets/Professors/applied.png"

        };

        private LocationItem _selected ;
        public object Selected {
            get { return _selected; }
            set {
                var message = value as LocationItem;
                Set(ref _selected, message);
            }
        }

        public ProfessorsPageViewModel()
        {
            lpanel = new ObservableCollection<LocationItem>();
            for (int i = 0; i < arrlocations.Length; i++)
            {
                lpanel.Add(new LocationItem() { Name = arrlocations[i], source = new Uri(Source[i]) });
            }
            Selected = lpanel[0];
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

        public void GotoProfessorDetailsPage() =>
                    NavigationService.Navigate(typeof(Views.ProfesorsDetailPage), Selected);

        //public DelegateCommand GotoProfessorDetailsPageDe => new DelegateCommand(async () =>
        //{

        //    NavigationService.Navigate(typeof(Views.ProfesorsDetailPage), Selected);
        //});
    }

}
