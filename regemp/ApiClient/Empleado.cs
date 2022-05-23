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
        public int idUsuario { get; set; }
        public Usuario usuario { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public int idDepartamento { get; set; }
        public Departamento departamento { get; set; }
        public byte[] foto { get; set; }
        public int idEstado { get; set; }
        public Estado estado { get; set; }

        public Empleado()
        {

        }
    }
}