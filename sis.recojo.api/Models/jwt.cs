using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace sis.recojo.api.Models
{
    public class jwt
    {

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }


        //public static dynamic middleware(ClaimsIdentity identity) {
        //    try
        //    {
        //        if(identity.Claims.Count() == 0) 
        //        {
        //            return new
        //            {
        //                success = false,
        //                message = "Verificar el token",
        //                result = ""
        //            };
        //        }
        //        var id = identity.Claims.FirstOrDefault( x => x.Type == "id").Value;

        //        Usuario usuario = _dbcontext.Usuarios.FirstOrDefault(x => x.UsuarioId == int.Parse(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "error", Response = "Usuario no encontrado" });
        //    }
        //}
            
    }
}
 