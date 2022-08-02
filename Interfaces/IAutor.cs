using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface IAutor
{
    //Retorna un listado total de los autores registrados
    List<AutorDTO> GetAllAutores();
    //Agrega un nuevo autor a la base de datos y devuelve como respuesta, un string
    string RegisterNewAutor(AutorDTO autor);
    //Actualiza un autor existente y devuelve como respuesta el objeto con los cambios implementados
    AutorDTO UpdateCurrentAutor(AutorDTO update);
    //Elimina un autor mediante su id como primary key y regresa como respuesta un string de confirmacion
    string DeleteSelectAutor(string id);
    
}