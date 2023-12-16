using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Quiz.ViewsModels
{
    public class PreguntasViewModels : INotifyPropertyChanged
    {
        private int _indicePreguntaActual ;
        private int _preguntasContestadas ;
        private int _preguntasCorrectas ;
        private bool _mensajeCorrectoVisible;
        private bool _mensajeIncorrectoVisible;
        private bool _ocultarMensajesControlVisible;
        private string _pregunta;
        private List<Preguntas> _preguntas;

        public List<Preguntas> Preguntas
        {
            get => _preguntas;
            set
            {
                _preguntas = value;
                OnPropertyChanged(nameof(Preguntas));
            }
        }
        public string Pregunta
        {
            get => _pregunta;
            set
            {
                _pregunta = value;
                OnPropertyChanged(nameof(Pregunta));
            }
        }

        public PreguntasViewModels()
        {

            CargarPreguntas();
            _indicePreguntaActual = _indicePreguntaActual;


        }
        private void CargarPreguntas()
        {
            // Aquí cargarías las preguntas desde donde sea necesario
            // Por ejemplo, podrías inicializar la lista directamente en el ViewModel
            List<Preguntas> _preguntas = new List<Preguntas> {

                 new Preguntas { Pregunta = "1.- El número atómico del litio es 17", EsCierto = false },
                 new Preguntas { Pregunta = "2.- El unicornio es el animal nacional de Escocia", EsCierto = true },
                 new Preguntas { Pregunta = "3.- La acrofobia es el miedo al ajo", EsCierto = true },
                 new Preguntas { Pregunta = "4.- Hay dos partes del cuerpo que no pueden curarse a sí mismas.", EsCierto = false },
                 new Preguntas { Pregunta = "5.- La Gran Muralla China es más larga que la distancia entre Londres y Pekín", EsCierto = true },
                 new Preguntas { Pregunta = "6.- Hay 219 episodios de Friends.", EsCierto = false },
                 new Preguntas { Pregunta = "7.- La A es la letra más utilizada en el idioma inglés", EsCierto = false },
                 new Preguntas { Pregunta = "8.- Hay cinco grupos sanguíneos diferentes", EsCierto = false },
                 new Preguntas { Pregunta = "9.- El café está hecho de bayas", EsCierto = true },
                 new Preguntas { Pregunta = "10.- La única letra que no está en la tabla periódica es la letra J", EsCierto = true },

            };

            // Después de cargar las preguntas, actualiza la propiedad Preguntas
            Preguntas = _preguntas;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public string PreguntaActual => _preguntas[_indicePreguntaActual].Pregunta;

        public ICommand SiguienteCommand => new Command(Siguiente);
        public ICommand AnteriorCommand => new Command(Anterior);
        public ICommand CiertoCommand => new Command(Cierto);
        public ICommand FalsoCommand => new Command(Falso);

        public void Siguiente()
        {
            _indicePreguntaActual = (_indicePreguntaActual + 1) % _preguntas.Count;
            OnPropertyChanged(nameof(PreguntaActual)); // Notificar cambio en la pregunta mostrada
            OcultarMensajesControlVisible = true; // Mostrar mensajes
            MostrarMensajeCorrecto();
            OcultarMensajes();
        }
        public void Anterior()
        {
            _indicePreguntaActual = (_indicePreguntaActual - 1 + _preguntas.Count) % _preguntas.Count;
            OnPropertyChanged(nameof(PreguntaActual)); // Notificar cambio en la pregunta mostrada
            OcultarMensajesControlVisible = true; // Mostrar mensajes
            MostrarMensajeIncorrecto();
            OcultarMensajes();
        }

        // Método para manejar la respuesta "Cierto"
        private void Cierto()
        {
            VerificarRespuesta(true);
        }

        // Método para manejar la respuesta "Falso"
        private void Falso()
        {
            VerificarRespuesta(false);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void VerificarRespuesta(bool respuestaUsuario)
        {
            bool respuestaCorrecta = _preguntas[_indicePreguntaActual].EsCierto;

            if (respuestaUsuario == respuestaCorrecta)
            {
                // La respuesta es correcta
                // Mostrar mensaje de "Correcto" y ocultar "Incorrecto"
                MostrarMensajeCorrecto();
                _preguntasCorrectas++;
                OnPropertyChanged(nameof(NumeroPreguntasCorrectasText));
            }
            else
            {
                // La respuesta es incorrecta
                // Mostrar mensaje de "Incorrecto" y ocultar "Correcto"
                MostrarMensajeIncorrecto();
            }
            // Incrementar el contador de preguntas contestadas
            _preguntasContestadas++;
            OnPropertyChanged(nameof(NumeroPreguntasContestadasText));
        }


        public int MostrarPreguntasContestadas
        {

            get => _preguntasContestadas;
            set
            {
                _preguntasContestadas = value;
                OnPropertyChanged(nameof(MostrarPreguntasContestadas));
                OnPropertyChanged(nameof(NumeroPreguntasContestadasText)); // Notificar cambio en la propiedad relacionada con la vista
            }
        }
        public string NumeroPreguntasContestadasText => $"Preguntas contestadas: {MostrarPreguntasContestadas}";

        // Mostrar el número de preguntas contestadas 
        public int MostrarPreguntasCorrectas
        {


            get => _preguntasCorrectas;
            set
            {
                _preguntasContestadas = value;
                OnPropertyChanged(nameof(MostrarPreguntasCorrectas));
                OnPropertyChanged(nameof(NumeroPreguntasCorrectasText)); // Notificar cambio en la propiedad relacionada con la vista
            }
        }
        public string NumeroPreguntasCorrectasText => $"Preguntas correctas: {MostrarPreguntasCorrectas   }";



        public bool MensajeCorrectoVisible
        {
            get => _mensajeCorrectoVisible;
            set
            {
                _mensajeCorrectoVisible = value;
              
                OnPropertyChanged(nameof(MensajeCorrectoVisible));
      
            }
        }

        public bool MensajeIncorrectoVisible
        {
            get => _mensajeIncorrectoVisible;
            set
            {
                _mensajeIncorrectoVisible = value;
               
                OnPropertyChanged(nameof(MensajeIncorrectoVisible));
                
            }
        }


       
        private void MostrarMensajeCorrecto()
        {
            MensajeCorrectoVisible = true;
            MensajeIncorrectoVisible = false;
            Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            {
                OcultarMensajesControlVisible = false; // Ocultar mensajes después de 0.5 segundos
                OnPropertyChanged(nameof(OcultarMensajesControlVisible));
                return false;
            });
        }

        private void MostrarMensajeIncorrecto()
        {
            MensajeCorrectoVisible = false;
            MensajeIncorrectoVisible = true;
            Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            {
                OcultarMensajesControlVisible = false; // Ocultar mensajes después de 0.5 segundos
                OnPropertyChanged(nameof(OcultarMensajesControlVisible));
                return false;
            });
        }

        private void OcultarMensajes()
        {
            MensajeCorrectoVisible = false;
            MensajeIncorrectoVisible = false;
            OcultarMensajesControlVisible = false;
            OnPropertyChanged(nameof(MensajeCorrectoVisible));
            OnPropertyChanged(nameof(MensajeIncorrectoVisible));
            OnPropertyChanged(nameof(OcultarMensajesControlVisible));
        }



        public bool OcultarMensajesControlVisible
        {
            get => _ocultarMensajesControlVisible;
            set
            {

                _ocultarMensajesControlVisible = value;
                OnPropertyChanged(nameof(OcultarMensajesControlVisible));
                
            }
        }




    }

}