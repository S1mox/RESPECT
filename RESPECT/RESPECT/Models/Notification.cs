
using System.ComponentModel;

namespace RESPECT.Models
{
    public partial class Notification : INotifyPropertyChanged
    {
        private int id;
        private int? idSender;
        private int? idReceiver;
        private int? idRoom;
        private int? value;
        private string description;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public int? IdSender
        {
            get
            {
                return idSender;
            }

            set
            {
                if (value != idSender)
                {
                    idSender = value;
                    OnPropertyChanged("IdSender");
                }
            }
        }

        public int? IdReceiver
        {
            get
            {
                return idReceiver;
            }

            set
            {
                if (value != idReceiver)
                {
                    idReceiver = value;
                    OnPropertyChanged("IdReceiver");
                }
            }
        }

        public int? IdRoom
        {
            get
            {
                return idRoom;
            }

            set
            {
                if (value != idRoom)
                {
                    idRoom = value;
                    OnPropertyChanged("IdRoom");
                }
            }
        }

        public int? Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return $"{IdSender} отправил вам {value} количество RS внутри комнаты {IdRoom} с подписью {description}";
        }
    }
}
