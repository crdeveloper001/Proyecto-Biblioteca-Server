namespace Biblioteca_Server.DTO;

public class PrestamoDTO
{
    public int idPrestamo { get; set; }
    public string lector { get; set; }
    public string libro { get; set; }
    public string personalBiblioteca { get; set; }
    public string? fechaPrestamo { get; set; }
    public string fechaDevolucion { get; set; }
    public string FechaDevuelto { get; set; }
}