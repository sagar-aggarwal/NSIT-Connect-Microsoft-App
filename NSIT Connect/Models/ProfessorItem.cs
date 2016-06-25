using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSIT_Connect.Models
{
    public class ProfessorItem : INotifyPropertyChanged
    {
        private string name;
        private string room;
        private string phone;
        private string email;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Name");
            }
        }

        public string Room
        {
            get { return room; }
            set
            {
                room = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Room");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Email");
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

