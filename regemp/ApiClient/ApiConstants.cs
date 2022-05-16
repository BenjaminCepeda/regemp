using System;
using System.Collections.Generic;
using System.Text;

namespace regemp.ApiClient
{
    static class ApiConstants
    {        
        // NO FUNCIONA CON 127.0.0.1
        // DIRECCION DEL SERVICIO PUBLICADO https://stormy-badlands-17405.herokuapp.com
        public const string API_URL = "https://stormy-badlands-17405.herokuapp.com/";
        public static Encoding ENCODING = Encoding.UTF8;
        public const string MEDIA_TYPE = "application/json";
        public const string ERROR_MESSAGE = @"\t{0} ERROR {1}";
        public const string SAVE_MESSAGE = @"\t{0} successfully saved.";
        public const string DELETE_MESSAGE = @"\t{0} successfully deleted.";

    }
}
