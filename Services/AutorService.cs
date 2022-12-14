using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Biblioteca_Server.Services;

public class AutorService : IAutor //Implementa la interface con los metodo crud, esta clase no se puede modificar por dependencia de la interface
{
    private DatabaseDAO dbaccess = new DatabaseDAO();
    public string ErrorHandler(string errorMessage)
    {
        return errorMessage;
    }
    public List<AutorDTO> GetAllAutores()
    {
        using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                    "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                    dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
        {
            return connection.Query<AutorDTO>("SELECT * FROM Autor").ToList();
        }
    }

    public object SearchAutorByName(string name)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var UserInfo = connection.QuerySingle(
                    $"select  * from Autor where nombre='{name}'");

                AutorDTO payload = new AutorDTO()
                {
                    nombre = UserInfo.nombre,
                    apellidos = UserInfo.apellidos,
                    nacionalidad = UserInfo.nacionalidad,
                    codigoAutor = UserInfo.codigoAutor,

                };
                if (payload == null)
                {
                    return "NO SE ENCONTRO LA INFORMACION DEL AUTOR, INTENTELO NUEVAMENTE";
                }
                else
                {
                    return payload;
                }

            }
        }
        catch (Exception errorAuthentication)
        {
            return ErrorHandler($"NO SE ENCONTRO INFORMACION SOBRE->{name}, INTENTELO NUEVAMENTE");
        }
    }

    public string RegisterNewAutor(AutorDTO autor)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"INSERT INTO autor(codigoAutor,nombre,apellidos,nacionalidad) VALUES ('{autor.codigoAutor}','{autor.nombre}','{autor.apellidos}','{autor.nacionalidad}')");

                return "Autor: " + autor.nombre + " ha sido insertado en la base de datos";
            }
        }
        catch (Exception errorInsert)
        {
            return errorInsert.Message;
        }
    }

    public Object UpdateCurrentAutor(AutorDTO update)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var updateMethod = connection.Execute(
                    $"UPDATE autor SET nombre = '{update.nombre}',apellidos='{update.apellidos}',nacionalidad='{update.nacionalidad}' where codigoAutor='{update.codigoAutor}'");

                switch (updateMethod)
                {
                    case 1:
                        return update;
                    case 0:
                        return "Ocurrio un error al ejecutar la transaccion, el codigo del autor no es valido o no existe";
                    default:
                        return "OCURRIO UN ERROR AL EJECUTAR LA TRANSACCION";
                }

            }
        }
        catch (Exception errorUpdate)
        {
            throw errorUpdate.GetBaseException();
        }
    }

    public string DeleteSelectAutor(string id)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var delete = connection.Execute($"DELETE FROM autor where codigoAutor='{id}'");

                switch (delete)
                {
                    case 0:
                        return "EL autor no existe, intentelo nuevamente";
                        break;
                    case 1:
                        return "El autor: " + id + " fue eliminado";
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