using System;
using System.Collections.Generic;
using System.Text;

namespace regemp.ApiClient
{
    public class Estado
    {
        public int id { get; set; }
        public string descripcion { get; set; }

        public Estado(int id)
        {
            this.id = id;
            if (id == 1)
                descripcion = "Activo";
            else
                descripcion = "Desactivado";
        }
    }
}
