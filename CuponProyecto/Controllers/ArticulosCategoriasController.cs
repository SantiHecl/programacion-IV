using CuponProyecto.Data;
using CuponProyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosCategoriasController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ArticulosCategoriasController(DataBaseContext context)
        {
            _context = context;
        }

        /*[HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloCategoria(ArticuloModel articuloModel)
        {

        }*/

    }
}
