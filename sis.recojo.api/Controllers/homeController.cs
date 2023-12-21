using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using sis.recojo.api.Models;

namespace sis.recojo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class homeController : ControllerBase
    {
        public readonly sisrecojoContext _dbcontext;

        public homeController(sisrecojoContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        [Route("all")]
        public IActionResult home()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                listaUsuarios = _dbcontext.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = listaUsuarios });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = listaUsuarios });
            }
        }
    }
}
