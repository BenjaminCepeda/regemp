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
            string nombreUsuario = "";
            string mensaje = validarIngreso(txtUser.Text, txtPassword.Text);
            if (mensaje!="") {
                await DisplayAlert("Error de validación", mensaje, "Continuar");
            }
            else
            {
                nombreUsuario = txtUser.Text;
                await Navigation.PushAsync(new Inicio(nombreUsuario));
            }
        }
        private string validarIngreso(string usuario, string clave)
        {
            string mensaje = "";
            if (usuario != "estudiante2022")
            {
                mensaje = "Usuario incorrecto !";
            } else if (clave != "uisrael2022")
            {
                mensaje = "Clave incorrecta !";
            }
            return mensaje;
        }
    }
}
