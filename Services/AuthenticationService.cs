using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Biblioteca_Server.Services;

public class AuthenticationService :IAuthentication
{
    private DatabaseDAO dbaccess = new DatabaseDAO();
    
    public string ErrorHandler(string errorMessage)
    {
        return errorMessage;
    }
    public Object Authenticate(string email, string password)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var UserInfo = connection.QuerySingle(
                    $"select  * from personalbiblioteca where email='{email}' and contraseña = '{password}'");
                PersonalBibliotecaDTO payload = new PersonalBibliotecaDTO()
                {
                    usuario = UserInfo.usuario,
                    nombre = UserInfo.nombre,
                    apellidos = UserInfo.apellidos,
                    email = UserInfo.email,
                    direccion = UserInfo.direccion,
                    telefono = UserInfo.telefono
                };

                if (payload == null)
                {
                    return "CORREO O CONTRASEÑA INCORRECTO, VERIFIQUE LA INFORMACION";
                }
                else
                {
                    return payload;
                }

            }
        }
        catch (Exception errorAuthentication)
        {
            return ErrorHandler("EL CORREO O CONTRASEÑA SON INCORRECTOS, INTENTELO NUEVAMENTE");
        }
        
    }

  
}