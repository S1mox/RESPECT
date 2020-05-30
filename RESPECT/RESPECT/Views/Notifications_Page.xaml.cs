using RESPECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications_Page : ContentPage
    {
        public List<Notification> Notifications { get; set; }
        public List<Notification_visual> Visuals { get; set; } = new List<Notification_visual>();

        public Notifications_Page()
        {
            InitializeComponent();

            Notifications = CachingData.CurrentData.Server.Notifications.Where(n => n.IdReceiver == CachingData.CurrentData.CurrentUser.Id).ToList();
            Notifications.Reverse();

            for (int i = 0; i < Notifications.Count; i++)
            {
                Visuals.Add(new Notification_visual(Notifications[i]));
            }

            notifications_list.ItemsSource = Visuals;

            OnChangedList(null, null);

            notifications_list.HasUnevenRows = true;

            BindingContext = this;
        }

        private async void OnChangedList(object sender, EventArgs args)
        {
            if (Notifications != null)
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

        private async void Refresh(object sender, EventArgs e)
        {
            try
            {
                List<Notification> notifications = CachingData.CurrentData.Server.Notifications.Where(n => n.IdReceiver == CachingData.CurrentData.CurrentUser.Id).ToList();

                Notifications.Clear();
                Notifications.AddRange(notifications);
                Notifications.Reverse();

                notifications_list.ItemsSource = Notifications;
            }
            catch (Exception ex)
            {
                await DisplayAlert($"Ошибка", ex.Message, "Ок");
                throw;
            }
        }
    }
}