using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSApi.Controllers.Productos.Dtos;
using POSApi.Controllers.Usuarios.Dtos;
using POSApi.DB.PuntoVentaEntities;

namespace POSApi.Controllers.Productos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly PuntoVentaEntities _db;
        public ProductosController(PuntoVentaEntities db)
        {
            this._db = db;
        }

        //GET: Obtener la lista de todos productos
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var data = await _db.Productos.Where(x => x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodProducto,
                        x.NombreProducto,
                        x.Precio,
                        x.CodCategoria,
                        x.Activo
                    })
                    .ToListAsync();


                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //GET: Obtener un producto por su id
        [Authorize]
        [HttpGet("CodProducto")]
        public async Task<IActionResult> GetProducto(int CodProducto)
        {
            try
            {
                var data = _db.Productos.Where(x => x.CodProducto == CodProducto && x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodProducto,
                        x.NombreProducto,
                        x.Precio,
                        x.CodCategoria,
                        x.Activo
                    })
                    .FirstOrDefault();


                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Post: Crear un producto
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostProducto(ProductoDTO data)
        {
            try
            {
                Producto producto = new()
                {
                    NombreProducto = data.NombreProducto,
                    Precio = data.Precio,
                    CodCategoria = data.CodCategoria,
                    Activo = data.Activo,
                    Eliminado = false
                };

                await _db.Productos.AddAsync(producto);
                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Put: Actualizar un producto
        [Authorize]
        [HttpPut("{CodProducto}")]
        public async Task<IActionResult> PutProducto(int CodProducto, ProductoDTO data)
        {
            try
            {
                var productoDb = await _db.Productos
                    .FirstOrDefaultAsync(x => x.CodProducto == CodProducto && x.Eliminado == false);

                if (productoDb == null)
                {
                    return NotFound("Registro no encontrado");
                }

                productoDb.NombreProducto = data.NombreProducto;
                productoDb.Precio = data.Precio;
                productoDb.CodCategoria = data.CodCategoria;
                productoDb.Activo = data.Activo;

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Delete: Cambiar a estado eliminado un producto
        [Authorize]
        [HttpDelete("CodProducto")]
        public async Task<IActionResult> DeleteProducto(int CodProducto)
        {
            try
            {
                var productoDb = await _db.Productos
                    .FirstOrDefaultAsync(x => x.CodProducto == CodProducto && x.Eliminado == false);

                if (productoDb == null)
                {
                    return NotFound("Registro no encontrado");
                }

                productoDb.Activo = false;
                productoDb.Eliminado = true;
                
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
