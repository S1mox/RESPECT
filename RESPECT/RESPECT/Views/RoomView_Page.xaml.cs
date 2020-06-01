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
    public partial class RoomView_Page : ContentPage
    {
        internal Room CurrentRoom;
        internal RootsViewModel RootsViewModel { get; set; } = new RootsViewModel();

        internal TransferViewModel transfer = new TransferViewModel();

        public List<User> Users { get; set; }

        public List<Room> ChildsRoom { get; set; } = new List<Room>();  // Вложенные комнаты
        public List<User> Moderators { get; set; } = new List<User>();
        public List<User> Childs { get; set; } = new List<User>();      // Обычные участник
        public List<Points> ChildsPoints { get; set; } = new List<Points>();    // Очки участников

        public RoomView_Page(Room room)
        {
            InitializeComponent();
            
            CurrentRoom = CachingData.CurrentData.Server.Rooms.Where(r => r.Id == room.Id).ToList().First();

            Users = CachingData.CurrentData.GetUsersInRoom(CurrentRoom.Id);

            for (int i = 0; i < Users.Count; i++)
            {
                if (CachingData.CurrentData.GetPointsUserInRoom(Users[i].Id, CurrentRoom.Id) == 500000)
                {
                    Moderators.Add(Users[i]);
                }
                else
                {
                    Childs.Add(Users[i]);
                }
            }

            ChildsRoom = CachingData.CurrentData.GetChildsRooms(CurrentRoom);

            Room_image.SetBinding(Image.SourceProperty, new Binding() { Source = CurrentRoom, Path = "PathToLogo" });
            Room_image.Aspect = Aspect.AspectFill;

            Room_name.SetBinding(Label.TextProperty, new Binding() { Source = CurrentRoom, Path = "Name" });
            Room_key.SetBinding(Label.TextProperty, new Binding() { Source = CurrentRoom, Path = "InviteKey" });

            Rooms_list.ItemsSource = ChildsRoom;
            Moderators_list.ItemsSource = Moderators;

            if (!RootsViewModel.IsModerator(CurrentRoom))
            {
                pickup_button.IsVisible = false;
                give_button.IsVisible = false;
                create_button.IsVisible = false;
                seturi_button.IsVisible = false;
                seturi_button_uwp.IsVisible = false;
            }

            if (!RootsViewModel.IsMember(CurrentRoom))
            {
                enter_button.IsVisible = true;
            }

            if (RootsViewModel.IsMember(CurrentRoom))
            {
                exit_button.IsVisible = true;
            }

            if (ChildsRoom.Count == 0)
            {
                rooms_stack.IsVisible = false;
            }

            if (Moderators.Count == 0)
            {
                moderators_stack.IsVisible = false;
            }

            if (Childs.Count == 0)
            {
                users_stack.IsVisible = false;
            }

            for (int i = 0; i < Childs.Count; i++)
            {
                foreach (var item in CachingData.CurrentData.Server.Points)
                {
                    if (Childs[i].Id == item.UserId && CurrentRoom.Id == item.RoomId)
                    {
                        ChildsPoints.Add(item);
                    }
                }
            }            

            BindingContext = this;

            Update();
        }

        private async void Update()
        {
            List<Points> points = new List<Points>();
            List<User> childs = new List<User>();

            points.AddRange(ChildsPoints);
            childs.AddRange(Childs);

            try
            {
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        if (points[i].Value < points[j].Value)
                        {
                            Points temp = points[j];
                            points[j] = points[i];
                            points[i] = temp;

                            User tempu = childs[j];
                            childs[j] = childs[i];
                            childs[i] = tempu;
                        }
                    }
                }

                ChildsPoints.Clear();
                ChildsPoints.AddRange(points);

                Childs.Clear();
                Childs.AddRange(childs);

                Users_list.ItemsSource = Childs;
                Points_list.ItemsSource = ChildsPoints;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ок");
                throw;
            }
        }

        private async void GoToUser(object sender, ItemTappedEventArgs e)
        {
            User user = (User)e.Item;
            ListView list = (ListView)sender;

            string[] buttons = RootsViewModel.IsModerator(CurrentRoom) ?
                new string[] { "Открыть профиль", "Назначить модератором", "Удалить пользователя", "Передать очки" } 
                : RootsViewModel.IsMember(CurrentRoom) ? 
                new string[] { "Открыть профиль" , "Передать очки" } 
                :
                new string[] { "Открыть профиль" };

            string result = await DisplayActionSheet("Профиль", null, null, buttons);

            switch (result)
            {
                case "Открыть профиль":
                    await Navigation.PushAsync(new Views.Profile_Page(user));
                    break;

                case "Назначить модератором":
                    bool answer = await DisplayAlert("Подтверждение", $"Вы уверены, что хотите дать права модератора пользователю {user} ?", "Да", "Отмена");

                    if (answer)
                    {
                        CachingData.CurrentData.UpdateData();
                        transfer.GiveRoots(user.Id, CurrentRoom.Id);

                        Childs.Remove(user);
                        Moderators.Add(user);
                    }

                    break;

                case "Передать очки":
                    try
                    {
                        string value = await DisplayPromptAsync("Передача", "Сколько вы планируете перевести очков?");
                        string description = await DisplayPromptAsync("Передача", "Описание перевода", placeholder: "За что?");

                        if (!string.IsNullOrEmpty(value))
                        {
                            if (RootsViewModel.IsModerator(CurrentRoom))
                            {
                                transfer.GivePoints(user.Id, CachingData.CurrentData.CurrentUser.Id, CurrentRoom.Id, int.Parse(value), string.IsNullOrEmpty(description) ? "" : description);
                            }
                            else if (RootsViewModel.IsMember(CurrentRoom))
                            {
                                transfer.GivePoints(user.Id, CurrentRoom.Id, int.Parse(value), string.IsNullOrEmpty(description) ? "" : description);
                            }

                            await DisplayAlert("Передача", "Перевод выполнен успешно", "ОК");
                        }                     
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Передача", $"Перевод не выполнен, ошибка: {ex.Message}", "ОК");
                    }

                    break;

                case "Удалить пользователя":
                    try
                    {
                        bool isExit = await DisplayAlert("Удаление пользователя", $"Вы уверены, что хотите удалить пользователя {user} ?", "Да", "Отмена");

                        if (isExit)
                        {
                            Points points = CachingData.CurrentData.Server.Points
                            .Where(p => p.RoomId == CurrentRoom.Id && p.UserId == user.Id)
                            .ToList()
                            .First();

                            List<Notification> notifications = CachingData.CurrentData.Server.Notifications
                                .Where(n => n.IdReceiver == user.Id && n.IdRoom == CurrentRoom.Id).ToList();

                            await CachingData.CurrentData.Server.DeletePoints(points);

                            foreach (var item in notifications)
                            {
                                await CachingData.CurrentData.Server.DeleteNotification(item);
                            }

                            Childs.Remove(user);
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Ошибка", ex.Message, "Ок");
                        throw;
                    }

                    break;

                default:
                    break;
            }

            list.SelectedItem = null;
        }

        private async void PickupButton_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageGivePoints_Page(this, 0));
        }

        private async void GiveButton_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageGivePoints_Page(this, 1));
        }

        private void GoToRoom(object sender, ItemTappedEventArgs e)
        {
            Models.Room room = ((Models.Room)e.Item);
            ListView view = ((ListView)sender);

            Navigation.PushAsync(new RoomView_Page(room) { Title = room.Name });

            view.SelectedItem = null;
        }

        private async void CreateButton_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Room_create(CurrentRoom));
        }

        private void EnterToRoom(object sender, EventArgs e)
        {
            transfer.GeneratePoints(CachingData.CurrentData.CurrentUser.Id, CurrentRoom.Id, 1000);
            Childs.Add(CachingData.CurrentData.CurrentUser);

            enter_button.IsVisible = false;
        }  

        private async void SetPicture_clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Установка изображения", "Вставьте URL-ссылку изображения в поле", "Ок", "Отмена");

            if (result != null && result != "")
            {
                try
                {
                    await CachingData.CurrentData.Server.UpdateRoom(new Room()
                    {
                        Id = CurrentRoom.Id,
                        InviteKey = CurrentRoom.InviteKey,
                        Name = CurrentRoom.Name,
                        ParentRoom = CurrentRoom.ParentRoom,
                        PathToLogo = result
                    });

                    CachingData.CurrentData.UpdateData();

                    await DisplayAlert("New picture", CurrentRoom.PathToLogo, "Ok");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "Ок");
                    throw;
                }
            }
        }

        private async void ExitFromRoom(object sender, EventArgs e)
        {
            try
            {
                bool result = await DisplayAlert("Выход из группы", "Вы уверены, что хотите выйти из комнаты?", "Да", "Отмена");

                if (result)
                {
                    Points points = CachingData.CurrentData.Server.Points
                    .Where(p => p.RoomId == CurrentRoom.Id && p.UserId == CachingData.CurrentData.CurrentUser.Id)
                    .ToList()
                    .First();

                    List<Notification> notifications = CachingData.CurrentData.Server.Notifications
                        .Where(n => n.IdReceiver == CachingData.CurrentData.CurrentUser.Id && n.IdRoom == CurrentRoom.Id).ToList();

                    await CachingData.CurrentData.Server.DeletePoints(points);

                    foreach (var item in notifications)
                    {
                        await CachingData.CurrentData.Server.DeleteNotification(item);
                    }

                    exit_button.IsVisible = false;
                    enter_button.IsVisible = true;
                    Childs.Remove(CachingData.CurrentData.CurrentUser);
                    Moderators.Remove(CachingData.CurrentData.CurrentUser);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ок");
                throw;
            }
        }

        private async void GoToModerator(object sender, ItemTappedEventArgs e)
        {
            User user = (User)e.Item;
            await Navigation.PushAsync(new Profile_Page(user));
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
                    CurrentRoom = await CachingData.CurrentData.Server.UpdateRoom(new Room()
                    {
                        Id = CurrentRoom.Id,
                        InviteKey = CurrentRoom.InviteKey,
                        Name = CurrentRoom.Name,
                        ParentRoom = CurrentRoom.ParentRoom,
                        PathToLogo = result
                    });

                    CachingData.CurrentData.UpdateData();
                    
                    Room_image.SetBinding(Image.SourceProperty, new Binding() { Source = CurrentRoom, Path = "PathToLogo" });
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