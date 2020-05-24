using System;
using System.Collections.Generic;

using System.ComponentModel;

namespace RESPECT.Models
{
    public class Room : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal int Id { get; private set; }

        private string name;
        private string inviteKey;
        private string pathToLogo;
        private List<(int Id, int Points)> usersAndPoints = new List<(int Id, int Points)>();
        private List<Room> nestedRooms = new List<Room>();

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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("InviteKey"));
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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PathToLogo"));
                }
            }
        }

        public List<(int Id, int Points)> UsersAndPoints
        {
            get
            {
                return usersAndPoints;
            }
            set
            {
                if (value != usersAndPoints)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("UsersAndPoints"));
                }
            }
        }

        public List<Room> NestedRooms
        {
            get
            {
                return nestedRooms;
            }
            set
            {
                if (value != nestedRooms)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NestedRooms"));
                }
            }
        }

        public Room(int id, string name, string inviteKey, string pathToLogo, List<(int Id, int Points)> usersAndPoints = null, List<Room> nestedRooms = null)
        {
            this.Id = id;

            this.name = name;
            this.inviteKey = inviteKey;
            this.pathToLogo = pathToLogo;

            if (usersAndPoints != null)
            {
                this.usersAndPoints = usersAndPoints;
            }

            if (nestedRooms != null)
            {
                this.nestedRooms = nestedRooms;
            }
        }
    }
}
