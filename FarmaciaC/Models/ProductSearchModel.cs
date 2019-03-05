using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaC.Models
{

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ProductSearchModel
    {
        [JsonProperty(PropertyName = "producto")]
        public string producto { get; set; }

        [JsonProperty(PropertyName = "precio")]
        public decimal precio { get; set; }

        [JsonProperty(PropertyName = "idSucursalProducto")]
        public int idSucursalProducto { get; set; }

        [JsonProperty(PropertyName = "idSucursal")]
        public int idSucursal { get; set; }

        [JsonProperty(PropertyName = "sucursal")]
        public string sucursal { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public string latitud { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public string longitud { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string direccion { get; set; }

        [JsonProperty(PropertyName = "distancia")]
        public double distancia { get; set; }

        [JsonProperty(PropertyName = "idFarmacia")]
        public int idFarmacia { get; set; }
    }
}