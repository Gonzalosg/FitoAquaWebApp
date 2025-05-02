using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{
    public class Obra
    {

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Usuario? Cliente { get; set; }

        public ICollection<UsuarioObra>? Asignaciones { get; set; }
        public ICollection<Albaran>? Albaranes { get; set; }
        public ICollection<ParteAveria>? PartesAveria { get; set; }
        public ICollection<FotoEstadoObra>? FotosEstado { get; set; }
    }
}
