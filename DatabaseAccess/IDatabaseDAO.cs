namespace Biblioteca_Server.DatabaseAccess;

public interface IDatabaseDAO
{
    string GetUrlDatabase();
    string GetUsername();
    string GetPassword();
    string GetDatabaseName();
}