using System.ComponentModel;
using tempTrello.Model;

namespace tempTrello.Model
{
    class BoardModel : INotifyPropertyChanged
    {
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
        private BindingList<ListModel> listModel;
        public BindingList<ListModel> ListModel
        {
            get
            {
                return listModel;

            }
            set
            {
                if (listModel != value)
                {
                    listModel = value;
                    OnPropertyChanged("ListModel");
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
