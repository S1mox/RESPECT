using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace RESPECT.Models
{
    public class User: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal int Id { get; private set; }

        public string login { get; set; }
        public string password { get; set; }

        private string name;
        private string status;
        private string pathToImage;
        private List<(int id, int points)> roomsAndPoint = new List<(int id, int points)>();
        private List<string> notifications = new List<string>();

        public int AllPoints { get; private set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        public string PathToImage
        {
            get { return pathToImage; }
            set
            {
                if (pathToImage != value)
                {
                    pathToImage = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PathToImage"));
                }
            }
        }

        public List<(int id, int points)> RoomsAndPoint
        {
            get { return roomsAndPoint; }
            set
            {
                if (roomsAndPoint != value)
                {
                    roomsAndPoint = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RoomsAndPoint"));
                }
            }
        }

        public List<string> Notifications
        {
            get { return notifications; }
            set
            {
                if (notifications != value)
                {
                    notifications = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Notifications"));
                }
            }
        }

        public User(int ID, string name, string login, string password, string pathToImage, List<(int id, int points)> roomsAndPoint, List<string> notifications = null, string status = "")
        {
            this.Id = ID;

            this.name = name;

            this.login = login;
            this.password = password;

            this.status = status;
            this.pathToImage = pathToImage;
            this.roomsAndPoint = roomsAndPoint;

            if (notifications != null)
            {
                this.notifications = notifications;
            }

            if (status != "")
            {
                this.status = status;
            }

            AllPoints = GetPoints(this);
        }

        private static int GetPoints(Models.User user)
        {
            int sum = 0;

            if (user.RoomsAndPoint == null)
            {
                return 0;
            }

            for (int i = 0; i < user.RoomsAndPoint.Count; i++)
            {
                sum += user.RoomsAndPoint[i].points;
            }

            return sum;
        }
        
        public static bool Initialize(Models.User user, string log, string pas)
        {
            if (user.login == log)
            {
                if (user.password == pas)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool MoreThanZero()
        {
            if (notifications.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
