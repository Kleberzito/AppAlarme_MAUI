using AppAlarme.src.AppAlarme.Core.Models;
using AppAlarme.src.AppAlarme.Presentation.Enums;
using AppAlarme.src.AppAlarme.Presentation.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppAlarme.src.AppAlarme.Presentation.ViewsModels
{
    public class AlarmListViewModel
    {        
        public ObservableCollection<AlarmViewModel> Alarmes { get; set; }

        public ICommand AbrirAlarmCreateCommand { get; }
        public ICommand SalvarAlarmeCommand { get; }
        public ICommand AbrirAlarmEditCommand { get; }
        public ICommand ExcluirAlarmeCommand { get; }

        public AlarmListViewModel()
        {
            Alarmes = new ObservableCollection<AlarmViewModel>();

            var exemploAlarme = new AlarmViewModel
            {
                NomeAlerta = "Alarme de Exemplo",
                Descricao = "texto de descrição",
                Horario = DateTime.Now.AddMinutes(5).ToString("HH:mm"),
                HorarioSelecionado = DateTime.Now.AddMinutes(5).TimeOfDay,
                Status = true,
                Repeat = typeRepeat.Weekly,
                DOWeek = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday },
            };

            Alarmes.Add(exemploAlarme);

            AbrirAlarmCreateCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AlarmCreatePage(this));
            });

            AbrirAlarmEditCommand = new Command<AlarmViewModel>(async (alarmeSelecionado) =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AlarmCreatePage(this, alarmeSelecionado));
            });

            ExcluirAlarmeCommand = new Command<AlarmViewModel>(async (alarmeSelecionado) =>
            {
                if (alarmeSelecionado != null && Alarmes.Contains(alarmeSelecionado))
                {
                    Alarmes.Remove(alarmeSelecionado);
                    await Application.Current.MainPage.Navigation.PopAsync(); // volta para a lista
                }
            });
        }
    }
}