using RESPECT.Models;
using RESPECT.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RESPECT.ViewModel
{
    class ServerClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<User> Users { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Points> Points { get; set; }

        Users_Service Users_Service = new Users_Service();
        Rooms_Service Rooms_Service = new Rooms_Service();
        Notificatins_Service Notificatins_Service = new Notificatins_Service();
        Points_Service Points_Service = new Points_Service();

        public INavigation Navigation { get; set; }

        public ServerClientViewModel()
        {
            Users = new List<User>();
            Rooms = new List<Room>();
            Notifications = new List<Notification>();
            Points = new List<Points>();
        }

        public async Task GetUsers()
        {
            IEnumerable<User> Users = await Users_Service.Get();

            // очищаем список
            this.Users = Users.ToList();
        }

        public async Task GetRooms()
        {
            IEnumerable<Room> rooms = await Rooms_Service.Get();

            // очищаем список
            Rooms = rooms.ToList();
        }

        public async Task GetNotifications()
        {
            IEnumerable<Notification> Notifications = await Notificatins_Service.Get();

            // очищаем список
            this.Notifications = Notifications.ToList();
        }

        public async Task GetPoints()
        {
            IEnumerable<Points> Points = await Points_Service.Get();

            // очищаем список
            this.Points = Points.ToList();
        }

        public async Task<User> GetUser(int id)
        {
            return await Users_Service.GetUser(id);
        }

        public async Task<Room> GetRoom(int id)
        {
            return await Rooms_Service.GetRoom(id);
        }

        public async Task<Notification> GetNotification(int id)
        {
            return await Notificatins_Service.GetNotification(id);
        }

        public async Task<Points> GetPoints(int id)
        {
            return await Points_Service.GetPoints(id);
        }

        public async Task<User> AddUser(User user)
        {
            return await Users_Service.Add(user);
        }

        public async Task<Room> AddRoom(Room room)
        {
            return await Rooms_Service.Add(room);
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            return await Notificatins_Service.Add(notification);
        }

        public async Task<Points> AddPoints(Points points)
        {
            return await Points_Service.Add(points);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await Users_Service.PutUser(user);
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            return await Rooms_Service.PutRoom(room);
        }

        public async Task<Notification> UpdateNotification(Notification notification)
        {
            return await Notificatins_Service.PutNotification(notification);
        }

        public async Task<Points> UpdatePoints(Points points)
        {
            return await Points_Service.PutPoints(points);
        }

        public async Task<User> DeleteUser(User user)
        {
            return await Users_Service.DeleteUser(user);
        }

        public async Task<Room> DeleteRoom(Room room)
        {
            return await Rooms_Service.DeleteRoom(room);
        }

        public async Task<Notification> DeleteNotification(Notification notification)
        {
            return await Notificatins_Service.DeleteNotifications(notification);
        }

        public async Task<Points> DeletePoints(Points points)
        {
            return await Points_Service.DeletePoints(points);
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
