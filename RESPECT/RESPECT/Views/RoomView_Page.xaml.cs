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
    public partial class RoomView_Page : ContentPage
    {
        internal DB_Data.Rooms CurrentRoom;

        public RoomView_Page(DB_Data.Rooms room) 
        {
            InitializeComponent();

            CurrentRoom = room;

            //Debug();

            Room_image.SetBinding(Image.SourceProperty, new Binding() { Source = CurrentRoom, Path = "PathToLogo" });
            Room_image.Aspect = Aspect.AspectFill;

            Room_name.SetBinding(Label.TextProperty, new Binding() { Source = CurrentRoom, Path = "Name" });
        }

        public async void Debug()
        {
            await DisplayAlert(CurrentRoom.Name, CurrentRoom.PathToLogo, "ok");
        }
    }
}