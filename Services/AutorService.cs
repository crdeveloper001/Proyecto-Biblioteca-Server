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

                return "autor: " + autor.nombre + "Insertado a la base de datos";
            }
        }
        catch (Exception errorInsert)
        {
            return errorInsert.Message;
        }
    }

    public AutorDTO UpdateCurrentAutor(AutorDTO update)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"UPDATE autor SET nombre = '{update.nombre}',apellidos='{update.apellidos}',nacionalidad='{update.nacionalidad}' where codigoAutor='{update.codigoAutor}'");

                return update;

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
                connection.Execute($"DELETE FROM autor where codigoAutor='{id}'");

                return "Autor con ID=> " + id + " Eliminado";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

   
}