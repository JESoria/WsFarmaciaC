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

                db.sucursal_producto.OrderBy(x => x.ID_SUCURSAL_PRODUCTO).ToList().ForEach(x =>
                {
                    db.producto.Where(y => y.ID_PRODUCTO == x.ID_PRODUCTO && y.PRODUCTO1.Contains(data.producto) && x.ID_SUCURSAL == data.idSucursal).ToList().ForEach(z =>
                    {
                        lista.Add(new ProductSearchModel
                        {
                            idSucursalProducto = x.ID_SUCURSAL_PRODUCTO,
                            idSucursal = x.ID_SUCURSAL,
                            producto = z.PRODUCTO1,
                            precio = Convert.ToDecimal(x.PRECIO)
                        });
                    });
                });
                return Ok(lista.OrderBy(x => x.precio));
            }
        }
    }
}
