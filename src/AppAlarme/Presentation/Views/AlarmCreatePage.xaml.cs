using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace AppAlarme.src.AppAlarme.Presentation.Views;

public partial class AlarmCreatePage : ContentPage
{
    public DateTime today = DateTime.Now;

    public AlarmCreatePage()
	{
        InitializeComponent();
        OnAppearing();

        AlarmTimeLabel.Text = DateTime.Now.ToString("HH:mm"); // valor inicial

        LabelMonth.Text = $"{today.ToString("MMMM", CultureInfo.CurrentCulture)} de {today.Year}"; 
    }
    private async void OnOpenListClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        OpenCalendar(today);
    }

    private void OpenCalendar(DateTime today)
    {        
        int nDaysMonth = DateTime.DaysInMonth(today.Year, today.Month);
        DateTime dayFirst = new DateTime(today.Year, today.Month, 1);        

        HeaderGrid.Children.Clear();

        for (int nDay = 1; nDay <= nDaysMonth; nDay++)
        {
            var date = new DateTime(today.Year, today.Month, nDay);
            var label = new Label
            {
                Text = nDay.ToString(),
                Padding = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromArgb("#262526")
            };

            int col = (int)date.DayOfWeek;
            int line = (nDay + (int)dayFirst.DayOfWeek - 1) / 7;

            HeaderGrid.Add(label, col, line);
        }
    }

    // Quando clicar em "Editar"
    private void OnEditTimeClicked(object sender, EventArgs e)
    {
        AlarmTimePicker.Focus(); // abre o seletor
    }

    // Quando o usuário escolher uma hora
    private void OnTimeChanged(object sender, EventArgs e)
    {
        AlarmTimeLabel.Text = AlarmTimePicker.Time.ToString(@"hh\:mm");
    }
}

