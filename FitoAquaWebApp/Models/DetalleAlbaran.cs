using System.ComponentModel.DataAnnotations.Schema;

namespace FitoAquaWebApp.Models
{
    public class DetalleAlbaran
    {
        public int AlbaranId { get; set; }
        public Albaran Albaran { get; set; } = null!;

        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        public int CantidadUsada { get; set; }
    }
}
