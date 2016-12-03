using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace NSIT_Connect.Models
{
    class CodeRadarItem : INotifyPropertyChanged
    {
        private string _title;
        private string _link;
        private string _description;
        private string _start;
        private string _end;
        private string _days;
        private Uri _logo;
        private SolidColorBrush _color;

        public event PropertyChangedEventHandler PropertyChanged;


        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Color");
            }
        }

        public Uri Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Logo");
            }
        }

        public string Days
        {
            get { return _days; }
            set
            {
                _days = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Days");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Title");
            }
        }

        public string Link
        {
            get { return _link; }
            set
            {
                _link = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Link");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Description");
            }
        }

        public string Start
        {
            get { return _start; }
            set
            {
                _start = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Start");
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                _end = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("End");
            }
        }


        protected void OnPropertyChanged(string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
