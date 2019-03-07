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
        public async Task<IHttpActionResult> SearchDrugstore (SearchModel data)
        {
            List<ProductSearchModel> lista  = new List<ProductSearchModel>();

            using (farmaciacEntities  db = new farmaciacEntities())
            {

                db.sucursal_producto.OrderBy(x => x.id_sucursal_producto).ToList().ForEach(x =>
                {
                    db.producto.Where(y => y.id_producto == x.id_producto && y.producto1.Contains(data.producto) && x.id_sucursal == data.idSucursal).ToList().ForEach(z =>
                    {
                        lista.Add(new ProductSearchModel
                        {
                            idSucursalProducto = x.id_sucursal_producto,
                            idSucursal = x.id_sucursal,
                            producto = z.producto1,
                            precio = Convert.ToDecimal(x.precio)
                        });
                    });
                });
                return Ok(lista.OrderBy(x => x.precio));
            }
        }
    }
}
