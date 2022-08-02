using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalBibliotecaController : ControllerBase
    {
        
        private PersonalBibliotecaService service = new PersonalBibliotecaService();

        //HTTP://LOCALHOST:5001/api/PersonalBiblioteca/GetAllAutores/
        [HttpGet]
        [Route("GetAllPersonalBiblioteca")]
        public async Task<IActionResult> GetAllPersonalBiblioteca()
        {
            if (service.GetAllPersonalBiblioteca() == null)
            {
                return Problem("NO EXISTE INFORMACION A CONSULTAR");
            }
            else
            {
                return Ok(service.GetAllPersonalBiblioteca());
            }
        }
        [Route("PostPersonalBiblioteca")]
        [HttpPost]
        public async Task<IActionResult> PostPersonalBiblioteca([FromBody] PersonalBibliotecaDTO persona)
        {
            if (persona != null)
            {
                return Ok(service.RegisterNewPersonalBiblioteca(persona));
            }
            else
            {
                return Conflict("Ocurrio un error al insertar el dato, verifique la peticion");
            }
        }
        [Route("PutPersonalBiblioteca")]
        [HttpPut]
        public async Task<IActionResult> PutPersonalBiblioteca([FromBody] PersonalBibliotecaDTO update)
        {
            if (update != null)
                {
                    return Ok(service.UpdateCurrentPersonalBiblioteca(update));
                }
                else
                {
                    return Conflict("Ocurrio un error al actualizar la informacion del usuario: " + update.nombre);
                }

        }
        //http://localhost:5001/api/PersonalBiblioteca/{cedula}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(string id)
        {
            if (id != "")
            {
                return Ok(service.DeleteSelectedPersonalBiblioteca(id));
            }
            else
            {
                return Conflict("Ocurrio un error al eliminar el usuario con id=> " + id +
                                "es posible que le dato no existe o el servidor no responde");
            }
        }
    }
}
