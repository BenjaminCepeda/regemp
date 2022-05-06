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
            EmpleadoClient api;
            try
            {
                api = new EmpleadoClient();
                empleado = await api.login(txtUser.Text, txtPassword.Text);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (empleado == null)
                {
                    await DisplayAlert("Error de validación", "Credenciales incorrectas", "Continuar");
                }
                else
                {
                    await Navigation.PushAsync(new Inicio(empleado));
                }
            }
        }
    }
}
