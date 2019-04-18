using FarmaciaC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FarmaciaC.Controllers
{
    public class DetailController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ProductDetail(ProductSearchModel data)
        {
            DetailModel detalle = null;

            using (farmaciacEntities db = new farmaciacEntities())
            {
                db.sucursal_producto.Where(x => x.ID_SUCURSAL_PRODUCTO == data.idSucursalProducto).ToList().ForEach(x =>
                {
                    db.producto.Where(y => y.ID_PRODUCTO == x.ID_PRODUCTO).ToList().ForEach(y =>
                    {
                        db.presentacion.Where(z => z.ID_PRESENTACION == y.ID_PRESENTACION).ToList().ForEach(z =>
                        {
                            db.categoria.Where(c => c.ID_CATEGORIA == y.ID_CATEGORIA).ToList().ForEach(c =>
                            {
                                db.laboratorio.Where(l => l.ID_LABORATORIO == y.ID_LABORATORIO).ToList().ForEach(l =>
                                {
                                    detalle = new DetailModel()
                                    {
                                        producto = y.PRODUCTO1,
                                        presentacion = z.PRESENTACION1,
                                        fechaVencimiento = x.FECHA_VENCIMIENTO.ToShortDateString(),
                                        laboratorio = l.LABORATORIO1.Trim(),
                                        principiosActivos = y.DESCRIPCION,
                                        categoria = c.CATEGORIA1,
                                        precio = Convert.ToDouble(x.PRECIO),
                                        existencia = x.EXISTENCIA
                                    };
                                });
                            });
                        });
                    });
                });

                return Ok(detalle);
            }
        }
    }
}
