using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using sis.recojo.api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace sis.recojo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly sisrecojoContext _dbcontext;
        public IConfiguration _Configuration;
        public readonly Middleware _middleware;


        public UsuarioController(sisrecojoContext _context, IConfiguration Configuration, Middleware middleware)
        {
            _dbcontext = _context;
            _Configuration = Configuration;
            _middleware = middleware;
        }

        [HttpPost]
        [Route("SignIn")]
        public dynamic SignIn([FromBody] Object req)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(req.ToString());
            
            if (data == null || data.usuario == null || data.contrasena == null)
            {
                return BadRequest(new { mensaje = "Datos de usuario incompletos" });
            }

            string user = data.usuario.ToString();
            string password = data.contrasena.ToString();
            
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { mensaje = "Credenciales incompletas" });
            }

            Usuario usuario = _dbcontext.Usuarios.Where(x => x.usuario == user && x.Contrasena == password).FirstOrDefault();

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }

            var jwt = _Configuration.GetSection("Jwt").Get<jwt>();

            var clains = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.UsuarioId.ToString()),
                new Claim("usuario", usuario.usuario),
                new Claim("nombre", usuario.Nombre),
                new Claim("puesto", usuario.Puesto),
                new Claim("AreaAsignada", usuario.AreaAsignada),
                new Claim("AdministradorId", usuario.AdministradorId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    clains,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: signingCredentials
                );
            return Ok(new { mensaje = "Usuario autenticado", Token = new JwtSecurityTokenHandler().WriteToken(token) });

        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public IActionResult All()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rtoken = _middleware.middleware(identity);

            if (!rtoken.success)
            {
                return Unauthorized(new { mensaje = rtoken.message});
            }

            List<Usuario> list = new List<Usuario>();
            try
            {
                list = _dbcontext.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = list });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = list });
            }
        }

        [HttpGet]
        [Route("{UsuarioId:int}")]
        [Authorize]
        public IActionResult obtenerUsuario( int UsuarioId)
        {
            Usuario usuario = _dbcontext.Usuarios.Find(UsuarioId);

            if(usuario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "error", Response = "Usuario no encontrado" });
                //return BadRequest("usuario no encontrado");
            }

            try
            {
                usuario = _dbcontext.Usuarios.Where(p => p.UsuarioId == UsuarioId).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = usuario });
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]
        public IActionResult agregarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("edit")]
        [Authorize]
        public IActionResult editarUsuario([FromBody] Usuario usuario)
        {
            Usuario usuario1 = _dbcontext.Usuarios.Find(usuario.UsuarioId);

            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "error", Response = "Usuario no encontrado" });
                //return BadRequest("usuario no encontrado");
            }

            try
            {
                usuario1.Nombre = usuario.Nombre is null ? usuario1.Nombre : usuario.Nombre;
                usuario1.usuario = usuario.usuario is null ? usuario1.usuario : usuario.usuario;
                usuario1.Nombre = usuario.Nombre is null ? usuario1.Nombre : usuario.Nombre;
                usuario1.Nombre = usuario.Nombre is null ? usuario1.Nombre : usuario.Nombre;
                usuario1.Nombre = usuario.Nombre is null ? usuario1.Nombre : usuario.Nombre;
                usuario1.Nombre = usuario.Nombre is null ? usuario1.Nombre : usuario.Nombre;

                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
