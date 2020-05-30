using RESPECT.Models;
using RESPECT.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace RESPECT.CachingData
{
    class CurrentData
    {
        public static User CurrentUser = new User();
        public static ServerClientViewModel Server { get; set; } = new ServerClientViewModel();

        static CurrentData()
        {
            UpdateData();
        }

        public static async void UpdateData()
        {
            await Server.GetRooms();
            await Server.GetPoints();
            await Server.GetNotifications();
            await Server.GetUsers();
        }

        public static void GetRooms(int UserId, List<Room> currentRoom)
        {
            UpdateData();

            if (currentRoom.Count > 0)
            {
                currentRoom.Clear();
            }

            for (int i = 0; i < Server.Points.Count; i++)
            {
                if (Server.Points[i].UserId == UserId)
                {
                    currentRoom.AddRange(Server.Rooms.Where(r => r.Id == Server.Points[i].RoomId));
                }
            }
        }

        public static List<User> GetUsersInRoom(int RoomId)
        {
            UpdateData();

            List<User> users = new List<User>();

            for (int i = 0; i < Server.Points.Count; i++)
            {
                if (Server.Points[i].RoomId == RoomId)
                {
                    users.AddRange(Server.Users.Where(u => u.Id == Server.Points[i].UserId));
                }
            }

            return users;
        }

        public static int GetPointsUserInRoom(int UserId, int RoomId)
        {
            UpdateData();

            for (int i = 0; i < Server.Points.Count; i++)
            {
                if (Server.Points[i].RoomId == RoomId && Server.Points[i].UserId == UserId)
                {
                    return Server.Points[i].Value;
                }
            }

            return 0;
        }

        public static List<Room> GetChildsRooms(Room Parent)
        {
            UpdateData();

            List<Room> childs = new List<Room>();

            for (int i = 0; i < Server.Rooms.Count; i++)
            {
                if (Server.Rooms[i].ParentRoom == Parent.Id && Server.Rooms[i].ParentRoom != 0)
                {
                    childs.Add(Server.Rooms[i]);
                }
            }

            return childs;
        }
    }
}
