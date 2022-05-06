using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace regemp
{
    public class ApiConnect
    {
        private const string API_URL = "http://172.26.112.1:8000/";
        private const string EMPLEADO_ENDPOINT = "empleado";
        private const string DEPARTEMENTO_ENDPOINT = "departamento";

        public Empleado empleadoLogueado { get; set; }

        public async void login(string usuario, string clave)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri= new Uri(API_URL + EMPLEADO_ENDPOINT);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept","application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode== HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Empleado> datos = JsonConvert.DeserializeObject<List<Empleado>>(content);
                if (datos == null)
                {
                    this.empleadoLogueado = null;
                }
                else {
                    this.empleadoLogueado = datos[0];
                }
            }

        }

    }

    public class Empleado
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public int perfil { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public int estado { get; set; }
        public int departamento { get; set; }
        public Empleado() { 
        }

    }

}
