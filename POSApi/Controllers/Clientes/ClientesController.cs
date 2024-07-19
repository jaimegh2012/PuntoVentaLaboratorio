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
    public class ClientesController : ControllerBase
    {
        private readonly PuntoVentaEntities _db;
        public ClientesController(PuntoVentaEntities db)
        {
            this._db = db;
        }

        //GET: Obtener la lista de todos los clientes
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var data = _db.Clientes.Where(x => x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodCliente,
                        x.NombreCliente,
                        x.Telefono,
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

        //GET: Obtener un cliente por su id
        [Authorize]
        [HttpGet("{CodCliente}")]
        public async Task<IActionResult> GetCliente(int CodCliente)
        {
            try
            {
                var data = _db.Clientes.Where(x => x.CodCliente == CodCliente && x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodCliente,
                        x.NombreCliente,
                        x.Telefono,
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

        //Post: Crear un cliente
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostCliente(ClienteDTO data)
        {
            try
            {
                Cliente cliente = new()
                {
                    NombreCliente = data.NombreCliente,
                    Telefono = data.Telefono,
                    Activo = data.Activo,
                    Eliminado = false
                };

                await _db.Clientes.AddAsync(cliente);
                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Put: Actualizar un cliente
        [Authorize]
        [HttpPut("{CodCliente}")]
        public async Task<IActionResult> PutCliente(int CodCliente, ClienteDTO data)
        {
            try
            {
                var ClienteDb = await _db.Clientes
                    .FirstOrDefaultAsync(x => x.CodCliente == CodCliente && x.Eliminado == false);

                if (ClienteDb == null)
                {
                    return NotFound("Cliente no encontrado");
                }

                ClienteDb.NombreCliente = data.NombreCliente;
                ClienteDb.Telefono = data.Telefono;
                ClienteDb.Activo = data.Activo;

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        //Delete: Cambiar a estado eliminado un cliente
        [Authorize]
        [HttpDelete("{CodCliente}")]
        public async Task<IActionResult> DeleteCliente(int CodCliente)
        {
            try
            {
                var clienteDb = await _db.Clientes
                    .FirstOrDefaultAsync(x => x.CodCliente == CodCliente && x.Eliminado == false);

                if (clienteDb == null)
                {
                    return NotFound("Cliente no encontrado");
                }

                clienteDb.Activo = false;
                clienteDb.Eliminado = true;

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
