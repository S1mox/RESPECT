using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewRooms_Page : ContentPage
    {
        public List<DB_Data.Rooms> Rooms { get; set; }
        private static DB_Data.Users User;

        public ListViewRooms_Page(DB_Data.Users user)
        {
            InitializeComponent();

            User = user;

            Setup();
        }

        public ListViewRooms_Page()
        {
            InitializeComponent();

            User = DB_Data.DB_Data_Controller.CurrentUser;

            Setup();
        }

        private void Setup()
        {
            Rooms = DB_Data.DB_Data_Controller.GetRooms(User.Id);            

            BindingContext = this;        
        }

        private async void GoToRoomViewPage(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new RoomView_Page((DB_Data.Rooms)e.Item)
            {
                Title = ((DB_Data.Rooms)e.Item).Name,
            });

            ((ListView)sender).SelectedItem = null;
        }
    }
}
