using AppAlarme.src.AppAlarme.Presentation.Enums;
using AppAlarme.src.AppAlarme.Presentation.ViewsModels;
using Microsoft.Maui.Graphics.Text;
using System.Globalization;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace AppAlarme.src.AppAlarme.Presentation.Views;

public partial class AlarmCreatePage : ContentPage
{
    public string nomeAlerta;
    public DateTime today = DateTime.Now;
    public int SizeBox { get; set; } = 30;
    private List<Button> _selectedButtons = new List<Button>();
    private AlarmListViewModel _listViewModel;   
    private AlarmViewModel _novoAlarme;
    private int _listDay = 0;
    private List<string> _strWeek { get; set; } = new List<string>();
    private static readonly Dictionary<string, DayOfWeek> DiasSemanaMap = new()
        {
            { "Dom", DayOfWeek.Sunday },
            { "Seg", DayOfWeek.Monday },
            { "Ter", DayOfWeek.Tuesday },
            { "Qua", DayOfWeek.Wednesday },
            { "Qui", DayOfWeek.Thursday },
            { "Sex", DayOfWeek.Friday },
            { "Sáb", DayOfWeek.Saturday }
        };

    public AlarmCreatePage(AlarmListViewModel listViewModel)
    {
        try
        {
            InitializeComponent();
            _listViewModel = listViewModel;
            _novoAlarme = new AlarmViewModel();
            BindingContext = _novoAlarme;

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
            if (_listViewModel == null || _listViewModel.Alarmes == null)
            {
                await DisplayAlert("Erro", "Lista de alarmes não disponível.", "OK");
                return;
            }

            if (AlarmTimePicker == null)
            {
                await DisplayAlert("Erro", "Controle de horário não encontrado.", "OK");
                return;
            }
            
            _novoAlarme.HorarioSelecionado = AlarmTimePicker.Time;
            _novoAlarme.Horario = AlarmTimePicker.Time.ToString(@"hh\:mm");
            
            if (_novoAlarme.DOWeek.Count == 0)
            {
                _novoAlarme.Status = false;                
            }
            else
            {
                _novoAlarme.Status = true;
            }

            if (_listDay == 7)
                _novoAlarme.Repeat = typeRepeat.Daily;
            else if (_listDay > 0)
                _novoAlarme.Repeat = typeRepeat.Weekly;
            else
                _novoAlarme.Repeat = typeRepeat.None;

            _novoAlarme.NomeAlerta = nomeAlerta.ToString();

            _listViewModel.Alarmes.Add(_novoAlarme);

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
        CreateCalendar(today);
    }

    private void InicializarControles()
    {
        AlarmTimePicker.Time = DateTime.Now.TimeOfDay;
        AlarmTimeLabel.Text = DateTime.Now.ToString("HH:mm");
        LabelMonth.Text = $"{DateTime.Today.ToString("MMMM", CultureInfo.CurrentCulture)} de {DateTime.Today.Year}";
    }

    private void CreateCalendar(DateTime today)
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
                    btn.BackgroundColor = Color.FromArgb("#D9E0FF");
                    btn.TextColor = Color.FromArgb("#3B4B91");
                    _selectedButtons.Remove(btn);
                }
                else
                {
                    // Marca e adiciona na lista                    
                    btn.BackgroundColor = Color.FromArgb("#546BCF");
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

    private void OnEditTimeClicked(object sender, EventArgs e)
    {
        AlarmTimePicker.Focus();
    }

    private void OnTimeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            AlarmTimeLabel.Text = AlarmTimePicker.Time.ToString(@"hh\:mm");
    }

    private void OnDiaSemanaClicked(object sender, EventArgs e)
    {        
        try
        {
            if (sender is Button btn && _novoAlarme != null)
            {
                if (!DiasSemanaMap.TryGetValue(btn.Text, out var dia))
                {
                    DisplayAlert("Erro", $"Dia inválido: {btn.Text}", "OK");
                    return;
                }

                if (_novoAlarme.DOWeek.Contains(dia))
                {
                    _strWeek.Remove(btn.Text);
                    _novoAlarme.DOWeek.Remove(dia);
                    _listDay--;
                    btn.BackgroundColor = Colors.Transparent;
                    btn.TextColor = Color.FromArgb("#3B4B91");
                }
                else
                {
                    _strWeek.Add(btn.Text);
                    _novoAlarme.DOWeek.Add(dia);
                    _listDay++;
                    btn.BackgroundColor = Color.FromArgb("#546BCF");
                    btn.TextColor = Color.FromArgb("#D9E0FF");
                }  
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"Falha ao processar seleção do dia: {ex.Message}", "OK");
            System.Diagnostics.Debug.WriteLine($"[OnDiaSemanaClicked] ERRO: {ex}");
        }
    }
}

