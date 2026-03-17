using System.Collections.ObjectModel;
using System.Windows.Input;
using AppAlarme.src.AppAlarme.Presentation.Views;

namespace AppAlarme.src.AppAlarme.Presentation.ViewsModels
{
    public class AlarmListViewModel
    {
        public ObservableCollection<AlarmViewModel> Alarmes { get; set; }

        public ICommand AbrirAlarmCreateCommand { get; }
        public ICommand SalvarAlarmeCommand { get; }

        public AlarmListViewModel()
        {
            Alarmes = new ObservableCollection<AlarmViewModel>(); 
            AbrirAlarmCreateCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AlarmCreatePage(this));
            });
        }
    }
}