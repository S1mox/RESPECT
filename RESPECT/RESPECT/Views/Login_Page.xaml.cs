using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Page : ContentPage
    {   
        private static App MainPage;

        public Login_Page(App app)
        {
            InitializeComponent();

            MainPage = app;
        }

        public async void Enter_clicked(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Server=tcp:respect.database.windows.net,1433;Initial Catalog=Respect_db;Persist Security Info=False;User ID=seamo_respect;Password=SqlProject2020;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                string sqlExpression = "SELECT * FROM dbo.Users";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    await DisplayAlert(".", "все выполнено верно" + connection.ClientConnectionId + " " + connection.Database, "ОК");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Фреймворк не подключился. " + ex.Message , "ОК");
                throw;
            }

            //bool result = false;

            //try
            //{
            //    using (DB_Data.Respect_dbContext respect_Db = new DB_Data.Respect_dbContext())
            //    {
            //        List<DB_Data.Users> users = respect_Db.Users.ToList();

            //        foreach (var item in users)
            //        {
            //            if (item.Login == login_entry.Text)
            //            {
            //                if (password_entry.Text == item.Password)
            //                {
            //                    //DB_Data.DB_Data_Controller.CurrentUser = item;
            //                    //MainPage.MainPage = new RootTabbed_Page();

            //                    await DisplayAlert("Авторизация", "Удачно", "Попробовать снова"); // отладка авторизации
            //                    result = true;
            //                }

            //            }
            //        }

            //        if (!result)
            //        {
            //            await DisplayAlert("Авторизация", "Ошибка авторизации, логин или пароль неверный", "Попробовать снова");
            //            login_entry.Text = "";
            //            password_entry.Text = "";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("error", ex.Message, " ok ");
            //    throw;
            //}
        }

        private void Register_clicked(object sender, EventArgs e)
        {

        }
    }
}