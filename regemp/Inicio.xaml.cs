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
    public partial class Inicio : ContentPage
    {
        Empleado empleado;
        public Inicio(Empleado empleadoLogueado)
        {
            InitializeComponent();
            empleado = empleadoLogueado;
            lblNombreUsuario.Text = empleado.nombres + " " + empleado.apellidos;
        }

        async private void btnDepartamentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Departamentos());
        }

        async private void btnPerfiles_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfiles());
        }

        async private void btnUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios());
        }

        private void btnEmpleados_Clicked(object sender, EventArgs e)
        {

        }

        async private void btnMenuBotones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuInicial());

        }
    }
}