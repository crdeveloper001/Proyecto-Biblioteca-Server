namespace Biblioteca_Server.DatabaseAccess;

public class DatabaseDAO : IDatabaseDAO
{
    private readonly string databaseURL = "n4m3x5ti89xl6czh.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
    private readonly string databaseUsername = "e6j30xqyw2qwtfep";
    private readonly string databasePassword = "p2va19nknwkxjv4w";
    private readonly string databaseName = "c9xpffa11ew9crd1";
   
    public string GetUrlDatabase()
    {
        return databaseURL;
    }
    public string GetUsername()
    {
        return databaseUsername;
    }
    public string GetPassword()
    {
        return databasePassword;
    }
    public string GetDatabaseName()
    {
        return databaseName;
    }
}