using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using regemp.util;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;

namespace regemp.ApiClient
{
    class EstadoClient
    {
        private const string ENDPOINT_NAME = "estado";

        HttpClient client;

        public EstadoClient()
        {
            client = new HttpClient();
        }
        public async Task<List<Estado>> GetAllDataAsync()
        {
            List<Estado> datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME+"/", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<List<Estado>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }

        public async Task<Estado> GetDataAsync(string id)
        {
            Estado datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME + "/{0}/", id));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Estado>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }


        public async Task SaveDataAsync(Estado item, bool isNewItem)
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

    }
}
