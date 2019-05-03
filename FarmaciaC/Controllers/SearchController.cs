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
        public IHttpActionResult NearbyDrugstore(SearchModel data)
        {
            double lonC = data.longitud;
            double latC = data.latitud;

            using (farmaciacEntities db = new farmaciacEntities())
            {
                GeoCoordinate distanceFrom = new GeoCoordinate();
                GeoCoordinate distanceTo = new GeoCoordinate();
                distanceFrom.Latitude = latC;
                distanceFrom.Longitude = lonC;

                List<ProductSearchModel> lista = new List<ProductSearchModel>();
                var Farmacia = db.farmacia.FirstOrDefault();

                db.sucursal.ToList().ForEach(x =>
                {
                    double lat = Convert.ToDouble(x.LATITUD, CultureInfo.CreateSpecificCulture("en-US"));
                    double lon = Convert.ToDouble(x.LONGITUD, CultureInfo.CreateSpecificCulture("en-US"));
                    distanceTo.Latitude = lat;
                    distanceTo.Longitude = lon;
                    
                    double distance = distanceFrom.GetDistanceTo(distanceTo);
                    double d = distance / 1000;
                    if (d < 2)
                    {
                        x.sucursal_producto.Where(y => y.producto.PRODUCTO1.ToLower().Contains(data.producto.ToLower())).ToList().ForEach(y =>
                        {
                            lista.Add(new ProductSearchModel()
                            {
                                sucursal = x.SUCURSAL1,
                                idSucursal = x.ID_SUCURSAL,
                                latitud = x.LATITUD,
                                longitud = x.LONGITUD,
                                direccion = x.DIRECCION,
                                idSucursalProducto = y.ID_SUCURSAL_PRODUCTO,
                                producto = y.producto.PRODUCTO1,
                                precio = Convert.ToDecimal(y.PRECIO),
                                idFarmacia = Convert.ToInt32(Farmacia.ID_FARMACIA)
                            });
                        });
                    }
                });
                return Ok(lista);
            }
        }
    }
}
