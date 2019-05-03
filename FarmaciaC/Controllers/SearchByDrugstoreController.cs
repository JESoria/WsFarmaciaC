using FarmaciaC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace FarmaciaC.Controllers
{
    public class SearchByDrugstoreController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SearchDrugstore(SearchModel data)
        {
            List<ProductSearchModel> lista = new List<ProductSearchModel>();

            using (farmaciacEntities db = new farmaciacEntities())
            {

                var Farmacia = db.farmacia.FirstOrDefault();

                db.sucursal_producto.Where(x => x.ID_SUCURSAL == data.idSucursal && x.producto.PRODUCTO1.ToLower().Contains(data.producto.ToLower())).ToList().ForEach(x =>
                {
                    lista.Add(new ProductSearchModel
                    {
                        idSucursalProducto = x.ID_SUCURSAL_PRODUCTO,
                        idSucursal = x.ID_SUCURSAL,
                        producto = x.producto.PRODUCTO1,
                        precio = Convert.ToDecimal(x.PRECIO),
                        sucursal = x.sucursal.SUCURSAL1,
                        latitud = x.sucursal.LATITUD,
                        longitud = x.sucursal.LONGITUD,
                        direccion = x.sucursal.DIRECCION,
                        idFarmacia = Convert.ToInt32(Farmacia.ID_FARMACIA)

                    });
                });
                return Ok(lista.OrderBy(x => x.precio));
            }
        }
    }
}
