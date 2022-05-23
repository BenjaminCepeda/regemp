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
    public partial class NuevoUsuario : ContentPage
    {
        Usuario usuario;
        List<Estado> estados;
        List<Perfil> perfiles;
        bool esNuevoRegistro = true;
        Perfil perfilSeleccionado = new Perfil();
        Estado estadoSeleccionado = new Estado();

        public NuevoUsuario()
        {
            InitializeComponent();
            usuario = new Usuario();
            cargaDatos();
            
        }
        public NuevoUsuario(Usuario usuarioActual)
        {
            InitializeComponent();
            esNuevoRegistro = false;
            usuario = usuarioActual;
            cargaDatos();            
        }

        async private void btnGrabar_Clicked(object sender, EventArgs e)
        {
            usuario.nombreUsuario= txtNombreUsuario.Text;
            usuario.clave = txtClave.Text;
            usuario.estado = estadoSeleccionado;
            usuario.perfil = perfilSeleccionado;

            UsuarioClient api;
            try
            {
                api = new UsuarioClient();
                await api.SaveDataAsync(usuario, esNuevoRegistro);
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
            usuario.idEstado = estado.id;
        }
        
        private void pckPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Perfil perfil = (Perfil)picker.SelectedItem;
            usuario.idPerfil = perfil.id;

        }
        async public void cargaDatos()
        {
            EstadoClient estadoAPI = new EstadoClient();
            PerfilClient perfilAPI = new PerfilClient();
            estados = await estadoAPI.GetAllDataAsync();
            pckEstados.ItemsSource = estados;
            perfiles = await perfilAPI.GetAllDataAsync();
            pckPerfiles.ItemsSource = perfiles;

            try
            {
                if (!esNuevoRegistro)
                {
                    estadoSeleccionado = usuario.estado;
                    perfilSeleccionado = usuario.perfil;
                    txtNombreUsuario.Text = usuario.nombreUsuario;
                    txtClave.Text = usuario.clave;
                    pckEstados.SelectedIndex = estados.FindIndex(a => a.id == estadoSeleccionado.id);
                    pckPerfiles.SelectedIndex = perfiles.FindIndex(a => a.id == perfilSeleccionado.id);
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMensaje>().LongAlert("Error al acceder a datos: " + ex.Message);
            }
            finally {
                this.OnAppearing();
            }
        }


    }
}