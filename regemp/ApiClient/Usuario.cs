using System;
using System.Collections.Generic;
using System.Text;

namespace regemp.ApiClient
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
        public int idPerfil {get;set;}
        public Perfil perfil { get; set; }
        public int estado { get; set; }
        public Estado Estado
        {
            get { return new Estado(estado); }
        }

    }
}
