using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{
    public class FotoEstadoObra
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string FotoUrl { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Usuario Empleado { get; set; }

        [ForeignKey("Obra")]
        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
