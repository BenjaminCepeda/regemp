using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using regemp.util;
using System.Diagnostics;


namespace regemp.ApiClient
{
    class UsuarioClient : IApiClient<Usuario>
    {
        private const string ENDPOINT_NAME = "usuario";
        HttpClient client;

        public UsuarioClient()
        {
            client = new HttpClient();
        }
        public async Task<List<Usuario>> GetAllDataAsync()
        {
            List<Usuario> datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<List<Usuario>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }

        public async Task<Usuario> GetDataAsync(string id)
        {
            PerfilClient perfilClient = new PerfilClient();
            Usuario datos = null;
            Uri uri = new Uri(string.Format(ApiConstants.API_URL + ENDPOINT_NAME + "/{0}/", id));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Usuario>(content);
                    if (datos != null && datos.idPerfil > 0) 
                    {
                        Perfil p = await perfilClient.GetDataAsync(datos.idPerfil.ToString());
                        datos.perfil = p;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format(ApiConstants.ERROR_MESSAGE, ENDPOINT_NAME, ex.Message));
            }
            return datos;
        }


        public async Task SaveDataAsync(Usuario item, bool isNewItem)
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
