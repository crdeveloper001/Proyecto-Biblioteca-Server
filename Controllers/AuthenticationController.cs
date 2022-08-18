using Biblioteca_Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationService _service = new AuthenticationService();

        //https://localhost:5001/api/Authentication/PostAuthentication?email=avargas26@gmail.com&password=admin1
        [Route("PostAuthentication")]
        [HttpPost]
        public IActionResult PostAuthentication(string email, string password)
        {
            if (email != String.Empty && password != String.Empty)
            {
                return Ok(_service.Authenticate(email, password));
            }
            else
            {
                return BadRequest(
                    "Error al validar la informacion, verifique que haya enviado la informacion de correo");
            }
        }
    }
}
