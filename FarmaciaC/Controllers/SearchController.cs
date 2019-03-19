using FarmaciaC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Device.Location;
using System.Globalization;

namespace FarmaciaC.Controllers
{
    public class SearchController : ApiController
    {
        [HttpPost]
        public Task<IHttpActionResult> NearbyDrugstore(SearchModel data)
        {
            string producto = data.producto;

            using (farmaciacEntities db = new farmaciacEntities())
            {
                var coord = new GeoCoordinate(data.latitud, data.longitud);
                List<ProductSearchModel> lista = new List<ProductSearchModel>();
                List<ProductSearchModel> lista2 = new List<ProductSearchModel>();
                var Farmacia = db.farmacia.FirstOrDefault();
                   

                db.sucursal_producto.OrderBy(x => x.ID_SUCURSAL_PRODUCTO).ToList().ForEach(x =>
                {
                    db.sucursal.Where(s => s.ID_SUCURSAL == x.ID_SUCURSAL).ToList().ForEach(y =>
                    {

                        db.producto.Where(p => p.ID_PRODUCTO == x.ID_PRODUCTO && p.PRODUCTO1.Contains(producto)).ToList().ForEach(w =>
                        {

                            lista.Add(new ProductSearchModel() { sucursal = y.SUCURSAL1, idSucursal = y.ID_SUCURSAL, latitud = y.LATITUD, longitud = y.LONGITUD, direccion = y.DIRECCION, idSucursalProducto = x.ID_SUCURSAL_PRODUCTO, producto = w.PRODUCTO1, precio = Convert.ToDecimal(x.PRECIO), idFarmacia = Convert.ToInt32(Farmacia.ID_FARMACIA) });

                        });

                    });
                });

                if (lista.Count() != 0)
                {
                    foreach (var x in lista)
                    {
                        double lat = Convert.ToDouble(x.latitud, CultureInfo.CreateSpecificCulture("en-US"));
                        double lon = Convert.ToDouble(x.longitud, CultureInfo.CreateSpecificCulture("en-US"));
                        double distance = SearchModel.Distance(data.latitud, data.longitud, lat, lon);
                        if (distance < 2)
                        {
                            ProductSearchModel products = new ProductSearchModel();
                            products.producto = x.producto;
                            products.idSucursalProducto = x.idSucursalProducto;
                            products.idSucursal = x.idSucursal;
                            products.sucursal = x.sucursal;
                            products.latitud = x.latitud;
                            products.longitud = x.longitud;
                            products.direccion = x.direccion;
                            products.distancia = distance;
                            products.precio = x.precio;
                            products.idFarmacia = x.idFarmacia;
                            lista2.Add(products);
                        }
                    }
                    return Ok(lista2.OrderBy(x => x.distancia));
                }
                else
                {
                    Ok();
                }
            }

            return Ok();
            
        }
    }
}
