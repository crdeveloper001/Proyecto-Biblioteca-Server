using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Biblioteca_Server.Services;

public class PersonalBibliotecaService : IPersonalBiblioteca
{
    private DatabaseDAO dbaccess = new DatabaseDAO();
    public string ErrorHandler(string errorMessage)
    {
        return errorMessage;
    }
    public List<PersonalBibliotecaDTO> GetAllPersonalBiblioteca()
    {
        using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                    "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                    dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
        {
            return connection.Query<PersonalBibliotecaDTO>("SELECT * FROM personalbiblioteca").ToList();
        }
    }

    public object SearchByName(string userName)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var UserInfo = connection.QuerySingle(
                    $"select  * from personalbiblioteca where usuario='{userName}'");

                PersonalBibliotecaDTO payload = new PersonalBibliotecaDTO()
                {
                    nombre = UserInfo.nombre,
                    apellidos = UserInfo.apellidos,
                    direccion = UserInfo.direccion,
                    email = UserInfo.email,
                    telefono = UserInfo.telefono,
                    usuario = UserInfo.usuario,
                    contraseña = "not available for security reasons",
                };
                if (payload == null)
                {
                    return ErrorHandler($"NO SE ENCONTRO INFORMACION SOBRE->{userName}, INTENTELO NUEVAMENTE");
                }
                else
                {
                    return payload;
                }

            }
        }
        catch (Exception errorAuthentication)
        {
            return ErrorHandler($"NO SE ENCONTRO INFORMACION SOBRE->{userName}, INTENTELO NUEVAMENTE");
        }
    }

    public string RegisterNewPersonalBiblioteca(PersonalBibliotecaDTO personal)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"INSERT INTO personalbiblioteca(usuario,contraseña,nombre,apellidos,email,telefono,direccion) " +
                    $"VALUES ('{personal.usuario}','{personal.contraseña}','{personal.nombre}','{personal.apellidos}','{personal.email}','{personal.telefono}','{personal.direccion}')");

                return "Usuario: " + personal.usuario + " ha sido insertado en la base de datos";
            }
        }
        catch (Exception errorInsert)
        {
            return errorInsert.Message;
        } 
    }

    public PersonalBibliotecaDTO UpdateCurrentPersonalBiblioteca(PersonalBibliotecaDTO update)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"UPDATE personalbiblioteca SET contraseña='{update.contraseña}',email='{update.email}',nombre='{update.nombre}', apellidos='{update.apellidos}', telefono='{update.telefono}',direccion='{update.direccion}' where usuario='{update.usuario}'");

                return update;

            }
        }
        catch (Exception errorUpdate)
        {
            throw errorUpdate.GetBaseException();
        }
    }

    public string DeleteSelectedPersonalBiblioteca(string id)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {

                var delete = connection.Execute($"DELETE FROM personalbiblioteca where usuario='{id}'");

                switch (delete)
                {
                    case 0:
                        return "EL biblotecario no existe, intentelo nuevamente";
                        break;
                    case 1:
                        return "El bibliotecario: " + id + " fue eliminado";
                        break;

                    default:
                        return "Ocurrio un error interno y no se pudo eliminar el valor";
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

   
}