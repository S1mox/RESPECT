using Xamarin.Forms;
using RESPECT.Views;

namespace RESPECT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CachingData.CurrentData.UpdateData();

            MainPage = new NavigationPage(new Login_Page(this))
            {               
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }

        protected override void OnStart()
        {
        }

        protected async override void OnSleep()
        {
            await SavePropertiesAsync();
        }

        protected override void OnResume()
        {
        }
    }
}
