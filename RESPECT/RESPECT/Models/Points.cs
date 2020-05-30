using System.ComponentModel;

namespace RESPECT.Models
{
    public partial class Points : INotifyPropertyChanged
    {
        private int id;
        private int userId;
        private int roomId;
        private int value;

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

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                if (value != userId)
                {
                    userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public int RoomId
        {
            get
            {
                return roomId;
            }

            set
            {
                if (value != roomId)
                {
                    roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        public int Value
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

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
