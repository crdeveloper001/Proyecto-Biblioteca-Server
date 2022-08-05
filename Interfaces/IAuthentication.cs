using Biblioteca_Server.DTO;

namespace Biblioteca_Server.Interfaces;

public interface IAuthentication
{
    Object Authenticate(String email, String password);
    String ErrorHandler(String errorMessage);
}