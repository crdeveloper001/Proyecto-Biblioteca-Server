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
        
        private LectorService service = new LectorService();

        //HTTP://LOCALHOST:5001/api/Lector/GetAllAutores/
        [HttpGet]
        [Route("GetAllLector")]
        public async Task<IActionResult> GetCredencials()
        {
            if (service.GetAllLectores() == null)
            {
                return Problem("NO EXISTE INFORMACION A CONSULTAR");
            }
            else
            {
                return Ok(service.GetAllLectores());
            }
        }
        [Route("PostLector")]
        [HttpPost]
        public async Task<IActionResult> PostLector([FromBody] LectorDTO autor)
        {
            if (autor != null)
            {
                return Ok(service.RegisterNewLector(autor));
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
                return Ok(service.UpdateCurrentLector(update));
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
                return Ok(service.DeleteSelectLector(id));
            }
            else
            {
                return Conflict("Ocurrio un error al eliminar el actor con id=> " + id +
                                "es posible que le dato no existe o el servidor no responde");
            }
        }
        
    }
}
