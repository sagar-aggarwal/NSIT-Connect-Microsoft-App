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
        private String _Name;
        private Uri _source;
        public event PropertyChangedEventHandler PropertyChanged;

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
