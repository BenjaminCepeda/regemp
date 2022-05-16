using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace regemp.ApiClient
{
    class EstadoClient
    {
        private const string ENDPOINT_NAME = "estado";

        public EstadoClient()
        {
        }

        public List<Estado> GetAllDataAsync()
        {
            List<Estado> datos = new List<Estado>();
            Estado estado = new Estado(1);
            datos.Add(estado);
            estado = new Estado(0);
            datos.Add(estado);
            return datos;
        }

    }
}
