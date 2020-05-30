using RESPECT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search_Page : ContentPage
    {
        public Search_Page()
        {
            InitializeComponent();
        }

        private async void Search_activate(object sender, PropertyChangedEventArgs e)
        {
            List<Room> rooms = new List<Room>();

            if (SearchBar_rooms.Text.Length > 0)
            {
                try
                {
                    foreach (var item in CachingData.CurrentData.Server.Rooms)
                    {

                        if (item.Name.ToLower().Contains(SearchBar_rooms.Text.ToLower()))
                        {
                            rooms.Add(item);
                        }
                    }

                    if (rooms.Count > 0)
                    {
                        Room_list.ItemsSource = rooms;
                        rooms_scroll.IsVisible = true;
                        NoData_label.IsVisible = false;
                    }
                    else
                    {
                        rooms_scroll.IsVisible = false;
                        NoData_label.IsVisible = true;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "Ок");
                    throw;
                }
            }
            else
            {
                rooms_scroll.IsVisible = false;
                NoData_label.IsVisible = true;
            }

        }

        private void GoToRoom(object sender, ItemTappedEventArgs e)
        {
            Room room = ((Room)e.Item);
            ListView view = ((ListView)sender);

            Navigation.PushAsync(new RoomView_Page(room) { Title = room.Name });

            view.SelectedItem = null;
        }

        private async void Search_key(object sender, EventArgs e)
        {
            string find_key = await DisplayPromptAsync("Пригласительный код комнаты", "Введите код");
            bool flag = false;

            if (!string.IsNullOrEmpty(find_key))
            {
                foreach (var item in CachingData.CurrentData.Server.Rooms)
                {
                    if (item.InviteKey == find_key)
                    {
                        await Navigation.PushAsync(new RoomView_Page(item));
                        flag = true;
                    }
                }

                if (!flag)
                {
                    await DisplayAlert("Поиск", "Комната не найдена", "ОК");
                }
            }          
        }

        private async void Search_key_uwp(object sender, EventArgs e)
        {
            string find_key = entry_uwp.Text;

            bool flag = false;

            if (!string.IsNullOrEmpty(find_key))
            {
                foreach (var item in CachingData.CurrentData.Server.Rooms)
                {
                    if (item.InviteKey == find_key)
                    {
                        await Navigation.PushAsync(new RoomView_Page(item));
                    }
                }

                if (!flag)
                {
                    await DisplayAlert("Поиск", "Комната не найдена", "ОК");
                }
            }          
        }
    }
}
