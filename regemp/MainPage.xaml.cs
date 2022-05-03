using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace regemp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private  async void btnAbrir_Clicked(object sender, EventArgs e)
        {
            Empleado empleado = null;
            empleado = validarIngreso(txtUser.Text, txtPassword.Text);
            if (empleado ==null) {
                await DisplayAlert("Error de validación", "Credenciales incorrectas", "Continuar");
            }
            else
            {

                await Navigation.PushAsync(new Inicio(empleado));
            }
        }
        private Empleado validarIngreso(string usuario, string clave)
        {
            ApiConnect api = new ApiConnect();
            api.login(usuario, clave);
            Empleado result = null;
            if (api.empleadoLogueado!= null)
            {
                result = api.empleadoLogueado;
            }
            return result;
        }
    }
}
