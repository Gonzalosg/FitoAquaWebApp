using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{
    public class Albaran
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string MesReferencia { get; set; }

        [ForeignKey("Obra")]
        public int ObraId { get; set; }
        public Obra Obra { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Usuario Empleado { get; set; }

        public ICollection<DetalleAlbaran>? Detalles { get; set; }
    }
}
