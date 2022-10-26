using Fotografias.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;


namespace Fotografias.ViewModels
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        public List<TipoFotografia> ListTipo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Propiedad para verificar que no se ha seleccionado una foto


        //private TipoFotografia _selectTipo { get; set; }

        //Propiedad para almacenar el tipo de fotografia
        private TipoFotografia _selectFoto;
        public TipoFotografia SelectFoto
        {
            get { return _selectFoto; }
            set
            {
                if (_selectFoto != value)
                {
                    _selectFoto = value;
                    SelectedFoto = _selectFoto.Key;
                }

            }
        }

        private int _selectedFoto;

        public int SelectedFoto
        {
            get { return _selectedFoto; }
            set
            {

                if (_selectedFoto != value)
                {
                    _selectedFoto = value;
                    OnPropertyChanged();
                }

            }
        }
        //Propiedades para verificar medidas

        private int _cantidadfotos;
        public int CantidadFotos
        {
            get { return _cantidadfotos; }
            set
            {
                _cantidadfotos = value;
                OnPropertyChanged();
            }
        }


        private double _totalpagar;
        public double TotalPagar
        {
            get { return _totalpagar; }
            set
            {
                _totalpagar = value;
                OnPropertyChanged();
            }
        }

        private bool _Medida1;

        public bool Medida1
        {
            get { return _Medida1; }
            set
            {
                _Medida1 = value;
                OnPropertyChanged();
            }
        }
        private bool _Medida2;
        public bool Medida2
        {
            get { return _Medida2; }
            set
            {
                _Medida2 = value;
                OnPropertyChanged();
            }
        }
        private bool _Medida3;
        public bool Medida3
        {
            get { return _Medida3; }
            set
            {
                _Medida3 = value;
                OnPropertyChanged();
            }
        }
        private bool _Medida4;
        public bool Medida4
        {
            get { return _Medida4; }
            set
            {
                _Medida4 = value;
                OnPropertyChanged();
            }
        }

        //Contructor del binding
        public PickerViewModel()
        {
            //ListTipo = GetTipo().OrderBy(t => t.Value).ToList();
        }

        public List<TipoFotografia> GetTipo()
        {
            var fotos = new List<TipoFotografia>()
            {
                new TipoFotografia() {Key = 1, Value = "Color"},
                new TipoFotografia() {Key = 2, Value = "Blanco y Negro"},
            };
            return fotos;
        }

        public async void CalculaPago()
        {


            //Validar que se escoja una medida

            if (_Medida1 == false && _Medida2 == false && _Medida3 == false && _Medida4 == false)
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Selecciona una medida", "Entendido");
                return;
            }
            if (_cantidadfotos == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Ingresa una cantidad mayor a 0", "Entendido");
                return;
            }
            if (SelectedFoto == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Seleccione un tipo de foto", "Entendido");
                return;
            }

            ////Calcular con seleccion de color
            if (SelectedFoto == 1)
            {
                TotalPagar = 0;
                if (_Medida1)
                {
                    //Foto a color 3 X 4
                    TotalPagar = _cantidadfotos * 5.5;
                }

                if (_Medida2)
                {

                    //Foto a color 4 X 6
                    TotalPagar = _cantidadfotos * 6.20;
                }
                if (_Medida3)
                {
                    //Foto a color 5 X 7
                    TotalPagar = _cantidadfotos * 7.5;
                }
                if (_Medida4)
                {
                    //Foto a color 6 X 8
                    TotalPagar = _cantidadfotos * 9;
                }
            }
            
            //Calcular con seleccion de B&N
            else if (SelectedFoto == 2)
            {
                TotalPagar = 0;
                if (_Medida1)
                {
                    //Foto a blanco y negro 3 X 4
                    TotalPagar = _cantidadfotos * 4;
                }
                if (_Medida2)
                {
                    //Foto a Blanco y negro 4 X 6
                    TotalPagar = _cantidadfotos * 5.20;
                }
                if (_Medida3)
                {
                    //Foto a Blanco y negro 5 X 7
                    TotalPagar = _cantidadfotos * 6;
                }
                if (_Medida4)
                {
                    //Foto a Blanco y negro 6 X 8
                    TotalPagar = _cantidadfotos * 7.90;
                }
            }



            

        }



        //Propiedades de clases

        public Command CalcularPagoComando { set; get; }
        public Command LimpiarComando { set; get; }
        public StackLayout contenedorMedida { set; get; }
        public StackLayout contenedorPicker { set; get; }
        public StackLayout contenedorPago { set; get; }


        //Constructor de las views y listas

        public PickerViewModel(StackLayout ContenedorMedida, StackLayout ContenedorPicker, StackLayout ContenedorPago)
        {
            ListTipo = GetTipo().OrderBy(t => t.Value).ToList();
            contenedorMedida = ContenedorMedida;
            contenedorPicker = ContenedorPicker;
            contenedorPago = ContenedorPago;
            CalcularPagoComando = new Command(CalculaPago);
            LimpiarComando = new Command(LimpiarSelect);
        }


        //Limpiar View
        public void LimpiarSelect()
        {
            string nombre;
            //Limpieza de RadioButtons
            foreach (View oview in contenedorMedida.Children)
            {
                nombre = oview.GetType().Name;
                if (nombre.Equals("RadioButton"))
                {
                    Medida1 = false;
                    Medida2 = false;
                    Medida3 = false;
                    Medida4 = false;

                }
            }
            foreach (View oview in contenedorPicker.Children)
            {
                nombre = oview.GetType().Name;
                if (nombre.Equals("Entry"))
                {
                    CantidadFotos = 0;
                }
            }
            foreach (View oview in contenedorPago.Children)
            {
                nombre = oview.GetType().Name;
                if (nombre.Equals("Label"))
                {

                }
            }
        }

    }


}
