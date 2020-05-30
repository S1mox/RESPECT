using RESPECT.Models;
using RESPECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration_Page : ContentPage
    {
        readonly ServerClientViewModel server = new ServerClientViewModel();

        public Registration_Page()
        {
            InitializeComponent();
        }

        private async void Registration_clicked(object sender, EventArgs e)
        {
            CachingData.CurrentData.UpdateData();

            if (login_entry.Text != "" && login_entry.Text != null)
            {
                if (password_entry.Text == confirm_password_entry.Text)
                {
                    if (new List<User>(server.Users.Where(u => u.Login == login_entry.Text).ToList()).Count == 0)
                    {
                        CachingData.CurrentData.UpdateData();

                        int id = CachingData.CurrentData.Server.Users.Count + 1;

                        for (int i = 0; i < CachingData.CurrentData.Server.Users.Count; i++)
                        {
                            if (i != CachingData.CurrentData.Server.Users[i].Id)
                            {
                                id = i;
                                break;
                            }
                        }
                        await server.AddUser(new User()
                        {
                            Id = id,
                            Login = login_entry.Text,
                            Password = password_entry.Text,
                            Name = name_entry.Text,
                            Status = "",
                            PathToImage = ""
                        });

                        CachingData.CurrentData.UpdateData();

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Ошибка регистрации", "Данный логин уже занят" + string.Join("\t", server.Users.Where(u => u.Login == login_entry.Text).ToList()), "Попробовать снова");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка регистрации", "Поля пароля и подтверждения пароля не совпадают", "Попробовать снова");
                }
            }
            else
            {
                await DisplayAlert("Ошибка регистрации", "Поле имени пустое пустое!", "ОК");
            }
        }
    }
}