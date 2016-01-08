using System.ComponentModel;

namespace tempTrello.Model
{
    class ListModel : INotifyPropertyChanged
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
        private string idCard;
        public string IdCard
        {
            get
            {
                return idCard;
            }
            set
            {
                if (idCard != value)
                {
                    idCard = value;
                    OnPropertyChanged("IdCard");
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
        private BindingList<CardModel> cardModel;
        public BindingList<CardModel> CardModel
        {
            get
            {
                return cardModel;

            }
            set
            {
                if (cardModel != value)
                {
                    cardModel = value;
                    OnPropertyChanged("CardModel");
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
