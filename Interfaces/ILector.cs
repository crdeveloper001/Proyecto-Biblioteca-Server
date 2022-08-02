using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface ILector
{
   
    List<LectorDTO> GetAllLectores();

    string RegisterNewLector(LectorDTO autor);
    
    LectorDTO UpdateCurrentLector(LectorDTO update);
  
    string DeleteSelectLector(string id);
}