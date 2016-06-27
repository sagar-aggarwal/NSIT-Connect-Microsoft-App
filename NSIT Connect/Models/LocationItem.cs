using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace NSIT_Connect.Models
{
    public class LocationItem : INotifyPropertyChanged
    {
        private string _Name;
        private Uri _source;
        private int _Number;
        private string _Key;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Number");
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Name");
            }
        }

        public Uri source
        {
            get { return _source; }
            set
            {
                _source = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("source");
            }
        }

        public string Key
        {
            get { return _Key; }
            set
            {
                _Key = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Key");
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
