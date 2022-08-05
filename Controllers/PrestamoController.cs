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
    public class PrestamoController : ControllerBase
    {
        
         private PrestamoService _service = new PrestamoService();
         //HTTP://LOCALHOST:5001/api/Prestamo/GetAllPrestamos/
        [HttpGet]
        [Route("GetAllPrestamos")]
        public async Task<IActionResult> GetAllPrestamos()
        {
            if (_service.GetAllPrestamos() == null)
            {
                return Problem("NO EXISTE INFORMACION A CONSULTAR");
            }
            else
            {
                 return Ok(_service.GetAllPrestamos());
            }
        }
         
        [Route("PostSearchByName")]
        [HttpPost]
        public IActionResult PostSearchByName(string name)
        {
            if (name != String.Empty)
            {
                return Ok(_service.SearchByLectorPrestamo(name));
            }
            else
            {
                return BadRequest(
                    "Error al validar la informacion, verifique que haya enviado la informacion");
            }
        }
        [Route("PostPrestamo")]
        [HttpPost]
        public async Task<IActionResult> PostPrestamo([FromBody] PrestamoDTO prestamo)
        {
            if (prestamo != null)
            {
                return Ok(_service.RegisterNewPrestamo(prestamo));
            }
            else
            {
                return Conflict("Ocurrio un error al insertar el dato, verifique la peticion");
            }
        }
        [Route("PutPrestamo")]
        [HttpPut]
        public async Task<IActionResult> PutPrestamo([FromBody] PrestamoDTO update)
        {
            if (update != null)
                {
                    return Ok(_service.UpdateCurrentPrestamo(update));
                }
                else
                {
                    return Conflict("Ocurrio un error al actualizar la informacion del prestamo del libro: " + update.libro);
                }

        }
        //http://localhost:5001/api/Prestamo/{idPrestamo}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(string id)
        {
            if (id != "")
            {
                return Ok(_service.DeleteSelectPrestamo(id));
            }
            else
            {
                return Conflict("Ocurrio un error al eliminar el prestamo con id=> " + id +
                                "es posible que le dato no existe o el servidor no responde");
            }
        }
        
    }
}
