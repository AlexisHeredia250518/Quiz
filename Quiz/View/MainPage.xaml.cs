using Quiz.ViewsModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace Quiz
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent(); // Inicializa los componentes de la página (interfaz gráfica)
            var preguntasViewModels = new PreguntasViewModels();
            BindingContext = preguntasViewModels;

        }

        
    }
}
