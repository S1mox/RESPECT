using RESPECT.CachingData;
using RESPECT.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Room_create : ContentPage
    {
        private int parent = 0;
        public Room_create()
        {
            InitializeComponent();
        }

        public Room_create(Room parentRoom)
        {
            InitializeComponent();

            parent = parentRoom.Id;
        }

        private async void Room_create_clicked(object sender, EventArgs e)
        {
            try
            {
                if (name_entry.Text != "" && name_entry != null)
                {
                    bool keyHave = false;
                    bool nameHave = false;

                    foreach (var item in CachingData.CurrentData.Server.Rooms)
                    {
                        if (item.InviteKey == key_entry.Text && (key_entry.Text != "" && key_entry.Text != null))
                        {
                            keyHave = true;
                        }

                        if (item.Name == name_entry.Text)
                        {
                            nameHave = true;
                        }
                    }

                    if (nameHave)
                    {
                        await DisplayAlert("Ошибка", "Данное имя комнаты уже занято", "Ок");
                        name_entry.Text = "";
                    }

                    if (keyHave)
                    {
                        await DisplayAlert("Ошибка", "Данный ключ комнаты уже используется", "Ok");
                    }
                    else
                    {
                        int id = CurrentData.Server.Rooms.Count + 1;

                        for (int i = 0; i < CurrentData.Server.Rooms.Count; i++)
                        {
                            if (i != CurrentData.Server.Rooms[i].Id)
                            {
                                id = i;
                                break;
                            }
                        }                        

                        Room result = await CurrentData.Server.AddRoom(new Room()
                        {
                            Id = id,
                            Name = name_entry.Text,
                            InviteKey = key_entry.Text == "" || key_entry.Text == null ? "" : key_entry.Text,
                            ParentRoom = parent,
                            PathToLogo = "",
                        });

                        await DisplayAlert("Id", id.ToString(), "Cancel"); //debug

                        int pid = CurrentData.Server.Rooms.Count + 1;

                        for (int i = 0; i < CurrentData.Server.Points.Count; i++)
                        {
                            if (i != CurrentData.Server.Points[i].Id)
                            {
                                pid = i;
                                break;
                            }
                        }

                        await CurrentData.Server.AddPoints(new Points()
                        {
                            Id = pid,
                            RoomId = id,
                            UserId = CurrentData.CurrentUser.Id,
                            Value = 500000
                        });

                        CachingData.CurrentData.UpdateData();
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", "Поле имени не заполнено!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ok");
                throw;
            }

        }
    }
}