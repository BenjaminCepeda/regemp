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
    public partial class Usuarios : ContentPage
    {
        List<Usuario> listaElementos;
        public Usuario itemSeleccionado;

        public Usuarios()
        {
            InitializeComponent();

        }

        async private void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoUsuario());
        }

        async public void cargaElementos()
        {
            UsuarioClient apiCliente = new UsuarioClient();

            try
            {
                listaElementos = await apiCliente.GetAllDataAsync();
                if (listaElementos != null)
                {
                    this.ListaElementos.ItemsSource = listaElementos;
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMensaje>().LongAlert("Error al acceder a datos: " + ex.Message);
            }

        }

        async private void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var imgButton = sender as ImageButton;
            itemSeleccionado = (Usuario)imgButton.BindingContext;
            UsuarioClient apiCliente = new UsuarioClient();
            try
            {
                bool result = await DisplayAlert("Alerta", "¿Desea borrar el elemento?", "Si", "No");
                if (result)
                {
                    await apiCliente.DeleteDataAsync(itemSeleccionado.id.ToString());
                }

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMensaje>().LongAlert("Error al acceder a datos: " + ex.Message);
            }
            finally
            {
                cargaElementos();
            }

        }

        async private void btnEditar_Clicked(object sender, EventArgs e)
        {
            var imgButton = sender as ImageButton;
            itemSeleccionado = (Usuario)imgButton.BindingContext;
            await Navigation.PushAsync(new NuevoUsuario(itemSeleccionado));
        }

        protected override void OnAppearing()
        {
            cargaElementos();
        }

    }
}