using AppAlarme.src.AppAlarme.Presentation.ViewsModels;
using Microsoft.Maui.Graphics.Text;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace AppAlarme.src.AppAlarme.Presentation.Views;

public partial class AlarmCreatePage : ContentPage
{
    public DateTime today = DateTime.Now;
    public int SizeBox { get; set; } = 30;
    private List<Button> _selectedButtons = new List<Button>();
    private AlarmListViewModel _listViewModel;

    public AlarmCreatePage(AlarmListViewModel listViewModel)
    {
        try
        {
            InitializeComponent();
            _listViewModel = listViewModel;
            BindingContext = this;
        }
        catch (Exception ex)
        {
            // Isso exibirá a mensagem de erro em um alerta (se possível) ou no console
            System.Diagnostics.Debug.WriteLine($"ERRO: {ex}");
            throw; 
        }
    }

    private async void OnOpenListClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            // Verifica o ViewModel da lista
            if (_listViewModel == null)
            {
                await DisplayAlert("Erro", "ViewModel da lista não recebido.", "OK");
                return;
            }

            // Verifica a coleção de alarmes
            if (_listViewModel.Alarmes == null)
            {
                await DisplayAlert("Erro", "A lista de alarmes não foi inicializada no ViewModel.", "OK");
                return;
            }

            // Verifica o controle de tempo
            if (AlarmTimePicker == null)
            {
                await DisplayAlert("Erro", "Controle de horário não encontrado.", "OK");
                return;
            }

            var novoAlarme = new AlarmViewModel
            {
                Descricao = "Novo Alarme", // Se tiver um Entry, use Entry.Text
                Horario = AlarmTimePicker.Time.ToString(@"hh\:mm"),
                Status = false
            };

            _listViewModel.Alarmes.Add(novoAlarme);

            // Verifica a navegação
            if (Navigation == null)
            {
                await DisplayAlert("Erro", "Navegação indisponível.", "OK");
                return;
            }

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Exceção: {ex.Message}\n{ex.StackTrace}", "OK");
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        InicializarControles();
        OpenCalendar(today);
    }

    private void InicializarControles()
    {
        AlarmTimePicker.Time = DateTime.Now.TimeOfDay;
        AlarmTimeLabel.Text = DateTime.Now.ToString("HH:mm");
        LabelMonth.Text = $"{DateTime.Today.ToString("MMMM", CultureInfo.CurrentCulture)} de {DateTime.Today.Year}";
    }

    private void OpenCalendar(DateTime today)
    {        
        int nDaysMonth = DateTime.DaysInMonth(today.Year, today.Month);
        DateTime dayFirst = new DateTime(today.Year, today.Month, 1);        

        HeaderGrid.Children.Clear();

        for (int nDay = 1; nDay <= nDaysMonth; nDay++)
        {
            var date = new DateTime(today.Year, today.Month, nDay);

            var button = new Button
            {
                Text = nDay.ToString(),
                Padding = new Thickness(5),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromArgb("#3B4B91"),
                BackgroundColor = Colors.Transparent
            };

            button.Clicked += (s, e) =>
            {
                var btn = (Button)s;

                if (_selectedButtons.Contains(btn))
                {
                    // Se já estava selecionado, desmarca
                    btn.BackgroundColor = Colors.Transparent;
                    btn.TextColor = Color.FromArgb("#3B4B91");
                    _selectedButtons.Remove(btn);
                }
                else
                {
                    // Marca e adiciona na lista
                    btn.BackgroundColor = Color.FromArgb("#3B4B91");
                    btn.TextColor = Color.FromArgb("#D9E0FF");
                    _selectedButtons.Add(btn);
                }

                Console.WriteLine($"Dia {btn.Text} {(_selectedButtons.Contains(btn) ? "selecionado" : "removido")}");
            };

            int col = (int)date.DayOfWeek;
            int line = (nDay + (int)dayFirst.DayOfWeek - 1) / 7;

            HeaderGrid.Add(button, col, line);
        }
    }

    // Quando clicar em "Editar"
    private void OnEditTimeClicked(object sender, EventArgs e)
    {
        AlarmTimePicker.Focus();
    }

    private void OnTimeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            AlarmTimeLabel.Text = AlarmTimePicker.Time.ToString(@"hh\:mm");
    }
}

