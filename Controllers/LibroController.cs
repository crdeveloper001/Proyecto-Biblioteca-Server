using Biblioteca_Server.DTO;
using Biblioteca_Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private LibroService service = new LibroService();

        //HTTP://LOCALHOST:5001/api/Libro/GetAllAutores/
        [HttpGet]
        [Route("GetAllLibro")]
        public async Task<IActionResult> GetAllLibro()
        {
            await Task.Run((() =>
            {
                if (service != null && service.GetAllLibro() == null)
                {
                    return Problem("NO EXISTE INFORMACION A CONSULTAR");
                }
                else
                {
                    return Ok(service.GetAllLibro());
                }
            }));

            return null!;
        }

        [Route("PostLibro")]
        [HttpPost]
        public async Task<IActionResult> PostLibro([FromBody] LibroDTO libro)
        {
            if (libro != null)
            {
                return Ok(service.RegisterNewLibro(libro));
            }
            else
            {
                return Conflict("Ocurrio un error al insertar el dato, verifique la peticion");
            }
        }

        [Route("PutLibro")]
        [HttpPut]
        public async Task<IActionResult> PutLibro([FromBody] LibroDTO update)
        {
            if (update != null)
            {
                return Ok(service.UpdateCurrentLibro(update));
            }
            else
            {
                return Conflict("Ocurrio un error al actualizar la informacion del libro: " + update.titulo);
            }
        }

        //http://localhost:5001/api/Lector/25efd
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(string id)
        {
            if (id != "")
            {
                return Ok(service.DeleteSelectLibro(id));
            }
            else
            {
                return Conflict("Ocurrio un error al eliminar el Libro con id=> " + id +
                                "es posible que le dato no existe o el servidor no responde");
            }
        }
    }
}