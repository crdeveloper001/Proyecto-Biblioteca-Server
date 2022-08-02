namespace Biblioteca_Server.DTO;

public class LibroDTO
{
    public string isbn { get; set; }
    public string titulo { get; set; }
    public string editorial { get; set; }
    public int añoPublicacion { get; set; }
    public string genero { get; set; }
    public int cantidad { get; set; }
    public string imagen { get; set; }
}