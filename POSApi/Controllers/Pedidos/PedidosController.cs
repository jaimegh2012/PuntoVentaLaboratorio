using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSApi.Controllers.Pedidos.Dtos;
using POSApi.DB.PuntoVentaEntities;

namespace POSApi.Controllers.Pedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PuntoVentaEntities _db;
        public PedidosController(PuntoVentaEntities db)
        {
            this._db = db;
        }

        //GET: Obtener la lista de todos los pedidos
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            try
            {
                var data = _db.Pedidos.Where(x => x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodPedido,
                        x.CodCliente,
                        x.CodDireccion,
                        x.Total,
                        x.Activo,
                        Detalle = x.DetallePedidos
                        .Select(x => new
                        {
                            x.CodDetallePedido,
                            x.CodPedido,
                            x.CodProducto,
                            x.Cantidad,
                            x.Precio,
                            x.Total,
                        }).ToList()
                    })
                    .ToList();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //GET: Obtener un pedido por su id
        [Authorize]
        [HttpGet("{CodPedido}")]
        public async Task<IActionResult> GetPedido(int CodPedido)
        {
            try
            {
                var data = _db.Pedidos.Where(x => x.CodPedido == CodPedido && x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodPedido,
                        x.CodCliente,
                        x.CodDireccion,
                        x.Total,
                        x.Activo,
                        Detalle = x.DetallePedidos
                        .Select(x => new
                        {
                            x.CodDetallePedido,
                            x.CodPedido,
                            x.CodProducto,
                            x.Cantidad,
                            x.Precio,
                            x.Total,
                        }).ToList()
                    })
                    .FirstOrDefault();


                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Post: Crear un pedido
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostPedido(PedidoDTO data)
        {
            try
            {
                Pedido pedido = new()
                {
                    CodCliente = data.CodCliente,
                    CodDireccion = data.CodDireccion,
                    CodUsuario = null,
                    Total = data.Total,
                    Activo = data.Activo
                };

                await _db.Pedidos.AddAsync(pedido);
                await _db.SaveChangesAsync();

                foreach (var producto in data.DetallePedido)
                {
                    DetallePedido detalle = new()
                    {
                        CodPedido = pedido.CodPedido,
                        CodProducto = producto.CodProducto,
                        Precio = producto.Precio,
                        Cantidad = producto.Cantidad,
                        Total = producto.Total,
                        Activo = true,
                        Eliminado = false
                    };

                    await _db.DetallePedidos.AddAsync(detalle);
                    await _db.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Put: Actualizar un pedido
        [Authorize]
        [HttpPut("{CodPedido}")]
        public async Task<IActionResult> PutPedido(int CodPedido, PedidoDTO data)
        {
            try
            {
                var pedidoDb = await _db.Pedidos
                    .FirstOrDefaultAsync(x => x.CodPedido == CodPedido && x.Eliminado == false);

                if (pedidoDb == null)
                {
                    return NotFound("Registro no encontrado");
                }

                pedidoDb.CodCliente = data.CodCliente;
                pedidoDb.CodDireccion = data.CodDireccion;
                pedidoDb.Total = data.Total;

                await _db.SaveChangesAsync();

                foreach (var producto in data.DetallePedido)
                {
                    var detalleDb = await _db.DetallePedidos
                        .FirstOrDefaultAsync(x => x.CodDetallePedido == producto.CodDetallePedido);

                    if (detalleDb != null)
                    {
                        detalleDb.CodProducto = producto.CodProducto;
                        detalleDb.Precio = producto.Precio;
                        detalleDb.Cantidad = producto.Cantidad;
                        detalleDb.Total = producto.Total;

                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        DetallePedido newDetalle = new()
                        {
                            CodPedido = pedidoDb.CodPedido,
                            CodProducto = producto.CodProducto,
                            Precio = producto.Precio,
                            Cantidad = producto.Cantidad,
                            Total = producto.Total,
                            Activo = true,
                            Eliminado = false
                        };

                        await _db.DetallePedidos.AddAsync(newDetalle);
                        await _db.SaveChangesAsync();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Delete: Cambiar a estado eliminado un pedido
        [Authorize]
        [HttpDelete("{CodPedido}")]
        public async Task<IActionResult> DeletePedido(int CodPedido)
        {
            try
            {
                var pedidoDb = await _db.Pedidos
                    .FirstOrDefaultAsync(x => x.CodPedido == CodPedido && x.Eliminado == false);

                if (pedidoDb == null)
                {
                    return NotFound("Registro no encontrado");
                }

                pedidoDb.Activo = false;
                pedidoDb.Eliminado = true;

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}
