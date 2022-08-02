using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface ILibro
{
    List<LibroDTO> GetAllLibro();

    string RegisterNewLibro(LibroDTO autor);
    
    LibroDTO UpdateCurrentLibro(LibroDTO update);
  
    string DeleteSelectLibro(string id);
}