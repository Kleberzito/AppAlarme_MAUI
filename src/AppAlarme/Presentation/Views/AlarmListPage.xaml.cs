using AppAlarme.src.AppAlarme.Presentation.ViewsModels;

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
        try
        {
            // Obtém o ViewModel atual do BindingContext
            var viewModel = BindingContext as AlarmListViewModel;
            if (viewModel == null)
            {
                await DisplayAlert("Erro", "ViewModel não encontrado", "OK");
                return;
            }

            // Passa o ViewModel para o construtor da página de criação
            await Navigation.PushAsync(new AlarmCreatePage(viewModel));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}