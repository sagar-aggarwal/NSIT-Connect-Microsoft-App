using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSIT_Connect.Models
{
    class HangoutItem : INotifyPropertyChanged
    {
        private double photowidth;
        private double photoheight;
        private double rating;

        private string opennowstring;
        private string name;
        private string icon;
        private string place_id;
        private Uri phtotref;
        private double longi;
        private double latti;
        private string vicinity;


        public event PropertyChangedEventHandler PropertyChanged;

        public double PhotoWidth
        { get { return photowidth; } set { photowidth = value; OnPropertyChanged(); } }

        public double PhotoHeight
        { get { return photoheight; } set { photoheight = value; OnPropertyChanged(); } }

        public double Rating
        { get { return rating; } set { rating = value; OnPropertyChanged(); } }

        public string OpenNowString
        { get { return opennowstring; } set { opennowstring = value; OnPropertyChanged(); } }

        public string Name
        { get { return name; } set { name = value; OnPropertyChanged(); } }

        public string Icon
        { get { return icon; } set { icon = value; OnPropertyChanged(); } }

        public string Place_ID
        { get { return place_id; } set { place_id = value; OnPropertyChanged(); } }

        public Uri Photo_Ref
        { get { return phtotref; } set { phtotref = value; OnPropertyChanged(); } }

        public double Longi
        { get { return longi; } set { longi = value; OnPropertyChanged(); } }

        public double Latii
        { get { return latti; } set { latti = value; OnPropertyChanged(); } }

        public string Vicinity
        { get { return vicinity; } set { vicinity = value; OnPropertyChanged(); } }


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
