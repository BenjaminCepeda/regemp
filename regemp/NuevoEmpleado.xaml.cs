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
    public partial class NuevoEmpleado : ContentPage
    {
        Empleado empleado;
        List<Usuario> usuarios;
        List<Departamento> departamentos;
        List<Estado> estados;
        bool esNuevoRegistro = true;
        Usuario usuarioSeleccionado = new Usuario();
        Departamento departamentoSeleccionado = new Departamento();
        Estado estadoSeleccionado = new Estado();

        public NuevoEmpleado()
        {
            InitializeComponent();
            empleado = new Empleado();
            cargaDatos();
        }

        public NuevoEmpleado(Empleado empleadoActual)
        {
            InitializeComponent();
            esNuevoRegistro = false;
            empleado = empleadoActual;
            cargaDatos();
        }

        async public void cargaDatos()
        {
            UsuarioClient usuarioAPI = new UsuarioClient();
            DepartamentoClient departamentoAPI = new DepartamentoClient();
            EstadoClient estadoAPI = new EstadoClient();
            estados = await estadoAPI.GetAllDataAsync();
            pckEstados.ItemsSource = estados;
            usuarios = await usuarioAPI.GetAllDataAsync();
            pckUsuarios.ItemsSource = usuarios;
            departamentos = await departamentoAPI.GetAllDataAsync();
            pckDepartamentos.ItemsSource = departamentos;

            try
            {
                if (!esNuevoRegistro)
                {
                    estadoSeleccionado = empleado.estado;
                    usuarioSeleccionado = empleado.usuario;
                    departamentoSeleccionado = empleado.departamento;
                    txtNombres.Text = empleado.nombres;
                    txtApellidos.Text = empleado.apellidos;
                    txtDireccion.Text = empleado.direccion;
                    txtCelular.Text = empleado.celular;
                    txtEmail.Text = empleado.email;

                    pckEstados.SelectedIndex = estados.FindIndex(a => a.id == estadoSeleccionado.id);
                    pckUsuarios.SelectedIndex = usuarios.FindIndex(a => a.id == usuarioSeleccionado.id);
                    pckDepartamentos.SelectedIndex = departamentos.FindIndex(a => a.id == departamentoSeleccionado.id);
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMensaje>().LongAlert("Error al acceder a datos: " + ex.Message);
            }
            finally
            {
                this.OnAppearing();
            }
        }

        private void pckUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Usuario usuario = (Usuario)picker.SelectedItem;
            empleado.idUsuario= usuario.id;
        }

        private void pckDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Departamento departamento = (Departamento)picker.SelectedItem;
            empleado.idDepartamento= departamento.id;

        }

        private void pckEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Estado estado= (Estado)picker.SelectedItem;
            empleado.idEstado = estado.id;

        }

        async private void btnGrabar_Clicked(object sender, EventArgs e)
        {
            empleado.nombres= txtNombres.Text;
            empleado.apellidos = txtApellidos.Text;
            empleado.direccion = txtDireccion.Text;
            empleado.celular = txtCelular.Text;
            empleado.email = txtEmail.Text;

            empleado.usuario = usuarioSeleccionado;
            empleado.departamento = departamentoSeleccionado;
            empleado.estado = estadoSeleccionado;

            EmpleadoClient api;
            try
            {
                api = new EmpleadoClient();
                await api.SaveDataAsync(empleado, esNuevoRegistro);
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
    }
}