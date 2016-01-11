using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempTrello.Model
{
    class MemberModel : INotifyPropertyChanged
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
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
        private string bio;
        public string Bio
        {
            get
            {
                return bio;
            }
            set
            {
                if (bio != value)
                {
                    bio = value;
                    OnPropertyChanged("Bio");
                }
            }
        }
        private string avatarHash;
        public string AvatarHash
        {
            get
            {
                return avatarHash;
            }
            set
            {
                if (avatarHash != value)
                {
                    avatarHash = value;
                    OnPropertyChanged("AvatarHash");
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
