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
        public async Task<IHttpActionResult> ProductDetail(ProductSearchModel data)
        {
            List<DetailModel> detalle = new List<DetailModel>();

            using (farmaciacEntities db = new farmaciacEntities())
            {
                db.sucursal_producto.Where(x => x.id_sucursal_producto == data.idSucursalProducto).ToList().ForEach(x => {
                    db.producto.Where(y => y.id_producto == x.id_producto).ToList().ForEach(y => {
                        db.presentacion.Where(z => z.id_presentacion == y.id_presentacion).ToList().ForEach(z => {
                            db.categoria.Where(c => c.id_categoria == y.id_categoria).ToList().ForEach(c => {
                                db.laboratorio.Where(l => l.id_laboratorio == y.id_laboratorio).ToList().ForEach(l =>
                                {
                                    detalle.Add(new DetailModel()
                                    {
                                        producto = y.producto1,
                                        presentacion = z.presentacion1,
                                        fechaVencimiento = x.fecha_vencimiento,
                                        laboratorio = l.laboratorio1,
                                        principiosActivos = y.descripcion,
                                        categoria = c.categoria1,
                                        precio = Convert.ToDouble(x.precio),
                                        existencia = x.existencia
                                    });
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
