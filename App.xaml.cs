using AppAlarme.src.AppAlarme.Presentation.Views;

namespace AppAlarme
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var navPage = new NavigationPage(new AlarmListPage());
            NavigationPage.SetHasNavigationBar(navPage, false);

            MainPage = navPage;
        }
    }
}