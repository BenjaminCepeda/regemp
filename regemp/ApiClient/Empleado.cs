using System;
using System.Collections.Generic;
using System.Text;

namespace regemp.ApiClient
{
    public class Empleado
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public int perfil { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public int estado { get; set; }
        public int departamento { get; set; }
        public Empleado()
        {

        }
    }
}