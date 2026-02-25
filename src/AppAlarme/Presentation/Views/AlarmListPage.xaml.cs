using static AppAlarme.src.AppAlarme.Presentation.ViewsModels.AlarmViewModel;

namespace AppAlarme.src.AppAlarme.Presentation.Views;

public partial class AlarmListPage : ContentPage
{
	public AlarmListPage()
	{
        InitializeComponent();
        BindingContext = new AlarmListViewModel();
	}
    private async void OnOpenCreateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AlarmCreatePage());
    }
}