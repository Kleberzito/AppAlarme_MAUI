using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppAlarme.src.AppAlarme.Presentation.ViewsModels
{
    public class AlarmViewModel : INotifyPropertyChanged
    {
        private string descricao;
        private string horario;
        private bool status;

        public string Descricao
        { 
            get => descricao;
            set
            {
                descricao = value;
                OnPropertyChanged();
            }
        }

        public string Horario
        {
            get => horario;
            set
            {
                horario = value;
                OnPropertyChanged();
            }
        }

        public bool Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EstadoTexto));
                OnPropertyChanged(nameof(CorDoBotao));
                OnPropertyChanged(nameof(CorDoTexto));
            }
        }

        public string EstadoTexto => Status ? "ON" : "OFF";
        public Color CorDoTexto => Status ? Colors.White : Color.FromArgb("#3b4b91");
        public Color CorDoBotao => Status ? Colors.Green : Color.FromArgb("#b3c1ff");

        public ICommand AlternarEstadoCommand { get; }

        public AlarmViewModel()
        {
            AlternarEstadoCommand = new Command(() =>
            {
                Status = !Status;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class AlarmListViewModel
        {
            public ObservableCollection<AlarmViewModel> Alarmes { get; set; }
            public AlarmListViewModel() 
            {
                Alarmes = new ObservableCollection<AlarmViewModel> 
                {
                    new AlarmViewModel {Descricao = "Primeiro teste", Horario = "20:00", Status = false},
                    new AlarmViewModel {Descricao = "Segundo teste", Horario = "20:30", Status = false},
                };
            }
        }
    }
}
