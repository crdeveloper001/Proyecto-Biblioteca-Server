using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface ILibro
{
    List<LibroDTO> GetAllLibro();
    Object SearchLibroByName(string bookName);
    string RegisterNewLibro(LibroDTO autor);
    
    Object UpdateCurrentLibro(LibroDTO update);
  
    string DeleteSelectLibro(string id);
    
    String ErrorHandler(String errorMessage);
}