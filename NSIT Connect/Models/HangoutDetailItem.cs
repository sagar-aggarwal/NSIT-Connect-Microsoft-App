using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSIT_Connect.Models
{
    class HangoutDetailItem : INotifyPropertyChanged
    {

        private string address;
        private string phone;
        private string internationalphone;
        private string website;
        private string googlemaps;


        public event PropertyChangedEventHandler PropertyChanged;


        public string Address
        { get { return address; } set { address = value; OnPropertyChanged(); } }

        public string Phone
        { get { return phone; } set { phone = value; OnPropertyChanged(); } }

        public string InterNationalPhone
        { get { return internationalphone; } set { internationalphone = value; OnPropertyChanged(); } }

        public string Website
        { get { return website; } set { website = value; OnPropertyChanged(); } }

        public string GoogleMaps
        { get { return googlemaps; } set { googlemaps = value; OnPropertyChanged(); } }


        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
