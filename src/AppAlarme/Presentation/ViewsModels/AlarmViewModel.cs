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
        private TimeSpan horarioSelecionado;

        public string Descricao
        {
            get => descricao;
            set { descricao = value; OnPropertyChanged(); }
        }

        public string Horario
        {
            get => horario;
            set { horario = value; OnPropertyChanged(); }
        }

        public TimeSpan HorarioSelecionado
        {
            get => horarioSelecionado;
            set { horarioSelecionado = value; OnPropertyChanged(); }
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

        // Propriedades derivadas
        public string EstadoTexto => Status ? "ON" : "OFF";
        public Color CorDoTexto => Status ? Colors.White : Color.FromArgb("#3b4b91");
        public Color CorDoBotao => Status ? Colors.Green : Color.FromArgb("#b3c1ff");

        // Comando para alternar estado
        public ICommand AlternarEstadoCommand { get; }

        public AlarmViewModel()
        {
            AlternarEstadoCommand = new Command(() => Status = !Status);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}