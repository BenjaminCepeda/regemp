using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using regemp.ApiClient;
using regemp.util;

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
                DependencyService.Get<IMensaje>().LongAlert("Error del sistema: " + ex.Message);
            }
            finally
            {
                if (empleado == null)
                {
                    DependencyService.Get<IMensaje>().LongAlert("Credenciales incorrectas");
                    //await DisplayAlert("Error de validación", "Credenciales incorrectas", "Continuar");
                }
                else
                {
                    await Navigation.PushAsync(new MenuInicial(empleado));
                }
            }
        }
    }
}
