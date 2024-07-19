using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSApi.Controllers.Usuarios.Dtos;
using POSApi.DB.PuntoVentaEntities;
using POSApi.Security.AES;

namespace POSApi.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly PuntoVentaEntities _db;
        public UsuariosController(PuntoVentaEntities db)
        {
            this._db = db;
        }

        //GET: Obtener la lista de todos los usuarios
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var data = await _db.Usuarios.Where(x => x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodUsuario,
                        x.NombreUsuario,
                        Usuario = x.Usuario1,
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostUsuarios(UsuarioDTO data)
        {
            try
            {
                var usuarioEncontrado = _db.Usuarios.FirstOrDefault(x => x.Usuario1.Trim() == data.Usuario.Trim());
                if (usuarioEncontrado != null)
                {
                    return BadRequest("Usuario ya existe");
                }

                Usuario usuario = new()
                {
                    NombreUsuario = data.NombreUsuario,
                    Usuario1 = data.Usuario,
                    Clave = data.Clave,
                    Activo = true,
                    Eliminado = false
                };

                await _db.Usuarios.AddAsync(usuario);
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
