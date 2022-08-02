using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Server.Controllers
{
    
    //HTTP://LOCALHOST:5001/api/Autor
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private AutorService service = new AutorService();

        //HTTP://LOCALHOST:5001/api/Autor/GetAllAutores/
        [HttpGet]
        [Route("GetAllAutores")]
        public IActionResult GetAllAutores()
        {
            if (service.GetAllAutores() == null)
            {
                return Problem("NO EXISTE INFORMACION A CONSULTAR");
            }
            else
            {
                return Ok(service.GetAllAutores());
            }
        }
        [Route("PostAutor")]
        [HttpPost]
        public IActionResult PostAutor([FromBody] AutorDTO autor)
        {
            if (autor != null)
            {
                return Ok(service.RegisterNewAutor(autor));
            }
            else
            {
                return Conflict("Ocurrio un error al insertar el dato, verifique la peticion");
            }
        }
        [Route("PutAutor")]
        [HttpPut]
        public async Task<IActionResult> PutAutor([FromBody] AutorDTO update)
        {
            if (update != null)
            {
                return Ok(service.UpdateCurrentAutor(update));
            }
            else
            {
                return Conflict("Ocurrio un error al actualizar la informacion del autor: " + update.nombre);
            }
        }
        //http://localhost:5001/api/Autor/25efd
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(string id)
        {
            if (id != "")
            {
                return Ok(service.DeleteSelectAutor(id));
            }
            else
            {
                return Conflict("Ocurrio un error al eliminar el actor con id=> " + id +
                                "es posible que le dato no existe o el servidor no responde");
            }
        }
    }
}