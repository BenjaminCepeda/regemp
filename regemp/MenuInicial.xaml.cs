using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using regemp.ApiClient;

namespace regemp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuInicial : ContentPage
    {
        public MenuInicial(Empleado empleadoLogueado)
        {
            InitializeComponent();
            lblNombreUsuario.Text = empleadoLogueado.email;
        }
    }
}