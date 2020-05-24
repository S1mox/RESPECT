using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile_Page : ContentPage
    {
        public List<DB_Data.Rooms> Rooms { get; set; }
        public DB_Data.Users User { get; set; }
        public int AllPointsUser { get; set; }

        public Profile_Page(DB_Data.Users currentUser)
        {
            InitializeComponent();

            User = currentUser;
            edit_button.IsVisible = false;

            Setup();
        }

        public Profile_Page()
        {
            InitializeComponent();
            User = DB_Data.DB_Data_Controller.CurrentUser;

            Setup();
        }

        private void Setup()
        {
            Rooms = DB_Data.DB_Data_Controller.GetRooms(User.Id);
            AllPointsUser = DB_Data.DB_Data_Controller.GetAllPoints(User.Id);

            User_image.SetBinding(Image.SourceProperty, new Binding() { Source = User, Path = "PathToImage" });
            User_name.SetBinding(Label.TextProperty, new Binding() { Source = User, Path = "Name", StringFormat = "Имя: {0}" });
            User_status.SetBinding(Label.TextProperty, new Binding() { Source = User, Path = "Status", StringFormat = "Статус: {0}" });
            User_Points.SetBinding(Label.TextProperty, new Binding() { Source = AllPointsUser, StringFormat = "Общее количество очков: {0}" });

            Title = User.Login;

            BindingContext = this;
        }

        private async void Edit_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Profile_Editor_Page());
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