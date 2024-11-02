using CuponProyecto.Data;
using CuponProyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuponProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public SolicitudCuponesController(DataBaseContext context)
        {
            _context = context;
        }

        // POST: api/Cupones
        [HttpPost]
        public async Task<ActionResult<CuponModel>> SolicitudCupon()
        {

        }
    }
}
