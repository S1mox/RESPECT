using Xamarin.Forms;

namespace RESPECT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Data.AppData.CurrentUser == null)
            {
                MainPage = new Views.Login_Page(this); // если пользователь не идентифицирован                
            }
            else
            {
                MainPage = new Views.RootTabbed_Page(); // если пользователь уже есть
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
