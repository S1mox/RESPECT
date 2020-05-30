using RESPECT.CachingData;
using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewRooms_Page : ContentPage
    {
        public List<Models.Room> Rooms { get; set; } = new List<Models.Room>();
        public Models.User User { get; set; } = new Models.User();

        public ListViewRooms_Page(Models.User user)
        {
            InitializeComponent();
            User = user;

            Setup();
        }

        public ListViewRooms_Page()
        {
            InitializeComponent();
            User = CurrentData.CurrentUser;

            Setup();
        }

        private async void Setup()
        {
            try
            {
                CachingData.CurrentData.GetRooms(User.Id, Rooms);              
            }
            catch (Exception ex)
            {
                await DisplayAlert($"{User.Id}", $"{User.Name} + {ex.Message}", "OK");
                throw;
            }

            rooms_list.ItemsSource = Rooms;

            BindingContext = this;
        }

        private async void GoToRoom(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Models.Room room = ((Models.Room)e.Item);
                ListView view = ((ListView)sender);

                await Navigation.PushAsync(new RoomView_Page(room) { Title = room.Name });

                view.SelectedItem = null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ок"); 
                throw;
            }           
        }

        private async void GoToRoomCreate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Room_create());
        }

        private async void Refresh_clicked(object sender, EventArgs e)
        {
            try
            {
                CurrentData.UpdateData();
                List<Models.Room> rooms = new List<Models.Room>();
                rooms.AddRange(Rooms);

                CurrentData.GetRooms(User.Id, rooms);

                Rooms = rooms;

                rooms_list.ItemsSource = Rooms;
            }
            catch (Exception ex)
            {
                await DisplayAlert($"{User.Id}", $"{User.Name} + {ex.Message}", "OK");
                throw;
            }
        }

        private void sort_clicked(object sender, EventArgs e)
        {
            var SortedRooms = from r in Rooms
                    orderby r.Name
                    select r;

            Rooms = SortedRooms.ToList();
            rooms_list.ItemsSource = Rooms;

        }
    }
}
