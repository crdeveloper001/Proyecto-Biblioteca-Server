using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectorController : ControllerBase
    {
        
        private LectorService _service = new LectorService();

        //HTTP://LOCALHOST:5001/api/Lector/GetAllAutores/
        [HttpGet]
        [Route("GetAllLector")]
        public async Task<IActionResult> GetCredencials()
        {
            if (_service.GetAllLectores() == null)
            {
                return Problem("NO EXISTE INFORMACION A CONSULTAR");
            }
            else
            {
                return Ok(_service.GetAllLectores());
            }
        }
        
        [Route("PostSearchByName")]
        [HttpPost]
        public IActionResult PostSearchByName(string name)
        {
            if (name != String.Empty)
            {
                return Ok(_service.SearchLectorByName(name));
            }
            else
            {
                return BadRequest(
                    "Error al validar la informacion, verifique que haya enviado la informacion");
            }
        }
        [Route("PostLector")]
        [HttpPost]
        public async Task<IActionResult> PostLector([FromBody] LectorDTO autor)
        {
            if (autor != null)
            {
                return Ok(_service.RegisterNewLector(autor));
            }
            else
            {
                return Conflict("Ocurrio un error al insertar el dato, verifique la peticion");
            }
        }
        [Route("PutLector")]
        [HttpPut]
        public async Task<IActionResult> PutLector([FromBody] LectorDTO update)
        {
            if (update != null)
            {
                return Ok(_service.UpdateCurrentLector(update));
            }
            else
            {
                return Conflict("Ocurrio un error al actualizar la informacion del autor: " + update.nombre);
            }
        }
        //http://localhost:5001/api/Lector/25efd
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLector(string id)
        {
            if (id != "")
            {
                return Ok(_service.DeleteSelectLector(id));
            }
            else
            {
                return Conflict("Ocurrio un error al eliminar el actor con id=> " + id +
                                "es posible que le dato no existe o el servidor no responde");
            }
        }
        
    }
}
