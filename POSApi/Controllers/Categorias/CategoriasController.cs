using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.DB.PuntoVentaEntities;

namespace POSApi.Controllers.Categorias
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly PuntoVentaEntities _db;
        public CategoriasController(PuntoVentaEntities db)
        {
            _db = db;
        }

        //GET: Obtener la lista de todos las categorias
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                var data = _db.Categorias.Where(x => x.Eliminado == false)
                    .Select(x => new
                    {
                        x.CodCategoria,
                        x.NombreCategoria
                    })
                    .ToList();


                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}
