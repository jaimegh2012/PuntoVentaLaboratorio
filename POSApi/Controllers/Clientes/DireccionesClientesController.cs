using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSApi.Controllers.Clientes.Dtos;
using POSApi.DB.PuntoVentaEntities;

namespace POSApi.Controllers.Clientes
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionesClientesController : ControllerBase
    {
        private readonly PuntoVentaEntities _db;
        public DireccionesClientesController(PuntoVentaEntities db)
        {
            this._db = db;
        }

        //GET: Obtener la lista de todas las direcciones de un cliente
        [Authorize]
        [HttpGet("Cliente/{CodCliente}")]
        public async Task<IActionResult> GetDireccionesCliente(int CodCliente)
        {
            try
            {
                var data = _db.DireccionesClientes.Where(x => x.CodCliente == CodCliente && x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodDireccion,
                        x.CodCliente,
                        x.Direccion,
                        x.Activo
                    })
                    .ToList();


                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //GET: Obtener una direccion por su id
        [Authorize]
        [HttpGet("{CodDireccion}")]
        public async Task<IActionResult> GetDireccion(int CodDireccion)
        {
            try
            {
                var data = _db.DireccionesClientes.Where(x => x.CodDireccion == CodDireccion && x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodDireccion,
                        x.CodCliente,
                        x.Direccion,
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

        //Post: Crear una direccion
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostDireccion(DireccionDTO data)
        {
            try
            {
                DireccionesCliente direccion = new()
                {
                    CodCliente = data.CodCliente,
                    Direccion = data.Direccion,
                    Activo = true,
                    Eliminado = false
                };

                await _db.DireccionesClientes.AddAsync(direccion);
                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Put: Actualizar una direccion de cliente
        [Authorize]
        [HttpPut("{CodDireccion}")]
        public async Task<IActionResult> PutDireccionCliente(int CodDireccion, DireccionDTO data)
        {
            try
            {
                var direccionDb = await _db.DireccionesClientes
                    .FirstOrDefaultAsync(x => x.CodDireccion == CodDireccion && x.Eliminado == false);

                if (direccionDb == null)
                {
                    return NotFound("Registro no encontrado");
                }

                //direccionDb.CodCliente = data.CodCliente;
                direccionDb.Direccion = data.Direccion;
                direccionDb.Activo = data.Activo;

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Delete: Cambiar a estado eliminado una direccion de cliente
        [Authorize]
        [HttpDelete("{CodDireccion}")]
        public async Task<IActionResult> DeleteDireccionCliente(int CodDireccion)
        {
            try
            {
                var direccionDb = await _db.DireccionesClientes
                    .FirstOrDefaultAsync(x => x.CodDireccion == CodDireccion && x.Eliminado == false);

                if (direccionDb == null)
                {
                    return NotFound("Registro no encontrado");
                }

                direccionDb.Activo = false;
                direccionDb.Eliminado = true;

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
