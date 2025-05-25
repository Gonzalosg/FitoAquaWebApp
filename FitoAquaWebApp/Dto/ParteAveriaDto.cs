namespace FitoAquaWebApp.DTOs
{
    public class ParteAveriaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public string Estado { get; set; } = "Abierta";
        public int EmpleadoId { get; set; }
        public string? NombreEmpleado { get; set; }
        public int ObraId { get; set; }
        public string? NombreObra { get; set; }

    }
}