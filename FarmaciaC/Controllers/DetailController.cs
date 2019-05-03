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
                    detalle = new DetailModel()
                    {
                        producto = x.producto.PRODUCTO1,
                        presentacion = x.producto.presentacion.PRESENTACION1,
                        fechaVencimiento = x.FECHA_VENCIMIENTO.ToShortDateString(),
                        laboratorio = x.producto.laboratorio.LABORATORIO1.Trim(),
                        principiosActivos = x.producto.DESCRIPCION.Trim().Replace("\t"," "),
                        categoria = x.producto.categoria.CATEGORIA1,
                        precio = Convert.ToDouble(x.PRECIO),
                        existencia = x.EXISTENCIA
                    };
                });

                return Ok(detalle);
            }
        }
    }
}
