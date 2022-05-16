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
    public partial class NuevoDepartamento : ContentPage
    {
        Departamento departamento;
        List<Estado> estados;
        int estadoSeleccionado=1;

        public NuevoDepartamento()
        {
            EstadoClient estadoAPI = new EstadoClient();
            InitializeComponent();
            departamento = new Departamento();
            estados = estadoAPI.GetAllDataAsync();
            pckEstados.ItemsSource = estados;

        }

        async private void btnGrabar_Clicked(object sender, EventArgs e)
        {
            departamento.descripcion = txtDescripcion.Text;
            departamento.estado = estadoSeleccionado;
            DepartamentoClient api;
            try
            {
                api = new DepartamentoClient();
                await api.SaveDataAsync(departamento,true);
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
            estadoSeleccionado = estado.id;
        }
    }
}