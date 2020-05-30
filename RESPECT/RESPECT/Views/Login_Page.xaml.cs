using RESPECT.CachingData;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Page : ContentPage
    {
        public static App MainPage;
        List<Models.User> users = new List<Models.User>();

        public Login_Page(App app)
        {
            InitializeComponent();

            MainPage = app;
        }

        public async void Enter_clicked(object sender, EventArgs e)
        {
            try
            {
                bool result = false;
                users = CurrentData.Server.Users.ToList();

                foreach (var item in users)
                {
                    if (item.Login == login_entry.Text)
                    {
                        if (password_entry.Text == item.Password)
                        {
                            CurrentData.CurrentUser = item;

                            MainPage.MainPage = new RootTabbed_Page();

                            result = true;
                        }
                    }
                }

                if (!result)
                {
                    await DisplayAlert("Авторизация", "Логин или пароль неверные", "Попробовать снова"); // отладка авторизации
                    login_entry.Text = "";
                    password_entry.Text = "";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }

        private void Register_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registration_Page());
        }
    }
}