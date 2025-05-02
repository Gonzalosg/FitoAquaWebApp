using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{
    public enum EstadoAveria
    {
        Abierta,
        EnProceso,
        Terminada
    }

    public class ParteAveria
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }

        public string? FotoUrl { get; set; }

        public EstadoAveria Estado { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Usuario Empleado { get; set; }

        [ForeignKey("Obra")]
        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
