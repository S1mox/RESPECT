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
    public partial class Notifications_Page : ContentPage
    {
        public DB_Data.Users User { get; set; }
        public List<string> Notifications { get; set; }

        public Notifications_Page()
        {
            InitializeComponent();

            User = DB_Data.DB_Data_Controller.CurrentUser;
            Notifications = DB_Data.DB_Data_Controller.GetNotifications(User.Id);

            BindingContext = this;
        }

        private async void OnChangedList(object sender, EventArgs args)
        {
            if (User != null && Notifications != null)
            {
                try
                {
                    if (Notifications.Count > 0)
                    {
                        NoNotificaions_Label.IsVisible = false;
                        notifications_scroll.IsVisible = true;
                    }
                    else
                    {
                        notifications_scroll.IsVisible = false;
                        NoNotificaions_Label.IsVisible = true;
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Ошибка", e.Message, "Ok");
                    throw;
                }
            }
        }
    }
}