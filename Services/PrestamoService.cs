using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Biblioteca_Server.Services;

public class PrestamoService : IPrestamo
{
    private DatabaseDAO dbaccess = new DatabaseDAO();
    public string ErrorHandler(string errorMessage)
    {
        return errorMessage;
    }
    public List<PrestamoDTO> GetAllPrestamos()
    {
        using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                    "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                    dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()+";Convert Zero Datetime=True"))
        {
            return connection.Query<PrestamoDTO>("SELECT * FROM prestamo").ToList();
        }
    }

    public object SearchByLectorPrestamo(string lector)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()+";CHARSET=utf8;convert zero datetime=True"))
            {
                var UserInfo = connection.QuerySingle(
                    $"select  * from prestamo where lector='{lector}'");

                PrestamoDTO payload = new PrestamoDTO()
                {
                    idPrestamo = UserInfo.idPrestamo,
                    lector = UserInfo.lector,
                    libro = UserInfo.libro,
                    fechaPrestamo = UserInfo.fechaPrestamo,
                    fechaDevolucion = UserInfo.fechaDevolucion,
                    FechaDevuelto = UserInfo.FechaDevuelto,
                    personalBiblioteca = UserInfo.personalBiblioteca
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
            return ErrorHandler($"NO SE ENCONTRO INFORMACION SOBRE EL PRESTAMO->{lector}, INTENTELO NUEVAMENTE");
        }
    }

    public string RegisterNewPrestamo(PrestamoDTO prestamo)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()+";CHARSET=utf8"))
            {
               
              
                connection.Execute(
                    $"INSERT INTO prestamo(" +
                    $"idPrestamo," +
                    $"lector," +
                    $"libro," +
                    $"personalBiblioteca," +
                    $"fechaPrestamo," +
                    $"fechaDevolucion," +
                    $"fechaDevuelto) " +
                    $"VALUES ('{prestamo.idPrestamo}','{prestamo.lector}','{prestamo.libro}','{prestamo.personalBiblioteca}','{prestamo.fechaPrestamo}','{prestamo.fechaDevolucion}','{prestamo.FechaDevuelto}')");

                 return "Prestamo: " + prestamo.idPrestamo + " ha sido insertado en la base de datos";
            }
        }
        catch (Exception errorInsert)
        {
            return errorInsert.Message;
        }
    }

    public PrestamoDTO UpdateCurrentPrestamo(PrestamoDTO update)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"UPDATE prestamo SET lector='{update.lector}',libro='{update.libro}',personalBiblioteca='{update.personalBiblioteca}',fechaPrestamo='{update.fechaPrestamo}',fechaDevolucion='{update.fechaDevolucion}',FechaDevuelto='{update.FechaDevuelto}' where idPrestamo='{update.idPrestamo}'");

                return update;

            }
        }
        catch (Exception errorUpdate)
        {
            throw errorUpdate.GetBaseException();
        }
    }

    public string DeleteSelectPrestamo(string id)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var delete = connection.Execute($"DELETE FROM prestamo where idPrestamo='{id}'");

                switch (delete)
                {
                    case 0:
                        return "EL prestamo no existe, intentelo nuevamente";
                        break;
                    case 1:
                        return "El prestamo: " + id + " fue eliminado";
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