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
        private const string ENDPOINT_NAME = "empleado";
        HttpClient client;

        public EmpleadoClient() {
            client = new HttpClient();

        }


        public async Task<List<Empleado>> GetAllDataAsync()
        {
            List<Empleado> datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME, string.Empty));
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
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }

        public async Task<Empleado> GetDataAsync(string pwd)
        {
            Empleado datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME + "/{0}/", pwd));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Empleado>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }

        public async Task SaveDataAsync(Empleado item, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME + "/", string.Empty));
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, ApiConstants.ENCODING, ApiConstants.MEDIA_TYPE);

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME + "/{0}/", item.id));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(string.Format(ApiConstants.SAVE_MESSAGE, ENDPOINT_NAME));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
        }

        public async Task DeleteDataAsync(string id)
        {
            Uri uri = new Uri(string.Format("{0}/{1}/", ApiConstants.API_URL + ENDPOINT_NAME, id));
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(string.Format(ApiConstants.DELETE_MESSAGE, ENDPOINT_NAME));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));                
            }
        }

        public async Task<Empleado> login(string username, string pwd)
        {
            Empleado empleado = null;
            Uri uri = new Uri(string.Format("{0}/?email={1}&pwd={2} ", ApiConstants.API_URL + ENDPOINT_NAME, username, pwd));
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
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return empleado;
        }
    }

}
