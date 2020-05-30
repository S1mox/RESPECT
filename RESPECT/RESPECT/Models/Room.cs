using System.ComponentModel;

namespace RESPECT.Models
{
    public class Room : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;

        private string name;
        private string inviteKey;
        private string pathToLogo;
        private int parentRoom;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string InviteKey
        {
            get
            {
                return inviteKey;
            }

            set
            {
                if (value != inviteKey)
                {
                    inviteKey = value;
                    OnPropertyChanged("InviteKey");
                }
            }
        }

        public string PathToLogo
        {
            get
            {
                return pathToLogo;
            }

            set
            {
                if (value != pathToLogo)
                {
                    pathToLogo = value;
                    OnPropertyChanged("PathToLogo");
                }
            }
        }

        public int ParentRoom
        {
            get
            {
                return parentRoom;
            }

            set
            {
                if (value != parentRoom)
                {
                    parentRoom = value;
                    OnPropertyChanged("ParentRoom");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return Name + " #" + Id.ToString();
        }
    }
}
