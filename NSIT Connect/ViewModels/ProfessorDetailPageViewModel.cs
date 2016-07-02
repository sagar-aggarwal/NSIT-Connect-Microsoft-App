using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Data.Json;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    public class ProfessorDetailPageViewModel : ViewModelBase
    {
        public string[] arrlocations = {"MANAGEMENT",
            "CHEMISTRY",
            "PHYSICS",
            "DEPARTMENT OF MATHS",
            "HUMANITIES & MANAGEMENT",
            "ELECTRONICS & COMMUNICATION ENGG",
            "COMPUTER ENGG",
            "INSTRUMENTATION & CONTROL ENGG",
            "MANUFACTURING \n     PROCESSES & AUTOMATION ENGG",
            "INFORMATION TECHNOLOGY",
            "BIO-TECHNOLOGY",
            "APPLIED SCIENCES" };

        private LocationItem _selected = default(LocationItem);
        public LocationItem Selected
        {
            get { return _selected; }
            set
            {
                var message = value as LocationItem;
                Set(ref _selected, message);
            }
        }

        private ObservableCollection<ProfessorItem> item = new ObservableCollection<ProfessorItem>();
        public ObservableCollection<ProfessorItem> Item { get { return item; } set { item = value; } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Selected = (suspensionState.ContainsKey(nameof(Selected))) ? suspensionState[nameof(Selected)] as LocationItem : parameter as LocationItem;
            Selected.Name = "#"+arrlocations[Selected.Number].ToLower();
            int key = Selected.Number;
            string[] name = new string[100], ids = new string[100], contact = new string[100];
            int k = 0;
            JsonArray ar;
            JsonObject ob;
            ar = JsonArray.Parse(Constants.pro);
            ob = ar.GetObjectAt((uint)key);
            ar = ob.GetNamedArray("ContentArray");
            for (uint j = 0; j < ar.Count; j++)
            {

                name[k] = ar.GetObjectAt(j).GetNamedString("Name");
                if (ar.GetObjectAt(j).GetNamedString("Designation") != "")
                    name[k] = name[k] + " , " + ar.GetObjectAt(j).GetNamedString("Designation");
                ids[k] = ar.GetObjectAt(j).GetNamedString("Email");
                contact[k] = ar.GetObjectAt(j).GetNamedString("ContactNo");
                k++;
            }
            Random rnd = new Random();
            Byte[] b = new Byte[3];
            for (int j = 0; j < k; j++) {
                if (ids[j] == string.Empty)
                    ids[j] = "not available";
                if (contact[j] == string.Empty)
                    contact[j] = "not available";

                rnd.NextBytes(b);
                Color color = Color.FromArgb(150,b[0], b[1], b[2]);

                SolidColorBrush brush = new SolidColorBrush(color);
                Item.Add(new ProfessorItem() { Email = ids[j] , Name = name[j], Phone = contact[j],Foreground = brush });
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
