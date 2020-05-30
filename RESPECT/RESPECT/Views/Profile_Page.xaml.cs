using RESPECT.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RESPECT.CachingData;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile_Page : ContentPage
    {
        public User User { get; set; } = new User();
        public List<Room> Rooms { get; set; } = new List<Room>();

        public Profile_Page(User currentUser)
        {
            InitializeComponent();
            User = currentUser;


            edit_button.IsVisible = false;
            seturi_button_uwp.IsVisible = false;

            Setup();
        }

        public Profile_Page()
        {
            InitializeComponent();
            User = CurrentData.CurrentUser;

            Setup();
        }

        private async void Refresh(object sender, EventArgs e)
        {
            try
            {
                CurrentData.UpdateData();
                List<Room> rooms = new List<Room>();
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

        private async void Setup()
        {
            User_image.SetBinding(Image.SourceProperty, new Binding() { Source = User, Path = "PathToImage" });
            User_name.SetBinding(Label.TextProperty, new Binding() { Source = User, Path = "Name", StringFormat = "Имя: {0}", Mode = BindingMode.OneWay });
            User_status.SetBinding(Label.TextProperty, new Binding() { Source = User, Path = "Status", StringFormat = "Статус: {0}", Mode = BindingMode.OneWay });

            User_name_entry.SetBinding(Entry.TextProperty, new Binding() { Source = User, Path = "Name", Mode = BindingMode.TwoWay });
            User_status_entry.SetBinding(Entry.TextProperty, new Binding() { Source = User, Path = "Status", Mode = BindingMode.TwoWay });

            if (User != CurrentData.CurrentUser)
            {
                User_image.IsEnabled = false;
            }

            try
            {
                CurrentData.GetRooms(User.Id, Rooms);
            }
            catch (Exception ex)
            {
                await DisplayAlert($"{User.Id}", $"{User.Name} + {ex.Message}", "OK");
                throw;
            }

            Title = User.Login;

            BindingContext = this;
        }

        private void GoToRoom(object sender, ItemTappedEventArgs e)
        {
            Models.Room room = ((Models.Room)e.Item);
            ListView view = ((ListView)sender);

            Navigation.PushAsync(new RoomView_Page(room) { Title = room.Name });

            view.SelectedItem = null;
        }

        private async void Edit_clicked(object sender, EventArgs e)
        {        
            User_name.IsVisible = !User_name.IsVisible;
            User_status.IsVisible = !User_status.IsVisible;

            User_name_entry.IsVisible = !User_name_entry.IsVisible;
            User_status_entry.IsVisible = !User_status_entry.IsVisible;

            User_name_entry.Text = string.IsNullOrEmpty(User_name_entry.Text) ? User.Name : User_name_entry.Text;
            User_status_entry.Text = string.IsNullOrEmpty(User_status_entry.Text) ? User.Status : User_status_entry.Text;

            await CurrentData.Server.UpdateUser(User);
        }

        private async void Seturi_button(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Установка изображения", "Вставьте URL-ссылку изображения в поле", "Ок", "Отмена");

            if (result != null && result != "")
            {
                try
                {
                    await CurrentData.Server.UpdateUser(new User()
                    {
                        Id = User.Id,
                        Login = User.Login,
                        Name = User.Name,
                        Password = User.Password,
                        Status = User.Status,
                        PathToImage = result
                    });

                    CurrentData.UpdateData();
                    User = await CurrentData.Server.GetUser(User.Id);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "Ок");
                    throw;
                }
            }
        }

        private void SetPictureUwp_clicked(object sender, EventArgs e)
        {
            uri_edit.IsVisible = !uri_edit.IsVisible;
        }

        private async void SetPictureUri_clicked(object sender, EventArgs e)
        {
            string result = uri_entry.Text;

            if (result != null && result != "")
            {
                try
                {
                    await CurrentData.Server.UpdateUser(new User()
                    {
                        Id = User.Id,
                        Login = User.Login,
                        Name = User.Name,
                        Password = User.Password,
                        Status = User.Status,
                        PathToImage = result
                    });

                   CurrentData.UpdateData();
                    User = await CurrentData.Server.GetUser(User.Id);
                    User_image.Source = User.PathToImage;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "Ок");
                    throw;
                }
            }
        }
    }
}