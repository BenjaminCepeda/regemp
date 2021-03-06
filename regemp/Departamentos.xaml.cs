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
    public partial class Departamentos : ContentPage
    {
        public List<Departamento> listaElementos;
        public Departamento itemSeleccionado;

        public Departamentos()
        {
            InitializeComponent();
            
        }
        async public void cargaElementos()
        {
            DepartamentoClient apiCliente = new DepartamentoClient();

            try
            {
                listaElementos = await apiCliente.GetAllDataAsync();
                if (listaElementos!= null) {
                    lstElementos.ItemsSource = listaElementos;
                }
            }
            catch (Exception ex){
                DependencyService.Get<IMensaje>().LongAlert("Error al acceder a datos: " + ex.Message);
            }

        }

        async private void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoDepartamento());
        }

        async private void btnBorrar_Clicked(object sender, EventArgs e)
        {
            var imgButton = sender as ImageButton;
            itemSeleccionado = (Departamento) imgButton.BindingContext;
            

            DepartamentoClient apiCliente = new DepartamentoClient();
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
            finally {
                cargaElementos();
            }
        }

        async private void btnEditar_Clicked(object sender, EventArgs e)
        {
            var imgButton = sender as ImageButton;
            itemSeleccionado = (Departamento)imgButton.BindingContext;
            await Navigation.PushAsync(new NuevoDepartamento(itemSeleccionado));
        }
        
        protected override void OnAppearing()
        {
            cargaElementos();
        }

    }
}