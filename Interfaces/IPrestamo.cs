using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface IPrestamo
{
    List<PrestamoDTO> GetAllPrestamos();

    string RegisterNewPrestamo(PrestamoDTO autor);
    
    PrestamoDTO UpdateCurrentPrestamo(PrestamoDTO update);
  
    string DeleteSelectPrestamo(string id);
}