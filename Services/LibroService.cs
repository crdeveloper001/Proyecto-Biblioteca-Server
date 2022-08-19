using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Biblioteca_Server.Services;

public class LibroService :ILibro
{
     private DatabaseDAO dbaccess = new DatabaseDAO();
     public string ErrorHandler(string errorMessage)
     {
         return errorMessage;
     }
    public List<LibroDTO> GetAllLibro()
    {
        using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                    "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                    dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
        {
            return connection.Query<LibroDTO>("SELECT * FROM libro").ToList();
        }
    }

    public object SearchLibroByName(string bookName)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var UserInfo = connection.QuerySingle(
                    $"select  * from libro where titulo='{bookName}'");

                LibroDTO payload = new LibroDTO()
                {
                    isbn = UserInfo.isbn,
                    titulo = UserInfo.titulo,
                    autor = UserInfo.autor,
                    genero = UserInfo.genero,
                    editorial = UserInfo.editorial,
                    añoPublicacion = UserInfo.añoPublicacion,
                    cantidad = UserInfo.cantidad,
                    imagen = UserInfo.imagen,
                   
                   
                };
                if (payload == null)
                {
                    return "NO SE ENCONTRO LA INFORMACION DEL LIBRO, INTENTELO NUEVAMENTE";
                }
                else
                {
                    return payload;
                }

            }
        }
        catch (Exception errorAuthentication)
        {
            return ErrorHandler($"NO SE ENCONTRO INFORMACION SOBRE->{bookName}, INTENTELO NUEVAMENTE");
        }
    }

    public string RegisterNewLibro(LibroDTO libro)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"INSERT INTO libro(isbn,titulo,autor,editorial,añoPublicacion,genero,cantidad,imagen) VALUES ('{libro.isbn}','{libro.titulo}','{libro.autor}','{libro.editorial}','{libro.añoPublicacion}','{libro.genero}','{libro.cantidad}','{libro.imagen}')");

                return "Libro: " + libro.titulo + " ha sido insertado en la base de datos";
            }
        }
        catch (Exception errorInsert)
        {
            return errorInsert.Message;
        }
    }

    public LibroDTO UpdateCurrentLibro(LibroDTO update)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"UPDATE libro SET titulo='{update.titulo}',autor='{update.autor}', editorial='{update.editorial}',añoPublicacion='{update.añoPublicacion}',genero='{update.genero}',cantidad='{update.cantidad}' where isbn='{update.isbn}'");

                return update;

            }
        }
        catch (Exception errorUpdate)
        {
            throw errorUpdate.GetBaseException();
        }
    }

    public string DeleteSelectLibro(string id)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var delete = connection.Execute($"DELETE FROM libro where isbn='{id}'");

                switch (delete)
                {
                    case 0:
                        return "EL libro no existe, intentelo nuevamente";
                        break;
                    case 1:
                        return "El libro: " + id + " fue eliminado";
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