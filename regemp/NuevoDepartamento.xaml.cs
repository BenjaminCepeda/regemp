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
        bool esNuevoRegistro = true;
        public Estado estadoSeleccionado;

        public NuevoDepartamento()
        {
            InitializeComponent();
            departamento = new Departamento();
            cargaDatos();
        }
        public NuevoDepartamento(Departamento departamentoActual)
        {
            InitializeComponent();
            esNuevoRegistro = false;
            departamento = departamentoActual;
            cargaDatos();

        }

        async private void btnGrabar_Clicked(object sender, EventArgs e)
        {
            departamento.descripcion = txtDescripcion.Text;
            departamento.estado = estadoSeleccionado;
            DepartamentoClient api;
            try
            {
                api = new DepartamentoClient();
                await api.SaveDataAsync(departamento,esNuevoRegistro);
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
            departamento.idEstado = estado.id;
        }

        async private void cargaDatos() {
            EstadoClient estadoAPI = new EstadoClient();
            estados = await estadoAPI.GetAllDataAsync();
            pckEstados.ItemsSource = estados;
            if (!esNuevoRegistro) {
                estadoSeleccionado = departamento.estado;
                txtDescripcion.Text = departamento.descripcion;
                pckEstados.SelectedIndex = estados.FindIndex(a => a.id == estadoSeleccionado.id);
            }
            else { pckEstados.SelectedIndex = 1; }
        }
    }
}