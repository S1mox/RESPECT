using RESPECT.Models;
using RESPECT.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageGivePoints_Page : ContentPage
    {
        public Room Room { get; set; }
        public List<User> Members { get; set; }
        private int key;

        public ManageGivePoints_Page(RoomView_Page room, int key)
        {
            InitializeComponent();

            Room = room.CurrentRoom;
            Members = room.Childs;
            this.key = key;
            CommunicationEntry.SetBinding(Entry.TextProperty, new Binding() { Mode = BindingMode.TwoWay, Source = CommunicationStepper, Path = "Value" });
            ResultatEntry.SetBinding(Entry.TextProperty, new Binding() { Mode = BindingMode.TwoWay, Source = ResultatStepper, Path = "Value" });
            DeployingEntry.SetBinding(Entry.TextProperty, new Binding() { Mode = BindingMode.TwoWay, Source = DeployingStepper, Path = "Value" });
            OtherEntry.SetBinding(Entry.TextProperty, new Binding() { Mode = BindingMode.TwoWay, Source = OtherStepper, Path = "Value" });

            picker_member.ItemsSource = Members;
            picker_member.SelectedIndex = 0;

            Title = key == 0 ? "Снять очки" : "Передать очки";

            BindingContext = this;
        }

        private async void GivePointsClicked(object sender, EventArgs e)
        {
            try
            {
                int com = int.Parse(CommunicationEntry.Text);
                int res = int.Parse(ResultatEntry.Text);
                int deploy = int.Parse(DeployingEntry.Text);
                int other = int.Parse(OtherEntry.Text);

                int sum = com + res + deploy + other;

                if (picker_member.SelectedItem != null)
                {
                    User receiver = (User)picker_member.SelectedItem;

                    bool result = await DisplayAlert("Подтверждение", key == 0 ? $"Вы уверены, что хотите снять {sum} у пользователя {receiver} ?" : $"Вы уверены, что хотите перевести {sum} пользователю {receiver} ?", "Да", "Отмена");

                    if (result)
                    {
                        CachingData.CurrentData.UpdateData();
                        TransferViewModel transfer = new TransferViewModel();

                        if (key == 0)
                        {
                            transfer.PickUpPoints(receiver.Id, CachingData.CurrentData.CurrentUser.Id, Room.Id, sum, description_entry.Text);
                        }
                        else
                        {
                            transfer.GivePoints(receiver.Id, CachingData.CurrentData.CurrentUser.Id, Room.Id, sum, description_entry.Text);
                        }

                        await DisplayAlert("Результат перевода", "Выполнено", "Ок");
                    }
                    else
                    {
                        await DisplayAlert("Результат перевода", "Операция не будет выполнена", "Ок");
                    }
                }
                else
                {
                    await DisplayAlert("Результат перевода", "Пользователь не выбран", "Ок");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ок");
                throw;
            }

        }
    }
}