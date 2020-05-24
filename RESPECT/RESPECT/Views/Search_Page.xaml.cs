using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RESPECT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search_Page : ContentPage
    {       
        private static DB_Data.Users CurrentUser;

        public Search_Page()
        {
            InitializeComponent();

            CurrentUser = DB_Data.DB_Data_Controller.CurrentUser;            
        }
    }
}
