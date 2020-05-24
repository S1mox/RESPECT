using System;
using System.Collections.Generic;
using System.Text;

namespace RESPECT.Data
{
    class AppData
    {
        internal static List<Models.User> users = new List<Models.User>
        {
            new Models.User(4, "Сяся", "nyalen", "yaterrarist", "https://sun9-38.userapi.com/c857420/v857420650/7a814/uwTVCypMFbo.jpg", 
                new List<(int id, int points)> 
                { 
                    (0, 200),
                    (1, 100), 
                    (2, 400),
                    (3, 100),
                }, status: "Норм живу, выживаю")
        };

        internal static List<Models.Room> Rooms = new List<Models.Room>
        {
            new Models.Room(0, "Аниме","xY6x","https://i.pinimg.com/originals/33/b8/69/33b869f90619e81763dbf1fccc896d8d.jpg"),
            new Models.Room(1,"Работа","Y6WW", "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcR7IP0MmeFhEjs5aRfJmCI93Wi6MvskUFJdWhqdLzu4tByDLg-Y"),
            new Models.Room(2,"ГТЭК", "P6TS","https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSv0xE0tDcd3bEeAfbi8JZf6g-sPvizm4VWi8ugEFPG9990JDKM"),
            new Models.Room(3,"Херувим", "HJTF","https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcS4KJay9hbjSg_BJaPqn6BmmCM3N1dgwO3N3B2DAtsqoZkAxf5S"),
            new Models.Room(4,"17-ый независимый", "MJ2F", "https://www.telegraph.co.uk/content/dam/technology/2016/05/11/new-instagram-logo_1_trans_NvBQzQNjv4BqqVzuuqpFlyLIwiB6NTmJwfSVWeZ_vEN7c6bHu2jJnT8.jpg?imwidth=450"),
            new Models.Room(5,"17-ый независимый", "MJ2F","https://www.telegraph.co.uk/content/dam/technology/2016/05/11/new-instagram-logo_1_trans_NvBQzQNjv4BqqVzuuqpFlyLIwiB6NTmJwfSVWeZ_vEN7c6bHu2jJnT8.jpg?imwidth=450"),
            new Models.Room(6,"17-ый независимый", "MJ2F","https://www.telegraph.co.uk/content/dam/technology/2016/05/11/new-instagram-logo_1_trans_NvBQzQNjv4BqqVzuuqpFlyLIwiB6NTmJwfSVWeZ_vEN7c6bHu2jJnT8.jpg?imwidth=450"),
            new Models.Room(7,"17-ый независимый",  "MJ2F", "https://www.telegraph.co.uk/content/dam/technology/2016/05/11/new-instagram-logo_1_trans_NvBQzQNjv4BqqVzuuqpFlyLIwiB6NTmJwfSVWeZ_vEN7c6bHu2jJnT8.jpg?imwidth=450"),
        };

        internal static Models.User CurrentUser { get; set; }

        internal static List<Models.Room> GenerateRoomsList(Models.User user)
        {
            List<Models.Room> rooms = new List<Models.Room>();

            for (int i = 0; i < user.RoomsAndPoint.Count; i++)
            {
                int indexer = FindRoom(user.RoomsAndPoint[i].id); 

                if (indexer >= 0)
                {
                    rooms.Add(Rooms[indexer]);
                }
            }

            return rooms;
        }

        private static int FindRoom(int id)
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                if (Rooms[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
