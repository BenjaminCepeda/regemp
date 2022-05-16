using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;



using System.Diagnostics;

namespace regemp.ApiClient
{
    public class DepartamentoClient : IApiClient<Departamento>
    {
        private const string ENDPOINT_NAME = "departamento";
        HttpClient client;

        public DepartamentoClient()
        {
            client = new HttpClient();
        }
        public async Task<List<Departamento>> GetAllDataAsync()
        {
            List<Departamento> datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<List<Departamento>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }

        public async Task<Departamento> GetDataAsync(string pwd)
        {
            Departamento datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME + "/{0}/", pwd));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Departamento>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }


        public async Task SaveDataAsync(Departamento item, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME+"/", string.Empty));
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

    }
}
