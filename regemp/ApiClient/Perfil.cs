using System;
using System.Collections.Generic;
using System.Text;

namespace regemp.ApiClient
{
    public class Perfil 
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public int esAdministrador { get; set; }
        public int idEstado { get; set; }
        public Estado estado { get; set; }

    }
}
