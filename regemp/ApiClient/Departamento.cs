using System;
using System.Collections.Generic;
using System.Text;

namespace regemp.ApiClient
{
    public class Departamento 
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
        public Estado Estado {
            get{ return new Estado(estado); }
        }
    }
}
