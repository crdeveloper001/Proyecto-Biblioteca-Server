using Biblioteca_Server.DatabaseAccess;
using Biblioteca_Server.DTO;
using Biblioteca_Server.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Biblioteca_Server.Services;

public class LectorService : ILector
{
    private DatabaseDAO dbaccess = new DatabaseDAO();
    public string ErrorHandler(string errorMessage)
    {
        return errorMessage;
    }
    public List<LectorDTO> GetAllLectores()
    {
        using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                    "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                    dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
        {
            return connection.Query<LectorDTO>("SELECT * FROM lector").ToList();
        }
    }

    public Object SearchLectorByName(string lectorName)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var UserInfo = connection.QuerySingle(
                    $"select  * from lector where nombre='{lectorName}'");

                LectorDTO payload = new LectorDTO()
                {
                    nombre = UserInfo.nombre,
                    apellidos = UserInfo.apellidos,
                    cedula = UserInfo.cedula,
                    direccion = UserInfo.direccion,
                    edad = UserInfo.edad,
                    email = UserInfo.email,
                    telefono = UserInfo.telefono,
                    gradoAcademico = UserInfo.gradoAcademico

                };
                if (payload == null)
                {
                    return "NO SE ENCONTRO LA INFORMACION DEL LECTOR, INTENTELO NUEVAMENTE";
                }
                else
                {
                    return payload;
                }

            }
        }
        catch (Exception errorAuthentication)
        {
            return ErrorHandler($"NO SE ENCONTRO INFORMACION SOBRE->{lectorName}, INTENTELO NUEVAMENTE");
        }
    }

    public string RegisterNewLector(LectorDTO lector)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"INSERT INTO lector(cedula,nombre,apellidos,email,telefono,direccion,gradoAcademico,edad) VALUES ('{lector.cedula}','{lector.nombre}','{lector.apellidos}','{lector.email}','{lector.telefono}','{lector.direccion}','{lector.gradoAcademico}','{lector.edad}')");

                return "lector: " + lector.nombre + "Insertado a la base de datos";
            }
        }
        catch (Exception errorInsert)
        {
            return errorInsert.Message;
        }
    }

    public LectorDTO UpdateCurrentLector(LectorDTO update)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                connection.Execute(
                    $"UPDATE lector SET nombre = '{update.nombre}',apellidos='{update.apellidos}',email='{update.email}',telefono='{update.telefono}',direccion='{update.direccion}',gradoAcademico='{update.gradoAcademico}',edad='{update.edad}' where cedula='{update.cedula}'");

                return update;

            }
        }
        catch (Exception errorUpdate)
        {
            throw errorUpdate.GetBaseException();
        }
    }

    public string DeleteSelectLector(string id)
    {
        try
        {
            using (var connection = new MySqlConnection("Server=" + dbaccess.GetUrlDatabase() + ";Port=3306;" +
                                                        "Database=" + dbaccess.GetDatabaseName() + ";Uid=" +
                                                        dbaccess.GetUsername() + ";Pwd=" + dbaccess.GetPassword()))
            {
                var delete = connection.Execute($"DELETE FROM lector where cedula='{id}'");

                switch (delete)
                {
                    case 0:
                        return "EL lector no existe, intentelo nuevamente";
                        break;
                    case 1:
                        return "El lector: " + id + " fue eliminado";
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