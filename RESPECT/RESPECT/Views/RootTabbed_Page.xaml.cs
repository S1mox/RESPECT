using BottomBar.XamarinForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootTabbed_Page : BottomBarPage
    {
        public RootTabbed_Page()
        {
            InitializeComponent();

            Children.Add(new NavigationPage(new ListViewRooms_Page())
            {
                Title = "Комнаты",
                BarBackgroundColor = Color.FromHex("#f5f5f6"),
                BarTextColor = Color.Black,
                IconImageSource = "baseline_horizontal_split_black_18.png",               
            });

            Children.Add(new NavigationPage(new Search_Page())
            {
                Title = "Поиск",
                BarBackgroundColor = Color.FromHex("#f5f5f6"),
                BarTextColor = Color.Black,
                IconImageSource = "sharp_search_black_18.png",
            });

            Children.Add(new NavigationPage(new Notifications_Page())
            {
                Title = "Уведомления",
                BarBackgroundColor = Color.FromHex("#f5f5f6"),
                BarTextColor = Color.Black,
                IconImageSource = "baseline_notifications_black_18.png"
            });


            ToolbarItem item = new ToolbarItem()
            {
                Text = "Выход из аккаунта",
                IconImageSource = ImageSource.FromFile("baseline_perm_identity_black_48.png"),
                Order = ToolbarItemOrder.Secondary,
            };

            ToolbarItem help = new ToolbarItem()
            {
                Text = "Справка",
                IconImageSource = ImageSource.FromFile("baseline_help_black_48dp.png"),
                Order = ToolbarItemOrder.Secondary
            };

            item.Clicked += Logout;
            help.Clicked += GetHelp;

            if (Device.RuntimePlatform == Device.UWP)
            {
                Children.Add(new NavigationPage(new Profile_Page())
                {
                    Title = "Профиль",
                    BarBackgroundColor = Color.FromHex("#f5f5f6"),
                    BarTextColor = Color.Black,
                    IconImageSource = "baseline_account_box_black_18.png",

                    ToolbarItems = { item, help }
                });
            }
            else
            {
                Children.Add(new NavigationPage(new Profile_Page())
                {
                    Title = "Профиль",
                    BarBackgroundColor = Color.FromHex("#f5f5f6"),
                    BarTextColor = Color.Black,
                    IconImageSource = "baseline_account_box_black_18.png",

                    ToolbarItems = { item }
                });
            }
           

            for (int i = 0; i < Children.Count; i++)
            {
                BottomBarPageExtensions.SetTabColor(Children[i], Color.FromHex("#000a12"));
            }
        }

        private async void Logout(object e, EventArgs args)
        {
            bool result = await DisplayAlert("Выход из аккаунта", "Вы уверены, что хотите выйти?", "Да", "Отмена");
            if (result)
            {
                CachingData.CurrentData.CurrentUser = null;

                Login_Page.MainPage.MainPage = new NavigationPage(new Login_Page(Login_Page.MainPage))
                {
                    BarBackgroundColor = Color.FromHex("#f5f5f6"),
                    BarTextColor = Color.Black,
                };
            }
        }

        private void GetHelp(object sender, EventArgs args)
        {
        }
    }
}