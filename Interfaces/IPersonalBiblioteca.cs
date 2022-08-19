using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface IPersonalBiblioteca
{
    List<PersonalBibliotecaDTO> GetAllPersonalBiblioteca();
    Object SearchByName(string userName);
    string RegisterNewPersonalBiblioteca(PersonalBibliotecaDTO autor);
    
    Object UpdateCurrentPersonalBiblioteca(PersonalBibliotecaDTO update);
  
    string DeleteSelectedPersonalBiblioteca(string id);
    String ErrorHandler(String errorMessage);
}