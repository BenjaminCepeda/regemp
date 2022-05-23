using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using regemp.ApiClient;
using regemp.util;
namespace regemp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoPerfil : ContentPage
    {
        Perfil perfil;
        List<Estado> estados;
        int esAdministrador = 0;
        Estado estadoSeleccionado ;
        bool esNuevoRegistro = true;

        public NuevoPerfil()
        {
            InitializeComponent();
            perfil = new Perfil();
            cargaDatos();
        }

        public NuevoPerfil(Perfil perfilActual)
        {
            InitializeComponent();
            esNuevoRegistro = false;
            perfil = perfilActual;
            cargaDatos();

        }

        private void swtEsAdministrador_Toggled(object sender, ToggledEventArgs e)
        {
            Switch swtEsAdministrador = sender as Switch;
            if (swtEsAdministrador.IsToggled)
            {
                esAdministrador = 1;
            }
            else
            {
                esAdministrador = 0;
            }
        }

        async private void btnGrabar_Clicked(object sender, EventArgs e)
        {
            perfil.descripcion = txtDescripcion.Text;
            perfil.esAdministrador = esAdministrador;
            perfil.estado = estadoSeleccionado;

            PerfilClient api;
            try
            {
                api = new PerfilClient();
                await api.SaveDataAsync(perfil, esNuevoRegistro);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMensaje>().LongAlert("Error al acceder a datos: " + ex.Message);
            }
            finally
            {
                DependencyService.Get<IMensaje>().LongAlert("Registro guardado");
                await Navigation.PopAsync();
            }
        }

        private void pckEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Estado estado = (Estado)picker.SelectedItem;
            perfil.idEstado = estado.id;
        }

        async private void cargaDatos()
        {
            EstadoClient estadoAPI = new EstadoClient();
            estados = await estadoAPI.GetAllDataAsync();
            pckEstados.ItemsSource = estados;
            if (!esNuevoRegistro)
            {
                estadoSeleccionado = perfil.estado;
                txtDescripcion.Text = perfil.descripcion;
                pckEstados.SelectedIndex = estados.FindIndex(a => a.id == estadoSeleccionado.id);
                if (perfil.esAdministrador > 0)
                { swtEsAdministrador.IsToggled = true;}
                else
                { swtEsAdministrador.IsToggled = false;}
            }
        }

    }
}