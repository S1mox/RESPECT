using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace RESPECT.DB_Data
{
    class DB_Data_Controller
    {
        public static Users CurrentUser { get; set; }
        
        public static List<DB_Data.Rooms> GetRooms(int UserId)
        {
            List<Rooms> result = new List<Rooms>();

            using (Respect_dbContext respect_Db = new Respect_dbContext())
            {
                List<Points> points_args = respect_Db.Points.ToList();
                List<Rooms> rooms = respect_Db.Rooms.ToList();

                foreach (var item in points_args)
                {
                    if (item.UserId == UserId)
                    {
                        result.Add(rooms[item.RoomId - 1]);
                    }
                }
            }

            return result;
        }

        public static int GetPoints(int UserId, int RoomId)
        {
            using (Respect_dbContext respect_Db = new Respect_dbContext())
            {
                List<Points> points_args = respect_Db.Points.ToList();

                foreach (var item in points_args)
                {
                    if (item.UserId == UserId && item.RoomId == RoomId)
                    {
                        return item.Value;
                    }
                }

                return 0;
            }
        }
    
        public static int GetAllPoints(int UserId)
        {
            int sum = 0;

            using (Respect_dbContext respect_Db = new Respect_dbContext())
            {
                List<Points> points_args = respect_Db.Points.ToList();

                foreach (var item in points_args)
                {
                    if (item.UserId == UserId)
                    {
                        sum += item.Value;
                    }
                }

                return sum;
            }
        }
    
        public static List<string> GetNotifications(int UserId)
        {
            List<string> result = new List<string>();

            using (Respect_dbContext respect_Db = new Respect_dbContext())
            {
                List<Notifications> notifications = respect_Db.Notifications.ToList();

                foreach (var item in notifications)
                {
                    if (item.IdReceiver == UserId)
                    {
                        result.Add($"{GetUser(item.Id).Name}  отправил вам {item.Value} внутри комнаты {GetRoom(item.IdRoom).Name}");
                    }
                }

                return result;
            }
        }

        public static Users GetUser(int id)
        {
            using (Respect_dbContext respect_Db = new Respect_dbContext())
            {
                List<Users> users = respect_Db.Users.ToList();

                foreach (var item in users)
                {
                    if (item.Id == id)
                    {
                        return item;
                    }
                }

                return null;
            }
        }
    
        public static Rooms GetRoom(int? id)
        {
            using (Respect_dbContext respect_Db = new Respect_dbContext())
            {
                List<Rooms> rooms = respect_Db.Rooms.ToList();

                foreach (var item in rooms)
                {
                    if (item.Id == id)
                    {
                        return item;
                    }
                }

                return null;
            }
        }
    }
}
