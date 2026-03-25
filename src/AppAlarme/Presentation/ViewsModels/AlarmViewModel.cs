using AppAlarme.src.AppAlarme.Presentation.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppAlarme.src.AppAlarme.Presentation.ViewsModels
{
    public class AlarmViewModel : INotifyPropertyChanged
    {
        // 🔹 Campos privados
        private string descricao;
        private string nomeAlerta;
        private string horario;
        private bool status;
        private TimeSpan horarioSelecionado;
        private typeRepeat repeat = typeRepeat.None;        
        private List<DayOfWeek> doWeek = new();
        public string StrWeek
        {
            get
            {
                if (DOWeek.Count == 7)
                    return "Todos os dias";

                return string.Join(", ", DOWeek.Select(d => d switch
                {
                    DayOfWeek.Sunday => "Dom",
                    DayOfWeek.Monday => "Seg",
                    DayOfWeek.Tuesday => "Ter",
                    DayOfWeek.Wednesday => "Qua",
                    DayOfWeek.Thursday => "Qui",
                    DayOfWeek.Friday => "Sex",
                    DayOfWeek.Saturday => "Sáb",
                    _ => ""
                }));
            }
        }


        // 🔹 Propriedades públicas com encapsulamento
        public string Descricao
        {
            get => descricao;
            set { descricao = value; OnPropertyChanged(); }
        }

        public string NomeAlerta
        {
            get => nomeAlerta;
            set { nomeAlerta = value; OnPropertyChanged(); }
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

        public typeRepeat Repeat
        {
            get => repeat;
            set { repeat = value; OnPropertyChanged(); }
        }

        public List<DayOfWeek> DOWeek
        {
            get => doWeek;
            set { doWeek = value; OnPropertyChanged(); }
        }

        // 🔹 Propriedades derivadas
        public string EstadoTexto => Status ? "ON" : "OFF";
        public Color CorDoTexto => Status ? Colors.White : Color.FromArgb("#3b4b91");
        public Color CorDoBotao => Status ? Colors.Green : Color.FromArgb("#b3c1ff");

        // 🔹 Comando para alternar estado
        public ICommand AlternarEstadoCommand { get; }

        public AlarmViewModel()
        {
            AlternarEstadoCommand = new Command(() => Status = !Status);
        }

        // 🔹 Implementação do INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}