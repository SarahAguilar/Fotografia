using Fotografias.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Fotografias.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoFotografia : ContentPage
    {
        public PagoFotografia()
        {
            InitializeComponent();
           
            BindingContext = new PickerViewModel(ContenedorMedidas, contenedorPickers, contenedorPagos);
            

        }

    }
}