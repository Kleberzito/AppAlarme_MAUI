using AppAlarme.src.AppAlarme.Presentation.Views;

namespace AppAlarme
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new AlarmListPage());
        }
    }
}