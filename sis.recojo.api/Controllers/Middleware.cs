using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sis.recojo.api.Models;
using System.Net;
using System.Security.Claims;

namespace sis.recojo.api.Controllers
{
    public class Middleware 
    {
        public readonly sisrecojoContext _dbcontext;
        public IConfiguration _Configuration;


        public Middleware(sisrecojoContext _context, IConfiguration Configuration)
        {
            _dbcontext = _context;
            _Configuration = Configuration;
        }

        public dynamic middleware(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar el token",
                        result = ""
                    };
                }
                var id = identity.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

                if (string.IsNullOrEmpty(id))
                {
                    return new
                    {
                        success = false,
                        message = "ID no encontrado en el token",
                        result = ""
                    };
                }

                Usuario usuario = _dbcontext.Usuarios.FirstOrDefault(x => x.UsuarioId == int.Parse(id));

                if (usuario == null)
                {
                    return new
                    {
                        success = false,
                        message = "Usuario no encontrado",
                        result = ""
                    };
                }

                return new
                {
                    success = true,
                    message = "verificado"
                };

            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "error", Response = "Usuario no encontrado" });
                return new
                {
                    success = false,
                    message = ex.Message
                };
            }
        }
    }

    //public ObjectResult middleware(ClaimsIdentity identity)
    //{
    //        try
    //        {
    //            if (identity.Claims.Count() == 0)
    //            {
    //                return new ObjectResult(new
    //                {
    //                    success = false,
    //                    message = "Verificar el token",
    //                    result = ""
    //                })
    //                {
    //                    StatusCode = (int)HttpStatusCode.Unauthorized
    //                };
    //            }

    //            var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

    //            if (string.IsNullOrEmpty(id))
    //            {
    //                return new ObjectResult(new
    //                {
    //                    success = false,
    //                    message = "ID no encontrado en el token",
    //                    result = ""
    //                })
    //                {
    //                    StatusCode = (int)HttpStatusCode.Unauthorized
    //                };
    //            }

    //            Usuario usuario = _dbcontext.Usuarios.FirstOrDefault(x => x.UsuarioId == int.Parse(id));

    //            if (usuario == null)
    //            {
    //                return new ObjectResult(new
    //                {
    //                    success = false,
    //                    message = "Usuario no encontrado",
    //                    result = ""
    //                })
    //                {
    //                    StatusCode = (int)HttpStatusCode.Unauthorized
    //                };
    //            }

    //            return new ObjectResult(new
    //            {
    //                success = true,
    //                message = "Token verificado"
    //            })
    //            {
    //                StatusCode = (int)HttpStatusCode.OK
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            return new ObjectResult(new
    //            {
    //                success = false,
    //                message = ex.Message,
    //                result = ""
    //            })
    //            {
    //                StatusCode = (int)HttpStatusCode.InternalServerError
    //            };
    //        }
    //    }

}
