using System.ComponentModel.DataAnnotations;

namespace FitoAquaWebApp.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantidadStock { get; set; }
        public string? FotoUrl { get; set; }

        public ICollection<DetalleAlbaran>? DetallesAlbaran { get; set; }
    }
}
