namespace FitoAquaWebApp.Dto
{
    public class AlbaranDto
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int ObraId { get; set; }
        public string? NombreObra { get; set; }
        public int EmpleadoId { get; set; }
        public string? NombreEmpleado { get; set; }
        public List<AlbaranDetalleDto> Detalles { get; set; } = new List<AlbaranDetalleDto>();
    }
}
