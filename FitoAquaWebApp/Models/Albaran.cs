using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{
    public class Albaran
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public string MesReferencia { get; set; } = string.Empty;

        [ForeignKey("Obra")]
        public int ObraId { get; set; }
        public Obra Obra { get; set; } = null!;

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Usuario Empleado { get; set; } = null!;

        public ICollection<DetalleAlbaran>? Detalles { get; set; } = new List<DetalleAlbaran>();
    }
}
