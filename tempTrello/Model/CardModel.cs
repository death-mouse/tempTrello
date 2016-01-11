using System;
using System.ComponentModel;

namespace tempTrello.Model
{
    class CardModel : INotifyPropertyChanged
    {
        private DateTime due;
        public DateTime Due
        {
            get
            {
                return due;
            }
            set
            {
                if(due != value)
                {
                    due = value;
                    OnPropertyChanged("Due");
                }
            }
        }
        private string user;
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string idBoard;
        public string IdBoard
        {
            get
            {
                return idBoard;
            }
            set
            {
                if (idBoard != value)
                {
                    idBoard = value;
                    OnPropertyChanged("IdBoard");
                }
            }
        }
        private string idList;
        public string IdList
        {
            get
            {
                return idList;
            }
            set
            {
                if (idList != value)
                {
                    idList = value;
                    OnPropertyChanged("IdList");
                }
            }
        }
        private string desc;
        public string Desc
        {
            get
            {
                return desc;
            }
            set
            {
                if (desc != value)
                {
                    desc = value;
                    OnPropertyChanged("Desc");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
