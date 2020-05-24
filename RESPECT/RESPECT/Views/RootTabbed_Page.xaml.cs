using BottomBar.XamarinForms;
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
    public partial class RootTabbed_Page : BottomBarPage
    {
        private static DB_Data.Users CurrentUser = DB_Data.DB_Data_Controller.CurrentUser;

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
                Text = "Настройки",
                IconImageSource = ImageSource.FromFile("baseline_settings_black_18dp.png"),
                Order = ToolbarItemOrder.Primary,
            };

            Children.Add(new NavigationPage(new Profile_Page())
            {
                Title = "Профиль",
                BarBackgroundColor = Color.FromHex("#f5f5f6"),
                BarTextColor = Color.Black,
                IconImageSource = "baseline_account_box_black_18.png",

                ToolbarItems = { item },
            });

            for (int i = 0; i < Children.Count; i++)
            {
                BottomBarPageExtensions.SetTabColor(Children[i], Color.FromHex("#000a12"));
            }            
        }
    }
}