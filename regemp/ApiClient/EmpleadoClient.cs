using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using regemp.ApiClient;

namespace regemp
{

    public class EmpleadoClient : IApiClient<Empleado>
    {
        // NO FUNCIONA CON 127.0.0.1
        private const string API_URL = "http://192.168.1.2:8000/";
        private const string ENDPOINT_NAME = "empleado";
        HttpClient client;
        Uri uri;

        

        public EmpleadoClient() {
            client = new HttpClient();

        }


        public async Task<List<Empleado>> RefreshDataAsync()
        {
            List<Empleado> datos = null;
            Uri uri = new Uri(string.Format(API_URL + ENDPOINT_NAME, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<List<Empleado>>(content);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return datos;
        }


        public async Task SaveDataAsync(Empleado item, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(API_URL + ENDPOINT_NAME, string.Empty));
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {                    
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteDataAsync(string id)
        {
            Uri uri = new Uri(string.Format("{0}/?id={1}",API_URL + ENDPOINT_NAME, id));
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<Empleado> login(string username, string pwd)
        {
            Empleado empleado = null;
            Uri uri = new Uri(string.Format("{0}/?email={1}&pwd={2} ",API_URL + ENDPOINT_NAME, username, pwd));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Empleado> result  = JsonConvert.DeserializeObject<List<Empleado>>(content);
                    if (result != null && result.Count>0) {
                        empleado = result[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return empleado;
        }
    }

}
